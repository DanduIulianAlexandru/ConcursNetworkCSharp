using System;
using System.Collections.Generic;
using ConcursNetwork.dto;

namespace Concurs.Networking;

public class ProtoUtils {
    public static ConcursResponse createOkResponse() {
        return new ConcursResponse{ Type = ConcursResponse.Types.Type.Ok };
    }

    public static ConcursResponse createErrorResponse(string err) {
        return new ConcursResponse { Type = ConcursResponse.Types.Type.Error, Error = err};
    }

    public static ConcursResponse createAddedParticipantResponse(ConcursModel.domain.Participant participant) {
        ParticipantDTO participantDTO = DTOUtils.getDTO(participant);
        Participant participantPB = new Participant {
            Id = participantDTO.id,
            Nume = participantDTO.nume,
            Varsta = participantDTO.varsta,
            Probe = participantDTO.probe
        };
        return new ConcursResponse { Type = ConcursResponse.Types.Type.AddedParticipant, Participant = participantPB };
    }

    public static ConcursResponse createOkayForLoginResponse(ConcursModel.domain.Admin admin) {
        AdminDTO adminDTO = DTOUtils.getDTO(admin);
        Admin adminPB = new Admin {
            Id = adminDTO.id,
            Username = adminDTO.username,
            Password = adminDTO.passwd
        };

        return new ConcursResponse { Type = ConcursResponse.Types.Type.Ok, Admin = adminPB };
    }

    public static ConcursResponse createGetProbeResponse(List<ConcursModel.domain.Proba> probe) {
        ConcursResponse response = new ConcursResponse { Type = ConcursResponse.Types.Type.GetProbe };
        List<ProbaDTO> probeDTO = DTOUtils.getProbeDTO(probe);

        foreach (ProbaDTO p in probeDTO) {
            response.Probe.Add(new Proba{ Id = p.id, Distanta = p.distanta, Stil = p.stil, NrParticipanti = p.nrParticipanti});
        }

        return response;
    }

    public static ConcursResponse createGetParticipantiResponse(List<ConcursModel.domain.Participant> participanti) {
        ConcursResponse response = new ConcursResponse { Type = ConcursResponse.Types.Type.GetParticipanti };
        List<ParticipantDTO> participantiDTO = DTOUtils.getParticipantiDTO(participanti);

        foreach (ParticipantDTO p in participantiDTO) {
            response.Participanti.Add(new Participant{Id = p.id, Nume = p.nume, Varsta = p.varsta, Probe = p.probe});
        }

        return response;
    }

    public static ConcursResponse createGetProbaResponse(ConcursModel.domain.Proba proba) {
        ProbaDTO probaDTO = DTOUtils.getDTO(proba);
        Proba probaPB = new Proba {
            Id = probaDTO.id,
            Distanta = probaDTO.distanta,
            Stil = probaDTO.stil,
            NrParticipanti = probaDTO.nrParticipanti
        };

        return new ConcursResponse { Type = ConcursResponse.Types.Type.GetProba, Proba = probaPB };
    }

    public static ConcursResponse createCautaParticipantiResponse(List<ConcursModel.domain.Participant> participanti) {
        ConcursResponse response = new ConcursResponse { Type = ConcursResponse.Types.Type.SearchedParticipanti };
        List<ParticipantDTO> participantiDTO = DTOUtils.getParticipantiDTO(participanti);

        foreach (ParticipantDTO p in participantiDTO) {
            response.Participanti.Add(new Participant{Id = p.id, Nume = p.nume, Varsta = p.varsta, Probe = p.probe});
        }

        return response;
    }

    public static ConcursModel.domain.Admin getAdmin(ConcursRequest request) {
        return DTOUtils.GetFromDTO(new AdminDTO(request.Admin.Id, request.Admin.Username, request.Admin.Password));
    }

    public static int getIdProba(ConcursRequest request) {
        return int.Parse(request.IdProba);
    }

    public static ConcursModel.domain.Participant getParticipant(ConcursRequest request) {
        return DTOUtils.GetFromDTO(new ParticipantDTO(request.Participant.Id, request.Participant.Nume,
            request.Participant.Varsta, request.Participant.Probe));
    }

    public static string getSearchString(ConcursRequest request) {
        return request.SearchString;
    }
}