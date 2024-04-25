using System;
using System.Collections.Generic;
using System.Linq;
using ConcursPersistence.config;
using ConcursModel.domain;
using ConcursModel.domain.validator;
using ConcursPersistence.repository;
using Npgsql;

namespace ConcursPersistence.repository; 

public class DBRepositoryParticipant : IRepository<int, Participant> {
    private ValidatorParticipant validatorParticipant;
    private const string TABLE_NAME = "participant";
    private NpgsqlConnection connection;
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);

    public DBRepositoryParticipant(DBConfig dbConfig, ValidatorParticipant validatorParticipant) {
        this.validatorParticipant = validatorParticipant;
        string CONNECTION_STRING = "Host=" + dbConfig.host + ";" +
                                   "Username="+ dbConfig.username + ";" +
                                   "Password=" + dbConfig.password + ";" +
                                   "Database=" + dbConfig.database;
        logger.Info("Connecting to database using: " + dbConfig.host + " " + dbConfig.username);
        connection = new NpgsqlConnection(CONNECTION_STRING);
        connection.Open();
        logger.Info("Connected...");
    }

    public Participant FindOne(int id) {
        logger.Info("Searching for the participant with id " + id);
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                if(reader.Read()) {
                    var j = (Int64)reader["id"];
                    var nume = reader["nume"] as string;
                    var varsta = (Int64)reader["varsta"];
                    long[] probeL = (long[])reader["probe"];
                    
                    int[] probe = probeL.Select(l => (int)l).ToArray();

                    Participant participant = new Participant(Convert.ToInt32(j),
                        nume, Convert.ToInt32(varsta), probe);
                    return participant;
                }
        }
        return null;
    }

    public IEnumerable<Participant> FindAll() {
        logger.Info("Searching for all participants");
        List<Participant> participants = new List<Participant>();
        string commandText = $"SELECT * FROM {TABLE_NAME}";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read()) {
                    var j = (Int64)reader["id"];
                    var nume = reader["nume"] as string;
                    var varsta = (Int64)reader["varsta"];
                    long[] probeL = (long[])reader["probe"];
                    
                    int[] probe = probeL.Select(l => (int)l).ToArray();

                    Participant participant = new Participant(Convert.ToInt32(j),
                        nume, Convert.ToInt32(varsta), probe);
                    participants.Add(participant);
                }
        }

        return participants;
    }
    
    public IEnumerable<Participant> FindAllByName(string name) {
        string like_name = "\'%" + name + "%\'";
        logger.Info("Searching for all participants with name " + name);
        List<Participant> participants = new List<Participant>();
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE nume like " + like_name;
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read()) {
                    var j = (Int64)reader["id"];
                    var nume = reader["nume"] as string;
                    var varsta = (Int64)reader["varsta"];
                    long[] probeL = (long[])reader["probe"];
                    
                    int[] probe = probeL.Select(l => (int)l).ToArray();

                    Participant participant = new Participant(Convert.ToInt32(j),
                        nume, Convert.ToInt32(varsta), probe);
                    participants.Add(participant);
                }
        }

        return participants;
    }
    
    public IEnumerable<Participant> FindAllByAge(int age) {
        logger.Info("Searching for all participants with age equal to " + age);
        List<Participant> participants = new List<Participant>();
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE varsta = " + age;
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read()) {
                    var j = (Int64)reader["id"];
                    var nume = reader["nume"] as string;
                    var varsta = (Int64)reader["varsta"];
                    long[] probeL = (long[])reader["probe"];
                    
                    int[] probe = probeL.Select(l => (int)l).ToArray();

                    Participant participant = new Participant(Convert.ToInt32(j),
                        nume, Convert.ToInt32(varsta), probe);
                    participants.Add(participant);
                }
        }

        return participants;
    }

    public Participant Save(Participant entity) {
        logger.Info("saving task " + entity);
        validatorParticipant.Validate(entity);
        string commandText = $"INSERT INTO {TABLE_NAME} (nume, varsta, probe) VALUES " +
                             $"(@nume, @varsta, @probe)";
        using (var cmd = new NpgsqlCommand(commandText, connection)) {
            cmd.Parameters.AddWithValue("nume", entity.nume);
            cmd.Parameters.AddWithValue("varsta", entity.varsta);
            cmd.Parameters.AddWithValue("probe", entity.probe);

            cmd.ExecuteNonQuery();
        }
        logger.Info("saved task " + entity);
        return entity;
    }

    public Participant Delete(int id) {
        logger.Info("deleting task " + id);
        Participant participant = FindOne(id);
        if (participant == null) {
            return null;
        }
        
        string commandText = $"DELETE FROM {TABLE_NAME} WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
        logger.Info("deleted task " + participant);
        return participant;
    }

    public Participant Update(Participant entity) {
        logger.Info("updating task " + entity);
        int oldId = entity.Id;
        Participant oldParticipant = FindOne(oldId);
        if (oldParticipant == null) {
            return null;
        }

        string commandText = $@"UPDATE {TABLE_NAME} SET nume = @nume, varsta = @varsta, probe = @probe WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("nume", entity.nume);
            cmd.Parameters.AddWithValue("varsta", entity.varsta);
            cmd.Parameters.AddWithValue("probe", entity.probe);

            cmd.ExecuteNonQuery();
        }
        logger.Info("updated task " + entity);
        return oldParticipant;
    }
}