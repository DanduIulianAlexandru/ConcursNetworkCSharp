using System;
using System.Collections.Generic;
using System.IO;
using ConcursPersistence.config;
using ConcursModel.domain;
using ConcursModel.domain.validator;
using Npgsql;

namespace ConcursPersistence.repository; 

public class DBRepositoryAdmin : IRepository<int, Admin> {
    private ValidatorAdmin validatorAdmin;
    private const string TABLE_NAME = "admin";
    private NpgsqlConnection connection;
    
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);

    public DBRepositoryAdmin(DBConfig dbConfig, ValidatorAdmin validatorAdmin) {
        this.validatorAdmin = validatorAdmin;
        string CONNECTION_STRING = "Host=" + dbConfig.host + ";" +
                                   "Username="+ dbConfig.username + ";" +
                                   "Password=" + dbConfig.password + ";" +
                                   "Database=" + dbConfig.database;
        logger.Info("Connecting to database using: " + dbConfig.host + " " + dbConfig.username);
        connection = new NpgsqlConnection(CONNECTION_STRING);
        connection.Open();
        logger.Info("Connected...");
    }


    public Admin FindOne(int id) { 
        logger.Info("Searching for the admin with id " + id);
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                if(reader.Read()) {
                    var j = (Int64)reader["id"];
                    var username = reader["username"] as string;
                    var password = reader["password"] as string;
                    
                    Admin admin = new Admin((int)j, username, password);
                    logger.Info("Found admin");
                    return admin;
                }
        }
        logger.Info("Admin not found");
        return null;
    }
    
    public Admin FindBy(string nume, string password1) { 
        logger.Info("Searching for the admin with name " + nume);
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE username = @nume and password = @password1";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("nume", nume);
            cmd.Parameters.AddWithValue("password1", password1);
            // Console.WriteLine(nume);
            // Console.WriteLine(password1);
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                if(reader.Read()) {
                    var j = (Int64)reader["id"];
                    var username = reader["username"] as string;
                    var password = reader["password"] as string;
                    
                    Admin admin = new Admin((int)j, username, password);
                    logger.Info("Found admin");
                    return admin;
                }
        }
        logger.Info("Admin not found");
        return null;
    }

    public Admin FindByName(string name) {
        logger.Info("Searching for the admin with name " + name);
        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE username = @username";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("username", name);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                if(reader.Read()) {
                    var j = (Int64)reader["id"];
                    var username = reader["username"] as string;
                    var password = reader["password"] as string;
                    
                    Admin admin = new Admin((int)j, username, password);
                    logger.Info("Found admin");
                    return admin;
                }
        }
        logger.Info("Admin not found");
        return null;
    }

    public IEnumerable<Admin> FindAll() {
        logger.Info("Searching for all admins");
        List<Admin> admins = new List<Admin>();
        string commandText = $"SELECT * FROM {TABLE_NAME}";
        using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read()) {
                    var ID = (Int64)reader["id"];
                    string username = reader["username"] as string;
                    string password = reader["password"] as string;

                    Admin admin = new Admin((int)ID, username, password);
                    admins.Add(admin);
                }
        }
        logger.Info("Found " + admins.Count + " admins");
        return admins;
    }

    public Admin Save(Admin entity) {
        logger.Info("saving task " + entity);
        validatorAdmin.Validate(entity);
        string commandText = $"INSERT INTO {TABLE_NAME} (username, password) VALUES (@username, @password)";
        using (var cmd = new NpgsqlCommand(commandText, connection)) {
            cmd.Parameters.AddWithValue("username", entity.username);
            cmd.Parameters.AddWithValue("password", entity.password);

            cmd.ExecuteNonQuery();
        }
        logger.Info("saved task " + entity);
        return entity;
    }

    public Admin Delete(int id) {
        logger.Info("deleting task " + id);
        Admin admin = FindOne(id);
        if (admin == null) {
            return null;
        }
        
        string commandText = $"DELETE FROM {TABLE_NAME} WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
        logger.Info("deleted task " + id);
        return admin;
    }

    public Admin Update(Admin entity) {
        logger.Info("updating task " + entity);
        int oldId = entity.Id;
        Admin oldAdmin = FindOne(oldId);
        if (oldAdmin == null) {
            return null;
        }

        string commandText = $@"UPDATE {TABLE_NAME} SET username = @username, password = @password WHERE id = @id";
        using (var cmd = new NpgsqlCommand(commandText, connection))
        {
            cmd.Parameters.AddWithValue("id", entity.Id);
            cmd.Parameters.AddWithValue("username", entity.username);
            cmd.Parameters.AddWithValue("password", entity.password);

            cmd.ExecuteNonQuery();
        }
        logger.Info("updated task " + entity);
        return oldAdmin;
    }
}