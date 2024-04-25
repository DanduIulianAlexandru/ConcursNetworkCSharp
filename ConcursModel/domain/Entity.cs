namespace ConcursModel.domain; 

public class Entity<TID> {
    public TID Id {
        get;
        set;
    }

    public Entity(TID id) {
        this.Id = id;
    }
}