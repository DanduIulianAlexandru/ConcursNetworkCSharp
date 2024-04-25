using System;

namespace ConcursClient; 

public enum ConcursEvent {
    ADDED_PARTICIPANT
}

public class ConcursEventArgs {
    private readonly ConcursEvent concursEvent;
    private readonly Object data;

    public ConcursEventArgs(ConcursEvent c, Object d) {
        concursEvent = c;
        data = d;
    }

    public ConcursEvent ConcursEvent {
        get{ return concursEvent; }
    }

    public Object Data {
        get{ return data; }
    }
}