namespace ConcursModel.domain.validator; 

public class ValidatorProba : IValidator<Proba>{
    public void Validate(Proba e) {
        string err = "";

        if (e.Id < 0) {
            err += "Id proba invalid! ";
        }

        if (err != "") {
            throw new ValidationException(err);
        }
    }
}