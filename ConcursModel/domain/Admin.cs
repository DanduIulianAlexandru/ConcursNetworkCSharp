namespace ConcursModel.domain; 

public class Admin : Entity<int> {
    public string username { get; set; }
    public string password { get; set; }

    public Admin(int id, string username, string password) : base(id) {
        this.username = username;
        this.password = password;
    }

    public override string ToString() {
        return base.ToString() + " " + username + " " + password;
    }
}