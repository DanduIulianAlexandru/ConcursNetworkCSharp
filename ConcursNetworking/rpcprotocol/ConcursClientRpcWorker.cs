using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ConcursModel.domain;
using ConcursNetwork.dto;
using ConcursServices;

namespace ConcursNetwork.rpcprotocol; 

public class ConcursClientRpcWorker : IConcursObserver {
    private IConcursServices server;
    private TcpClient connection;

    private NetworkStream stream;
    private IFormatter formatter;
    private volatile bool connected;
    public ConcursClientRpcWorker(IConcursServices server, TcpClient connection)
    {
        this.server = server;
        this.connection = connection;
        try
        {
				
            stream=connection.GetStream();
            formatter = new BinaryFormatter();
            connected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
    
    public void participantAdded(Participant participant) {
        ParticipantDTO participantDTO = DTOUtils.getDTO(participant);
        Response response = new Response.Builder().Type(ResponseType.ADDED_PARTICIPANT).Data(participantDTO).Build();

        try {
            sendResponse(response);
            Console.WriteLine("Participant added " + participant);
        }
        catch (IOException e) {
            throw new ConcursException("Sending message: " + e.Message);
        }
    }

    private Response handleRequest(Request request) {
        Response response = null;
        if (request.Type == RequestType.LOGIN) {
            Console.WriteLine("Login request ...");
            AdminDTO adminDTO = (AdminDTO)request.Data;
            Admin admin = DTOUtils.GetFromDTO(adminDTO);
            
            try {
                Admin loggedAdmin = null;
                
                lock (server) {
                   
                    loggedAdmin = server.login(admin, this);
                }
                
                AdminDTO loggedAdminDTO = DTOUtils.getDTO(loggedAdmin);
                
                return new Response.Builder().Type(ResponseType.OK).Data(loggedAdminDTO).Build();
              
            }
            catch (ConcursException e) {
                connected = false;
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }
        if (request.Type == RequestType.LOGOUT) {
            Console.WriteLine("Logout request ...");
            AdminDTO adminDTO = (AdminDTO)request.Data;
            Admin admin = DTOUtils.GetFromDTO(adminDTO);
            try {
                lock (server) {
                    server.logout(admin, this);
                }

                connected = false;
                return new Response.Builder().Type(ResponseType.OK).Data(adminDTO).Build();
            }
            catch (ConcursException e) {
                connected = false;
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }

        if (request.Type == RequestType.ADD_PARTICIPANT) {
            Console.WriteLine("Added participant request ...");
            ParticipantDTO participantDTO = (ParticipantDTO)request.Data;
            Participant participant = DTOUtils.GetFromDTO(participantDTO);
            try {
                lock (server) {
                    server.addParticipant(participant);
                }

                return new Response.Builder().Type(ResponseType.OK).Data(participantDTO).Build();
            }
            catch (ConcursException e) {
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }

        if (request.Type == RequestType.GET_PROBE) {
            Console.WriteLine("Probe requested ...");

            try {
                List<Proba> probe = null;
                lock (server) {
                    probe = server.getProbe();
                }

                List<ProbaDTO> probeDTO = DTOUtils.getProbeDTO(probe);
                return new Response.Builder().Type(ResponseType.OK).Data(probeDTO).Build();
            }
            catch (ConcursException e) {
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }
        
        if (request.Type == RequestType.GET_PARTICIPANTI) {
            Console.WriteLine("Probe requested ...");

            try {
                List<Participant> participanti = null;
                lock (server) {
                    participanti = server.getParticipanti();
                }

                List<ParticipantDTO> participantiDTO = DTOUtils.getParticipantiDTO(participanti);
                return new Response.Builder().Type(ResponseType.OK).Data(participantiDTO).Build();
            }
            catch (ConcursException e) {
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }
        
        if (request.Type == RequestType.GET_PROBA) {
            Console.WriteLine("Get proba request ...");
            String idS = (string)request.Data;
            try {
                Proba proba = null;
                lock (server) {
                    proba = server.getProba(int.Parse(idS));
                }

                if (proba == null) {
                    return new Response.Builder().Type(ResponseType.OK).Data(null).Build();
                }

                ProbaDTO probaDTO = DTOUtils.getDTO(proba);
                return new Response.Builder().Type(ResponseType.OK).Data(probaDTO).Build();
            }
            catch (ConcursException e) {
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }
        
        if (request.Type == RequestType.CAUTA_PARTICIPANTI) {
            Console.WriteLine("Cauta request ...");
            String searchString = (string)request.Data;
            try {
                List<Participant> participanti = null;
                lock (server) {
                    participanti = server.getParticipantiByString(searchString);
                }

                List<ParticipantDTO> participantiDTO = DTOUtils.getParticipantiDTO(participanti);
                return new Response.Builder().Type(ResponseType.OK).Data(participantiDTO).Build();
            }
            catch (ConcursException e) {
                return new Response.Builder().Data(e.Message).Type(ResponseType.ERROR).Build();
            }
        }

        return response;
    }


    public virtual void run()
    {
        while(connected)
        {
            try
            {
                object request = formatter.Deserialize(stream);
                
                object response = handleRequest((Request)request);
                if (response!=null)
                {
                    sendResponse((Response) response);
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
            Console.WriteLine("Error "+e);
        }
    }
    
    private void sendResponse(Response response)
    {
        Console.WriteLine("sending response "+response);
       
        lock (stream)
        {
           
            formatter.Serialize(stream, response);
            stream.Flush();
           
        }

    }
}
