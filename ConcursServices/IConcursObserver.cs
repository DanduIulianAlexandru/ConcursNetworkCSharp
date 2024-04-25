using ConcursModel.domain;

namespace ConcursServices; 

public interface IConcursObserver {
    void participantAdded(Participant participant);
}