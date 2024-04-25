using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ConcursModel.domain;
using ConcursNetwork.dto;
using ConcursNetwork.rpcprotocol;
using ConcursServices;
using Google.Protobuf;

namespace Concurs.Networking; 

public class ProtoConcursWorker : IConcursObserver{
    private IConcursServices server;
    private TcpClient connection;

    private NetworkStream stream;
    private volatile bool connected;
    
    public ProtoConcursWorker(IConcursServices server, TcpClient connection)
    {
        this.server = server;
        this.connection = connection;
        try
        {
				
            stream=connection.GetStream();
            connected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
    
    public void participantAdded(ConcursModel.domain.Participant participant) {
        try {
            sendResponse(ProtoUtils.createAddedParticipantResponse(participant));
            Console.WriteLine("Participant added " + participant);
        }
        catch (IOException e) {
            throw new ConcursException("Sending message: " + e.Message);
        }
    }

    private ConcursResponse handleRequest(ConcursRequest request) {
        ConcursResponse response = null;
        if (request.Type == ConcursRequest.Types.Type.Login) {
            Console.WriteLine("Login request ...");

            ConcursModel.domain.Admin admin = ProtoUtils.getAdmin(request);
            try {
                ConcursModel.domain.Admin loggedAdmin = null;
                lock (server) {
                    loggedAdmin = server.login(admin, this);
                }
                return ProtoUtils.createOkayForLoginResponse(loggedAdmin);
            }
            catch (ConcursException e) {
                connected = false;
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        if (request.Type == ConcursRequest.Types.Type.Logout) {
            Console.WriteLine("Logout request ...");
            ConcursModel.domain.Admin admin = ProtoUtils.getAdmin(request);
            try {
                lock (server) {
                    server.logout(admin, this);
                }

                connected = false;
                return ProtoUtils.createOkResponse();
            }
            catch (ConcursException e) {
                connected = false;
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        if (request.Type == ConcursRequest.Types.Type.AddParticipant) {
            Console.WriteLine("Added participant request ...");
            
            ConcursModel.domain.Participant participant = ProtoUtils.getParticipant(request);
            try {
                lock (server) {
                    server.addParticipant(participant);
                }

                return ProtoUtils.createOkResponse();
            }
            catch (ConcursException e) {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        if (request.Type == ConcursRequest.Types.Type.GetProbe) {
            Console.WriteLine("Probe requested ...");

            try {
                List<ConcursModel.domain.Proba> probe = null;
                lock (server) {
                    probe = server.getProbe();
                }
                return ProtoUtils.createGetProbeResponse(probe);
            }
            catch (ConcursException e) {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        
        if (request.Type == ConcursRequest.Types.Type.GetParticipanti) {
            Console.WriteLine("Participanti requested ...");

            try {
                List<ConcursModel.domain.Participant> participanti = null;
                lock (server) {
                    participanti = server.getParticipanti();
                }
                
                return ProtoUtils.createGetParticipantiResponse(participanti);
            }
            catch (ConcursException e) {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        
        if (request.Type == ConcursRequest.Types.Type.GetProba) {
            Console.WriteLine("Get proba request ...");
            int id = ProtoUtils.getIdProba(request);
            try {
                ConcursModel.domain.Proba proba = null;
                lock (server) {
                    proba = server.getProba(id);
                }

                if (proba == null) {
                    proba = new ConcursModel.domain.Proba(-1, Distanta.M50, Stil.MIXT);
                    proba.nrParticipanti = -1;
                }
                
                return ProtoUtils.createGetProbaResponse(proba);
            }
            catch (ConcursException e) {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        
        if (request.Type == ConcursRequest.Types.Type.CautaParticipanti) {
            Console.WriteLine("Cauta request ...");
            String searchString = ProtoUtils.getSearchString(request);
            try {
                List<ConcursModel.domain.Participant> participanti = null;
                lock (server) {
                    participanti = server.getParticipantiByString(searchString);
                }
                
                return ProtoUtils.createCautaParticipantiResponse(participanti);
            }
            catch (ConcursException e) {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        return response;
    }


    public virtual void run()
    {
        while(connected)
        {
            try {
                ConcursRequest request = ConcursRequest.Parser.ParseDelimitedFrom(stream);
                ConcursResponse response = handleRequest(request);
                if (response!=null)
                {
                    sendResponse(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
				
            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        try
        {
            stream.Close();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error " + e);
        }
    }
    
    private void sendResponse(ConcursResponse response)
    {
        Console.WriteLine("sending response "+response);
        lock (stream)
        {
            response.WriteDelimitedTo(stream);
            stream.Flush();
        }

    }
}