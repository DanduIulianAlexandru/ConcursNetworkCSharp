using ConcursModel.domain;
namespace ConcursModel.domain.validator; 

public class ValidatorAdmin : IValidator<Admin>{
    public void Validate(Admin e) {
        string err = "";

        if (e.Id < 0) {
            err += "Id admin invalid! ";
        }

        if (e.username == "") {
            err += "Username-ul adminului este invalid! ";
        }
        
        if (e.password == "") {
            err += "Parola admin invalid! ";
        }

        if (err != "") {
            throw new ValidationException(err);
        }
    }
}