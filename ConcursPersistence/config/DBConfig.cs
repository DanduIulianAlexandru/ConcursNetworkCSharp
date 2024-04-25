namespace ConcursPersistence.config; 

public class DBConfig {
    public string host { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string database { get; set; }

    public DBConfig() {
        
    }

    public void loadConfig(string fileName) {
        string[] lines = System.IO.File
            .ReadAllLines(fileName);
        host = lines[0];
        username = lines[1];
        password = lines[2];
        database = lines[3];
    }
}