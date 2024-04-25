using System;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using ConcursModel.domain;
using ConcursServices;

namespace ConcursClient; 

public class ConcursClientCtrl : IConcursObserver{
    public event EventHandler<ConcursEventArgs> updateEvent;
    private readonly IConcursServices server;
    private Admin loggedAdmin;

    public ConcursClientCtrl(IConcursServices server) {
        this.server = server;
        loggedAdmin = null;
    }

    public void login(string username, string password) {
        
        Admin adminL = new Admin(0, username, password);
        loggedAdmin = server.login(adminL, this);
        Console.WriteLine("Login Succeded...");
    }

    public void participantAdded(Participant participant) {
        ConcursEventArgs concursEventArgs = new ConcursEventArgs(ConcursEvent.ADDED_PARTICIPANT, participant);
        OnUserEvent(concursEventArgs);
    }

    public List<Participant> getParticipanti() {
        return server.getParticipanti();
    }

    public List<Proba> getProbe() {
        return server.getProbe();
    }

    public List<Participant> getParticipantiByString(string searchWord) {
        return server.getParticipantiByString(searchWord);
    }

    public void addParticipant(string nume, int varsta, int[] probe) {
        Participant participant = new Participant(0, nume, varsta, probe);
        server.addParticipant(participant);
    }

    
    public Proba getProba(int id) {
        return server.getProba(id);
    }

    public void logout() {
        if (loggedAdmin == null) {
            Console.WriteLine("Admin in not logged in");
            return;
        }
        server.logout(loggedAdmin, this);
        loggedAdmin = null;
    }

    protected virtual void OnUserEvent(ConcursEventArgs e) {
        if (updateEvent == null) {
            return;
        }

        updateEvent(this, e);
        Console.WriteLine("Update Event called");
    }
}