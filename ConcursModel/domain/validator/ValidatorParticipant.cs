namespace ConcursModel.domain.validator; 

public class ValidatorParticipant : IValidator<Participant>{
    public void Validate(Participant e) {
        string err = "";

        if (e.Id < 0) {
            err += "Id participant invalid! ";
        }

        if (e.nume == "") {
            err += "Numele participantului este invalid! ";
        }
        
        if (e.varsta < 5 || e.varsta > 90) {
            err += "Varsta participantului este invalida! ";
        }

        if (err != "") {
            throw new ValidationException(err);
        }
    }
}