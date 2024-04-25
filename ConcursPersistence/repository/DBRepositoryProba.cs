using System;
using System.Collections.Generic;
using ConcursPersistence.config;
using ConcursModel.domain;
using ConcursModel.domain.validator;
using Npgsql;

namespace ConcursPersistence.repository; 

public class DBRepositoryProba : IRepository<int, Proba> {
    private ValidatorProba validatorProba;
    private const string TABLE_NAME = "proba";
    private NpgsqlConnection connection;
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);

    public DBRepositoryProba(DBConfig dbConfig, ValidatorProba validatorProba) {
        this.validatorProba = validatorProba;
        string CONNECTION_STRING = "Host=" + dbConfig.host + ";" +
                                   "Username="+ dbConfig.username + ";" +
                                   "Password=" + dbConfig.password + ";" +
                                   "Database=" + dbConfig.database;
        logger.Info("Connecting to database using: " + dbConfig.host + " " + dbConfig.username);
        connection = new NpgsqlConnection(CONNECTION_STRING);
        connection.Open();
        logger.Info("Connected...");
    }

    public Proba FindOne(int id) {
        logger.Info("Searching for the proba with id " + id);
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                if(reader.Read()) {
                    var ID = (Int64)reader["id"];
                    var distantaS = reader["distanta"] as string;
                    var stilS = reader["stil"] as string;
                    var nrParticipanti = (Int64)reader["nr_participanti"];

                    Distanta distanta = distantaS switch {
                        "M50" => Distanta.M50,
                        "M200" => Distanta.M200,
                        "M800" => Distanta.M800,
                        "M1500" => Distanta.M1500,
                        _ => throw new ValidationException("Distanta Error")
                    };

                    Stil stil = stilS switch {
                        "FLUTURE" => Stil.FLUTURE,
                        "MIXT" => Stil.MIXT,
                        "SPATE" => Stil.SPATE,
                        "LIBER" => Stil.LIBER,
                        _ => throw new ValidationException("Stil enum error")
                    };

                    Proba proba = new Proba((int)ID, distanta, stil);
                    proba.nrParticipanti = (int)nrParticipanti;
                    return proba;
                }
        }
        return null;
    }

    public IEnumerable<Proba> FindAll() {
        logger.Info("Searching for all probe");
        List<Proba> probe = new List<Proba>();
        string commandText = $"SELECT * FROM {TABLE_NAME}";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read()) {
                    var ID = (Int64)reader["id"];
                    var distantaS = reader["distanta"] as string;
                    var stilS = reader["stil"] as string;
                    var nrParticipanti = (Int64)reader["nr_participanti"];

                    Distanta distanta = distantaS switch {
                        "M50" => Distanta.M50,
                        "M200" => Distanta.M200,
                        "M800" => Distanta.M800,
                        "M1500" => Distanta.M1500,
                        _ => throw new ValidationException("Distanta Error")
                    };

                    Stil stil = stilS switch {
                        "FLUTURE" => Stil.FLUTURE,
                        "MIXT" => Stil.MIXT,
                        "SPATE" => Stil.SPATE,
                        "LIBER" => Stil.LIBER,
                        _ => throw new ValidationException("Stil enum error")
                    };

                    Proba proba = new Proba((int)ID, distanta, stil);
                    proba.nrParticipanti = (int)nrParticipanti;
                    probe.Add(proba);
                }
        }

        return probe;
    }

    public Proba Save(Proba entity) {
        logger.Info("saving task " + entity);
        validatorProba.Validate(entity);
        string commandText = $"INSERT INTO {TABLE_NAME} (distanta, stil, nr_participanti) VALUES (@distanta, @stil, @nr)";
        using (var cmd = new NpgsqlCommand(commandText, connection)) {
            cmd.Parameters.AddWithValue("distanta", entity.distanta.ToString());
            cmd.Parameters.AddWithValue("stil", entity.stil.ToString());
            cmd.Parameters.AddWithValue("nr", entity.nrParticipanti);

            cmd.ExecuteNonQuery();
        }
        logger.Info("saved task " + entity);
        return entity;
    }

    public Proba Delete(int id) {
        logger.Info("deleting task " + id);
        Proba proba = FindOne(id);
        if (proba == null) {
            return null;
        }
        
        string commandText = $"DELETE FROM {TABLE_NAME} WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
        logger.Info("deleted task " + proba);
        return proba;
    }

    public Proba Update(Proba entity) {
        logger.Info("updating task " + entity);
        int oldId = entity.Id;
        Proba oldProba = FindOne(oldId);
        if (oldProba == null) {
            return null;
        }

        string commandText = $@"UPDATE {TABLE_NAME} SET distanta = @distanta, stil = @stil, nr_participanti = @nr WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("distanta", entity.distanta.ToString());
            cmd.Parameters.AddWithValue("stil", entity.stil.ToString());
            cmd.Parameters.AddWithValue("nr", entity.nrParticipanti);

            cmd.ExecuteNonQuery();
        }
        logger.Info("updated task " + entity);
        return oldProba;
    }

    public Proba incrementNr(int id) {
        if (FindOne(id) is null) {
            return null;
        }
        string commandText = $@"UPDATE {TABLE_NAME} SET nr_participanti = nr_participanti + 1 WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
        logger.Info("incremented task " + id);
        return FindOne(id);
        
    }
}