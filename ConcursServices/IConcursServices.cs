using System;
using System.Collections.Generic;
using ConcursModel.domain;

namespace ConcursServices; 

public interface IConcursServices {
    Admin login(Admin admin, IConcursObserver client);
    void logout(Admin admin, IConcursObserver client);
    void addParticipant(Participant participant);
    List<Proba> getProbe();
    List<Participant> getParticipanti();
    Proba getProba(int id);
    List<Participant> getParticipantiByString(String searchString);
}