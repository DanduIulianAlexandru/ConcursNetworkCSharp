using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using ConcursClient;
using ConcursModel.domain;
using ConcursModel.domain.validator;
using ConcursServices;

namespace ConcursForms; 

public partial class Form2 : Form {
    private readonly ConcursClientCtrl ctrl;
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase
        .GetCurrentMethod()?.DeclaringType);
    
    public Form2(ConcursClientCtrl ctrl) {
        InitializeComponent();
        this.ctrl = ctrl;
        this.ctrl.updateEvent += userUpdate;
        //FormClosed += Form2_FormClosed;
    }
    
    private void Form2_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }

    private void Form2_Load(object sender, EventArgs e) {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        errorLabel.Text = "";
        LoadProbe();
        LoadParticipanti();
    }

    public void userUpdate(object sender, ConcursEventArgs e) {
        if (e.ConcursEvent == ConcursEvent.ADDED_PARTICIPANT) {
            probeTable.BeginInvoke((Action)LoadProbe);
            participantiTable.BeginInvoke((Action)LoadParticipanti);
            Console.WriteLine("Notified admins");
        }
    }

    private void LoadProbe() {
        DataTable probe = new DataTable();
        probe.Columns.Add("ID Proba", typeof(int));
        probe.Columns.Add("Distanta", typeof(string));
        probe.Columns.Add("Stil", typeof(string));
        probe.Columns.Add("Nr", typeof(int));

        List<Proba> probeList = ctrl.getProbe().OrderBy(q => q.Id).ToList();

        foreach (Proba proba in probeList) {
            probe.Rows.Add(proba.Id, proba.distanta.ToString(), proba.stil.ToString(), proba.nrParticipanti);
        }
        
        probeTable.DataSource = probe;
        probeTable.Columns[0].Width = 80;
        probeTable.Columns[1].Width = 50;
        probeTable.Columns[2].Width = 70;
        probeTable.Columns[3].Width = 50;
    }

    private string convertToString(int[] probe) {
        string s = "";
        int c = 0;
        foreach (int p in probe) {
            c++;
            s += p.ToString();
            if (c != probe.Length) {
                s += " ";
            }
        }

        return s;
    }

    private void LoadParticipanti() {
        DataTable participanti = new DataTable();
        participanti.Columns.Add("Nume", typeof(string));
        participanti.Columns.Add("Varsta", typeof(int));
        participanti.Columns.Add("Probe", typeof(string));

        List<Participant> participantiList = ctrl.getParticipanti().OrderBy(q => q.nume).ToList();


        foreach (Participant participant in participantiList) {
            participanti.Rows.Add(participant.nume, participant.varsta, convertToString(participant.probe));
        }
        
        participantiTable.DataSource = participanti;
    }
    
    private void LoadParticipantiWithList(List<Participant> list) {
        DataTable participanti = new DataTable();
        participanti.Columns.Add("Nume", typeof(string));
        participanti.Columns.Add("Varsta", typeof(int));
        participanti.Columns.Add("Probe", typeof(string));

        foreach (Participant participant in list) {
            participanti.Rows.Add(participant.nume, participant.varsta, convertToString(participant.probe));
        }
        
        participantiTable.DataSource = participanti;
    }

    private void logoutButton_Click(object sender, EventArgs e) {
        ctrl.logout();
        ctrl.updateEvent -= userUpdate;
        Application.Exit();
    }
    
    public static bool IsNumeric(string strNum) {
        if (strNum == null) {
            return false;
        }
        try {
            int.Parse(strNum);
        }
        catch (FormatException) {
            return false;
        }
        return true;
    }

    private void inscrieButton_Click(object sender, EventArgs e) {
        string nume = numeField.Text;
        string varsta = varstaField.Text;
        string probeS = probeField.Text;

        if (nume.Equals("") || varsta.Equals("") || probeS.Equals("")) {
            errorLabel.Text = "Campurile nu pot fi empty";
            return;
        }
        
        if (!IsNumeric(varsta)){
            errorLabel.Text = "Varsta trebuie sa fie un numar";
            varstaField.Clear();
            return;
        }
        
        int nrProbe = 0;
        string[] parts = probeS.Split('\u0020');
        int[] probe = new int[parts.Length];
        foreach (string part in parts)
        {
            if (!IsNumeric(part))
            {
                Console.WriteLine(part);
                errorLabel.Text = "Formatul pentru probe nu e corect";
                probeField.Text = "";
                return;
            }
            if (ctrl.getProba(int.Parse(part)) == null)
            {
                errorLabel.Text = "Nu exista proba cu id-ul " + part;
                probeField.Text = "";
                return;
            }
            probe[nrProbe] = int.Parse(part);
            ++nrProbe;
        }

        try {
            ctrl.addParticipant(nume, int.Parse(varsta), probe);

            numeField.Text = "";
            varstaField.Text = "";
            probeField.Text = "";
            errorLabel.Text = "Participant adaugat cu succes";

        }
        catch (ConcursException ex) {
            errorLabel.Text = ex.ToString().Substring(31);
            logger.Error(ex.ToString());
            numeField.Text = "";
            varstaField.Text = "";
            probeField.Text = "";
        }
    }

    private void cautaButton_Click(object sender, EventArgs e) {
        string searchString = cautareField.Text;
        if (searchString.Equals("")){
            LoadParticipanti();
        }
        LoadParticipantiWithList(ctrl.getParticipantiByString(searchString));
    }
}