using System;

namespace ConcursNetwork.dto; 

[Serializable]
public class AdminDTO {
    public string id { get; }
    public string username { get; }
    public string passwd { get; }
    
    public AdminDTO(string id, string username, string pass)
    {
        this.id = id;
        this.username = username;
        this.passwd = pass;
    }
    
    public override string ToString()
    {
        return $"AdminDTO{{id='{id}', username='{username}', passwd='{passwd}'}}";
    }
}