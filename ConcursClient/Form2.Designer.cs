using System.ComponentModel;

namespace ConcursForms; 

partial class Form2 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.label1 = new System.Windows.Forms.Label();
        this.probeTable = new System.Windows.Forms.DataGridView();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.numeField = new System.Windows.Forms.TextBox();
        this.varstaField = new System.Windows.Forms.TextBox();
        this.probeField = new System.Windows.Forms.TextBox();
        this.errorLabel = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.cautareField = new System.Windows.Forms.TextBox();
        this.cautaButton = new System.Windows.Forms.Button();
        this.participantiTable = new System.Windows.Forms.DataGridView();
        this.logoutButton = new System.Windows.Forms.Button();
        this.inscrieButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.probeTable)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.participantiTable)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(154, 28);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(231, 47);
        this.label1.TabIndex = 0;
        this.label1.Text = "Probe";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // probeTable
        // 
        this.probeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.probeTable.Location = new System.Drawing.Point(38, 93);
        this.probeTable.Name = "probeTable";
        this.probeTable.RowTemplate.Height = 24;
        this.probeTable.Size = new System.Drawing.Size(396, 541);
        this.probeTable.TabIndex = 1;
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(586, 28);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(221, 30);
        this.label2.TabIndex = 2;
        this.label2.Text = "Inscrie Participant";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(479, 108);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(83, 26);
        this.label3.TabIndex = 3;
        this.label3.Text = "Nume:";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(474, 150);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(88, 25);
        this.label4.TabIndex = 4;
        this.label4.Text = "Varsta:";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(479, 193);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(88, 23);
        this.label5.TabIndex = 5;
        this.label5.Text = "Probe:";
        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // numeField
        // 
        this.numeField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.numeField.Location = new System.Drawing.Point(600, 108);
        this.numeField.Name = "numeField";
        this.numeField.Size = new System.Drawing.Size(206, 26);
        this.numeField.TabIndex = 6;
        // 
        // varstaField
        // 
        this.varstaField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.varstaField.Location = new System.Drawing.Point(600, 151);
        this.varstaField.Name = "varstaField";
        this.varstaField.Size = new System.Drawing.Size(206, 26);
        this.varstaField.TabIndex = 7;
        // 
        // probeField
        // 
        this.probeField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.probeField.Location = new System.Drawing.Point(599, 191);
        this.probeField.Name = "probeField";
        this.probeField.Size = new System.Drawing.Size(206, 26);
        this.probeField.TabIndex = 8;
        // 
        // errorLabel
        // 
        this.errorLabel.ForeColor = System.Drawing.Color.Red;
        this.errorLabel.Location = new System.Drawing.Point(492, 239);
        this.errorLabel.Name = "errorLabel";
        this.errorLabel.Size = new System.Drawing.Size(275, 24);
        this.errorLabel.TabIndex = 9;
        this.errorLabel.Text = "*error";
        // 
        // label6
        // 
        this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.Location = new System.Drawing.Point(539, 220);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(175, 21);
        this.label6.TabIndex = 10;
        this.label6.Text = "Sub forma 1 2 3 4";
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.Location = new System.Drawing.Point(586, 269);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(214, 34);
        this.label7.TabIndex = 11;
        this.label7.Text = "Cautare";
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // cautareField
        // 
        this.cautareField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cautareField.Location = new System.Drawing.Point(558, 329);
        this.cautareField.Name = "cautareField";
        this.cautareField.Size = new System.Drawing.Size(219, 26);
        this.cautareField.TabIndex = 12;
        // 
        // cautaButton
        // 
        this.cautaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.cautaButton.Location = new System.Drawing.Point(806, 326);
        this.cautaButton.Name = "cautaButton";
        this.cautaButton.Size = new System.Drawing.Size(128, 29);
        this.cautaButton.TabIndex = 13;
        this.cautaButton.Text = "Cauta";
        this.cautaButton.UseVisualStyleBackColor = true;
        this.cautaButton.Click += new System.EventHandler(this.cautaButton_Click);
        // 
        // participantiTable
        // 
        this.participantiTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.participantiTable.Location = new System.Drawing.Point(492, 389);
        this.participantiTable.Name = "participantiTable";
        this.participantiTable.RowTemplate.Height = 24;
        this.participantiTable.Size = new System.Drawing.Size(486, 245);
        this.participantiTable.TabIndex = 14;
        // 
        // logoutButton
        // 
        this.logoutButton.Location = new System.Drawing.Point(865, 682);
        this.logoutButton.Name = "logoutButton";
        this.logoutButton.Size = new System.Drawing.Size(131, 33);
        this.logoutButton.TabIndex = 15;
        this.logoutButton.Text = "Log out";
        this.logoutButton.UseVisualStyleBackColor = true;
        this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
        // 
        // inscrieButton
        // 
        this.inscrieButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.inscrieButton.Location = new System.Drawing.Point(839, 146);
        this.inscrieButton.Name = "inscrieButton";
        this.inscrieButton.Size = new System.Drawing.Size(123, 43);
        this.inscrieButton.TabIndex = 16;
        this.inscrieButton.Text = "Inscrie";
        this.inscrieButton.UseVisualStyleBackColor = true;
        this.inscrieButton.Click += new System.EventHandler(this.inscrieButton_Click);
        // 
        // Form2
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1009, 727);
        this.Controls.Add(this.inscrieButton);
        this.Controls.Add(this.logoutButton);
        this.Controls.Add(this.participantiTable);
        this.Controls.Add(this.cautaButton);
        this.Controls.Add(this.cautareField);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.errorLabel);
        this.Controls.Add(this.probeField);
        this.Controls.Add(this.varstaField);
        this.Controls.Add(this.numeField);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.probeTable);
        this.Controls.Add(this.label1);
        this.Name = "Form2";
        this.Text = "App";
        this.Load += new System.EventHandler(this.Form2_Load);
        ((System.ComponentModel.ISupportInitialize)(this.probeTable)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.participantiTable)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button inscrieButton;

    private System.Windows.Forms.Button logoutButton;

    private System.Windows.Forms.DataGridView participantiTable;

    private System.Windows.Forms.TextBox probeField;
    private System.Windows.Forms.Button cautaButton;

    private System.Windows.Forms.Label label7;

    private System.Windows.Forms.Label label6;

    private System.Windows.Forms.TextBox numeField;
    private System.Windows.Forms.TextBox varstaField;
    private System.Windows.Forms.Label errorLabel;

    private System.Windows.Forms.TextBox cautareField;

    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.DataGridView probeTable;

    private System.Windows.Forms.Label label1;

    #endregion
}