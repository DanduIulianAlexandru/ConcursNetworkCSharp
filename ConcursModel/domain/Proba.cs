namespace ConcursModel.domain; 

public class Proba : Entity<int> {
    public Distanta distanta { get; set; }
    public Stil stil { get; set; }
    public int nrParticipanti { get; set; }

    public Proba(int id, Distanta distanta, Stil stil) : base(id) {
        this.distanta = distanta;
        this.stil = stil;
    }

    public override string ToString() {
        return base.ToString() + " " + distanta + " " + stil + " " + nrParticipanti;
    }
}