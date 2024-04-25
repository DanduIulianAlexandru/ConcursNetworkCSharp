namespace ConcursForms {
partial class Form1 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.labelLogin = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.usernameField = new System.Windows.Forms.TextBox();
        this.passwordField = new System.Windows.Forms.TextBox();
        this.loginButtom = new System.Windows.Forms.Button();
        this.errorLabel = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // labelLogin
        // 
        this.labelLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelLogin.Location = new System.Drawing.Point(187, 9);
        this.labelLogin.Name = "labelLogin";
        this.labelLogin.Size = new System.Drawing.Size(450, 75);
        this.labelLogin.TabIndex = 0;
        this.labelLogin.Text = "Login for Admins";
        this.labelLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(210, 139);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(112, 28);
        this.label1.TabIndex = 1;
        this.label1.Text = "Username:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(210, 192);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(112, 25);
        this.label2.TabIndex = 2;
        this.label2.Text = "Password:";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // usernameField
        // 
        this.usernameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.usernameField.Location = new System.Drawing.Point(342, 139);
        this.usernameField.Name = "usernameField";
        this.usernameField.Size = new System.Drawing.Size(243, 26);
        this.usernameField.TabIndex = 3;
        // 
        // passwordField
        // 
        this.passwordField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.passwordField.Location = new System.Drawing.Point(342, 192);
        this.passwordField.Name = "passwordField";
        this.passwordField.PasswordChar = '*';
        this.passwordField.Size = new System.Drawing.Size(243, 26);
        this.passwordField.TabIndex = 4;
        // 
        // loginButtom
        // 
        this.loginButtom.Location = new System.Drawing.Point(314, 278);
        this.loginButtom.Name = "loginButtom";
        this.loginButtom.Size = new System.Drawing.Size(145, 50);
        this.loginButtom.TabIndex = 5;
        this.loginButtom.Text = "Login";
        this.loginButtom.UseVisualStyleBackColor = true;
        this.loginButtom.Click += new System.EventHandler(this.loginButtom_Click);
        // 
        // errorLabel
        // 
        this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.errorLabel.ForeColor = System.Drawing.Color.Red;
        this.errorLabel.Location = new System.Drawing.Point(228, 231);
        this.errorLabel.Name = "errorLabel";
        this.errorLabel.Size = new System.Drawing.Size(211, 24);
        this.errorLabel.TabIndex = 6;
        this.errorLabel.Text = "*error";
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.errorLabel);
        this.Controls.Add(this.loginButtom);
        this.Controls.Add(this.passwordField);
        this.Controls.Add(this.usernameField);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.labelLogin);
        this.Name = "Form1";
        this.Text = "Login";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label errorLabel;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox usernameField;
    private System.Windows.Forms.TextBox passwordField;
    private System.Windows.Forms.Button loginButtom;

    private System.Windows.Forms.Label labelLogin;

    #endregion
}
}