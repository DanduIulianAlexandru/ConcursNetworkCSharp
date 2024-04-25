using System;
using System.Net.Sockets;
using System.Threading;
using Concurs.Networking;
using ConcursModel.domain.validator;
using ConcursNetwork;
using ConcursNetwork.rpcprotocol;
using ConcursPersistence.config;
using ConcursPersistence.repository;
using ConcursServices;

namespace ConcursService; 

class StartServer {
    static void Main(string[] args) {
        DBConfig dbConfig = new DBConfig();
        dbConfig.loadConfig("db.config");
        
        DBRepositoryAdmin repositoryAdmin = new DBRepositoryAdmin(dbConfig, new ValidatorAdmin());
        DBRepositoryParticipant repositoryParticipant =
            new DBRepositoryParticipant(dbConfig, new ValidatorParticipant());
        DBRepositoryProba repositoryProba = new DBRepositoryProba(dbConfig, new ValidatorProba());
        IConcursServices serviceImpl = new ConcursServiceImpl(repositoryAdmin, repositoryParticipant, repositoryProba);
        
        //SerialChatServer server = new SerialChatServer("127.0.0.1", 55555, serviceImpl);
        ProtoConcursServer server = new ProtoConcursServer("127.0.0.1", 55555, serviceImpl);
        
        server.Start();
        Console.WriteLine("Server started ...");
    }
}

public class SerialChatServer: ConcurrentServer 
{
    private IConcursServices server;
    private ConcursClientRpcWorker worker;
    public SerialChatServer(string host, int port, IConcursServices server) : base(host, port)
    {
        this.server = server;
        Console.WriteLine("SerialChatServer...");
    }
    protected override Thread createWorker(TcpClient client)
    {
        worker = new ConcursClientRpcWorker(server, client);
        return new Thread(new ThreadStart(worker.run));
    }
}

public class ProtoConcursServer : ConcurrentServer {
    private IConcursServices server;
    private ProtoConcursWorker worker;

    public ProtoConcursServer(string host, int port, IConcursServices server) : base(host, port) {
        this.server = server;
        Console.WriteLine("Proto Concurs Server...");
    }
    
    protected override Thread createWorker(TcpClient client)
    {
        worker = new ProtoConcursWorker(server, client);
        return new Thread(new ThreadStart(worker.run));
    }
}