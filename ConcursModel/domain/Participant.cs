namespace ConcursModel.domain; 

public class Participant : Entity<int> {
    public string nume { get; set; }
    public int varsta { get; set; }
    public int[] probe { get; set; }

    public Participant(int id, string nume, int varsta) : base(id) {
        this.nume = nume;
        this.varsta = varsta;
    }

    public Participant(int id, string nume, int varsta, int[] probe) : base(id) {
        this.nume = nume;
        this.varsta = varsta;
        this.probe = probe;
    }
    
    public override string ToString() {
        string fancy = "";
        for (int i = 0; i < probe.Length; ++i) {
            fancy += probe[i].ToString();
            if (i != probe.Length - 1) {
                fancy += ", ";
            }
        }
        return base.ToString() + " " + nume + " " + varsta + " {" + fancy + "}";
        
    }
}