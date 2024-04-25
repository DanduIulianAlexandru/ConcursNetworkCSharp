using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ConcursModel.domain;
using ConcursNetwork.dto;
using ConcursServices;


namespace ConcursNetwork.rpcprotocol; 

public class ConcursServicesRpcProxy : IConcursServices{
    private string host;
    private int port;
    
    private IConcursObserver client;

    private NetworkStream stream;
		
    private IFormatter formatter;
    private TcpClient connection;

    private Queue<Response> responses;
    private volatile bool finished;
    private EventWaitHandle _waitHandle;
    
    public ConcursServicesRpcProxy(string host, int port)
    {
        this.host = host;
        this.port = port;
        responses=new Queue<Response>();
    }
    
    public Admin login(Admin admin, IConcursObserver client) {
        Console.WriteLine("ajutor");
        initializeConnection();
        AdminDTO adminDto = DTOUtils.getDTO(admin);
        Request request = new Request.Builder().Data(adminDto).Type(RequestType.LOGIN).Build();
        
        sendRequest(request);
        
        Response response = readResponse();
       
        if (response.Type == ResponseType.OK) {
            if (response.Data == null) {
                return null;
            }
            this.client = client;
            
            return DTOUtils.GetFromDTO((AdminDTO) response.Data);
        }

        if (response.Type == ResponseType.ERROR) {
            string err = (string)response.Data;
            closeConnection();
            throw new ConcursException(err);
        }

        return null;
    }

    public void logout(Admin admin, IConcursObserver client) {
        AdminDTO adminDTO = DTOUtils.getDTO(admin);
        Request request = new Request.Builder().Type(RequestType.LOGOUT).Data(adminDTO).Build();
        sendRequest(request);
        Response response = readResponse();
        closeConnection();
        if (response.Type == ResponseType.ERROR) {
            string err = (string) response.Data;
            throw new ConcursException(err);
        }
    }

    public void addParticipant(Participant participant) {
        ParticipantDTO participantDTO = DTOUtils.getDTO(participant);
        Request request = new Request.Builder().Type(RequestType.ADD_PARTICIPANT).Data(participantDTO).Build();
        sendRequest(request);
        Response response = readResponse();

        if (response.Type == ResponseType.ERROR) {
            string err = (string)response.Data;
            throw new ConcursException(err);
        }
    }

    public List<Proba> getProbe() {
        Request request = new Request.Builder().Type(RequestType.GET_PROBE).Build();
        sendRequest(request);
        Response response = readResponse();
        if (response.Type == ResponseType.ERROR) {
            String err = (string)response.Data;
            throw new ConcursException(err);
        }

        return DTOUtils.getProbeFromDTO((List<ProbaDTO>) response.Data);
    }

    public List<Participant> getParticipanti() {
        Request request = new Request.Builder().Type(RequestType.GET_PARTICIPANTI).Build();
        sendRequest(request);
        Response response = readResponse();
        if (response.Type == ResponseType.ERROR) {
            String err = (string)response.Data;
            throw new ConcursException(err);
        }

        return DTOUtils.getParticipantiFromDTO((List<ParticipantDTO>)response.Data);
    }

    public Proba getProba(int id) {
        Request request = new Request.Builder().Type(RequestType.GET_PROBA).Data(id.ToString()).Build();
        sendRequest(request);
        Response response = readResponse();
        if (response.Type == ResponseType.ERROR) {
            String err = (string)response.Data;
            throw new ConcursException(err);
        }
        if (response.Data == null) {
            return null;
        }

        return DTOUtils.GetFromDTO((ProbaDTO) response.Data);
    }

    public List<Participant> getParticipantiByString(string searchString) {
        Request request = new Request.Builder().Type(RequestType.CAUTA_PARTICIPANTI).Data(searchString).Build();
        sendRequest(request);
        Response response = readResponse();
        if (response.Type == ResponseType.ERROR) {
            String err = (string)response.Data;
            throw new ConcursException(err);
        }
        if (response.Data == null) {
            return new List<Participant>();
        }
        return DTOUtils.getParticipantiFromDTO((List<ParticipantDTO>)response.Data);
    }
    
    private void initializeConnection()
    {
        try
        {
            connection=new TcpClient(host,port);
            stream=connection.GetStream();
            formatter = new BinaryFormatter();
            finished=false;
            _waitHandle = new AutoResetEvent(false);
            startReader();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
    
    private void closeConnection()
    {
        finished=true;
        try
        {
            stream.Close();
			
            connection.Close();
            _waitHandle.Close();
            client=null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }

    }
    
    private void sendRequest(Request request) {
        try {
            formatter.Serialize(stream, request);
            stream.Flush();
        }
        catch (Exception e) {
            throw new ConcursException("Error sending object " + e);
        }

    }
    
    private Response readResponse() {
        Response response =null;
        try {
            _waitHandle.WaitOne();
            lock (responses) {
                //Monitor.Wait(responses); 
                response = responses.Dequeue();
                
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.StackTrace);
        }
        return response;
    }
    
    private void startReader() {
        Thread tw =new Thread(run);
        tw.Start();
    }
    
    private void handleUpdate(Response response) {
        if (response.Type == ResponseType.ADDED_PARTICIPANT) {
            Participant participant = DTOUtils.GetFromDTO((ParticipantDTO) response.Data);
            Console.WriteLine("Participant added " + participant);
            try {
                client.participantAdded(participant);
            }
            catch (ConcursException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
    
    private Boolean isUpdate(Response response) {
        return response.Type == ResponseType.ADDED_PARTICIPANT;
    }
    
    public virtual void run()
    {
        while(!finished)
        {
            try
            {
                object response = formatter.Deserialize(stream);
                Console.WriteLine("response received "+response);
                if (isUpdate((Response) response))
                {
                    handleUpdate((Response)response);
                }
                else
                {
                    lock (responses)
                    {
                        responses.Enqueue((Response)response);
                               
                    }
                    _waitHandle.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Reading error "+e);
            }
					
        }
    }
}