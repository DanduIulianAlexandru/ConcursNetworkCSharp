using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConcursClient;
using ConcursModel.domain;
using ConcursServices;

namespace ConcursForms {
public partial class Form1 : Form {
    private ConcursClientCtrl ctrl;
    public Form1(ConcursClientCtrl ctrl) {
        InitializeComponent();
        this.ctrl = ctrl;
    }

    private void Form1_Load(object sender, EventArgs e) {
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        errorLabel.Text = "";
    }

    private void loginButtom_Click(object sender, EventArgs e) {
        string username = usernameField.Text;
        string password = passwordField.Text;

        try {
            ctrl.login(username, password);
            Form2 concursWindow = new Form2(ctrl);
            concursWindow.Text = "App for admin " + username;
            concursWindow.Show();
            Hide();
        }
        catch (ConcursException er) {
            usernameField.Clear();
            passwordField.Clear();
            errorLabel.Text = er.Message;
        }
    }
    
}
}