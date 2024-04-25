using System;
using System.Collections.Generic;
using ConcursModel.domain;

namespace ConcursNetwork.dto; 

public class DTOUtils {
    private static string convertArrayToString(int[] probe) {
        string s = "";
        int c = 0;
        foreach (int p in probe) {
            c++;
            s += p.ToString();
            if (c != probe.Length) {
                s += " ";
            }
        }

        return s;
    }
    
    public static bool IsNumeric(string strNum) {
        if (strNum == null) {
            return false;
        }
        try {
            int.Parse(strNum);
        }
        catch (FormatException) {
            return false;
        }
        return true;
    }

    private static int[] convertStringToArray(string probeS) {
        int nrProbe = 0;
        string[] parts = probeS.Split('\u0020');
        int[] probe = new int[parts.Length];
        foreach (string part in parts)
        {
            if (IsNumeric(part)) {
                probe[nrProbe] = int.Parse(part);
                ++nrProbe;
            }
        }

        return probe;
    }
    
    public static Admin GetFromDTO(AdminDTO adminDTO)
    {
        string id = adminDTO.id;
        string username = adminDTO.username;
        string password = adminDTO.passwd;
        Admin admin = new Admin(int.Parse(id), username, password);
        return admin;
    }

    public static Participant GetFromDTO(ParticipantDTO participantDTO) {
        string id = participantDTO.id;
        string nume = participantDTO.nume;
        string varsta = participantDTO.varsta;
        string probeS = participantDTO.probe;
        int[] probe = convertStringToArray(probeS);
        Participant participant = new Participant(int.Parse(id), nume, int.Parse(varsta), probe);
        return participant;
    }

    public static Proba GetFromDTO(ProbaDTO probaDTO) {
        string id = probaDTO.id;
        string distantaS = probaDTO.distanta;
        string stilS = probaDTO.stil;
        
        Distanta distanta = distantaS switch
        {
            "M50" => Distanta.M50,
            "M200" => Distanta.M200,
            "M800" => Distanta.M800,
            "M1500" => Distanta.M1500,
            _ => Distanta.M50
        };

        Stil stil = stilS switch
        {
            "LIBER" => Stil.LIBER,
            "FLUTURE" => Stil.FLUTURE,
            "MIXT" => Stil.MIXT,
            "SPATE" => Stil.SPATE,
            _ => Stil.LIBER
        };
        Proba proba = new Proba(int.Parse(id), distanta, stil);
        proba.nrParticipanti = int.Parse(probaDTO.nrParticipanti);
        return proba;
    }

    public static AdminDTO getDTO(Admin admin) {
        return new AdminDTO(admin.Id.ToString(), admin.username, admin.password);
    }

    public static ParticipantDTO getDTO(Participant participant) {
        return new ParticipantDTO(participant.Id.ToString(), participant.nume, participant.varsta.ToString(),
            convertArrayToString(participant.probe));
    }

    public static ProbaDTO getDTO(Proba proba) {
        string distanta = proba.distanta switch
        {
            Distanta.M50 => "M50",
            Distanta.M200 => "M200",
            Distanta.M800 => "M800",
            Distanta.M1500 => "M1500",
            _ => throw new Exception("Unknown distanta")
        };

        string stil = proba.stil switch
        {
            Stil.MIXT => "MIXT",
            Stil.LIBER => "LIBER",
            Stil.SPATE => "SPATE",
            Stil.FLUTURE => "FLUTURE",
            _ => throw new Exception("Unknown stil")
        };
        return new ProbaDTO(proba.Id.ToString(), distanta, stil, proba.nrParticipanti.ToString());
    }

    public static List<ProbaDTO> getProbeDTO(List<Proba> probe) {
        List<ProbaDTO> probeDTO = new List<ProbaDTO>();
        foreach (Proba p in probe) {
            probeDTO.Add(getDTO(p));
        }
        return probeDTO;
    }

    public static List<Proba> getProbeFromDTO(List<ProbaDTO> probeDTO) {
        List<Proba> probe = new List<Proba>();
        foreach (ProbaDTO p in probeDTO) {
            probe.Add(GetFromDTO(p));
        }
        return probe;
    }

    public static List<ParticipantDTO> getParticipantiDTO(List<Participant> participanti) {
        List<ParticipantDTO> participantiDTO = new List<ParticipantDTO>();
        foreach (Participant p in participanti) {
            participantiDTO.Add(getDTO(p));
        }

        return participantiDTO;
    }

    public static List<Participant> getParticipantiFromDTO(List<ParticipantDTO> participantiDTO) {
        List<Participant> participanti = new List<Participant>();
        foreach (ParticipantDTO p in participantiDTO) {
            participanti.Add(GetFromDTO(p));
        }

        return participanti;
    }
}