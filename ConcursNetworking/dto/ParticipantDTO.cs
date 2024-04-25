using System;

namespace ConcursNetwork.dto; 

[Serializable]
public class ParticipantDTO {
    public string id { get; }
    public string nume { get; }
    public string varsta { get; }
    public string probe { get; }
    
    public ParticipantDTO(string id, string nume, string varsta, string probe)
    {
        this.id = id;
        this.nume = nume;
        this.varsta = varsta;
        this.probe = probe;
    }
    
    public override string ToString()
    {
        return "ParticipantDTO{" +
               "id='" + id + '\'' +
               ", nume='" + nume + '\'' +
               ", varsta='" + varsta + '\'' +
               ", probe='" + probe + '\'' +
               '}';
    }
}