using System;

namespace ConcursNetwork.dto; 

[Serializable]
public class ProbaDTO {
    public string id { get; }
    public string distanta { get; }
    public string stil { get; }
    public string nrParticipanti { get; }
    
    public ProbaDTO(string id, string distanta, string stil, string nrParticipanti)
    {
        this.id = id;
        this.distanta = distanta;
        this.stil = stil;
        this.nrParticipanti = nrParticipanti;
    }
    
    public override string ToString()
    {
        return "ProbaDTO{" +
               "id='" + id + '\'' +
               ", distanta='" + distanta + '\'' +
               ", stil='" + stil + '\'' +
               ", nrParticipanti='" + nrParticipanti + '\'' + 
               '}';
    }
}