using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using ConcursModel.domain;
using ConcursModel.domain.validator;
using ConcursNetwork.dto;
using ConcursPersistence.repository;
using ConcursServices;

namespace ConcursService; 

public class ConcursServiceImpl : IConcursServices {
    private DBRepositoryAdmin repositoryAdmin;
    private DBRepositoryParticipant repositoryParticipant;
    private DBRepositoryProba repositoryProba;
    private readonly IDictionary <int, IConcursObserver> loggedAdmins;

    public ConcursServiceImpl(DBRepositoryAdmin r1, DBRepositoryParticipant r2, DBRepositoryProba r3) {
        repositoryAdmin = r1;
        repositoryParticipant = r2;
        repositoryProba = r3;
        loggedAdmins = new Dictionary<int, IConcursObserver>();
    }
    
    public Admin login(Admin admin, IConcursObserver client) {
        
        Admin adminL = repositoryAdmin.FindBy(admin.username, admin.password);
        
        
        if (adminL != null) {
            
            if (loggedAdmins.ContainsKey(adminL.Id)) {
                
                throw new ConcursException("Admin already logged in.");
            }
            
            loggedAdmins.Add(adminL.Id, client);
            
           
            return adminL;
        }

        throw new ConcursException("Authentication failed.");
    }

    public void logout(Admin admin, IConcursObserver client) {
        Boolean removed = loggedAdmins.Remove(admin.Id);
        if (!removed) {
            throw new ConcursException(admin.username + " is not logged in");
        }
    }

    public void addParticipant(Participant participant) {
        if (participant == null) {
            throw new ConcursException("Participant is null");
        }

        try {
            repositoryParticipant.Save(participant);
            int[] probe = participant.probe;
            foreach (int id in probe) {
                repositoryProba.incrementNr(id);
            }
            notifyParticipantAdded(participant);
        }
        catch (ValidationException e) {
            throw new ConcursException(e.Message);
        }
    }

    private void notifyParticipantAdded(Participant participant) {
        Console.WriteLine("Notify participant added: " + participant);

        foreach (var client in loggedAdmins.Values) {
            Task.Run(() => client.participantAdded(participant));
        }
    }

    public List<Proba> getProbe() {
        return repositoryProba.FindAll().ToList();
    }

    public List<Participant> getParticipanti() {
        return repositoryParticipant.FindAll().ToList();
    }

    public Proba getProba(int id) {
        return repositoryProba.FindOne(id);
    }
    
    private static bool IsNumeric(string strNum) {
        try {
            double.Parse(strNum);
        } catch (FormatException) {
            return false;
        }
        return true;
    }

    public List<Participant> getParticipantiByString(string searchString) {
        if (IsNumeric(searchString)) {
            return repositoryParticipant.FindAllByAge(int.Parse(searchString)).ToList();
        }
        return repositoryParticipant.FindAllByName(searchString).ToList();
    }
}