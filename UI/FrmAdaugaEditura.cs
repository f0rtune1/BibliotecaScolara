using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaEditura : Form
    {
        private Editura editura;
        private bool isEditMode = false;

        public FrmAdaugaEditura(Editura ed = null)
        {
            InitializeComponent();
            ApplyTheme();
            if (ed != null)
            {
                editura = ed;
                isEditMode = true;
                PopulateForm();
                this.Text = "Modificare Editura";
                btnSalveaza.Text = "Actualizeaza";
            }
            else
            {
                editura = new Editura();
            }
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleButtonPrimary(btnSalveaza);
            ThemeHelper.StyleButtonNeutral(btnAnuleaza);
        }

        private void PopulateForm()
        {
            txtNume.Text = editura.NumeEditura;
            txtAdresa.Text = editura.Adresa;
            txtTelefon.Text = editura.Telefon;
            txtEmail.Text = editura.Email;
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            try
            {
                editura.NumeEditura = txtNume.Text;
                editura.Adresa = txtAdresa.Text;
                editura.Telefon = txtTelefon.Text;
                editura.Email = txtEmail.Text;

                bool result = isEditMode ? 
                    EdituraManager.Update(editura) : 
                    EdituraManager.Insert(editura);

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    Mesaje.Eroare("Nu s-a putut salva editura!");
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare: " + ex.Message);
            }
        }

        private void BtnAnuleaza_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool Valideaza()
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                Mesaje.Validare("Introduceți numele editurii!");
                return false;
            }

            if (txtNume.Text.Length < 3)
            {
                Mesaje.Validare("Numele editurii trebuie să aibă cel puțin 3 caractere!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblNume = new Label();
            this.txtNume = new TextBox();
            this.lblAdresa = new Label();
            this.txtAdresa = new TextBox();
            this.lblTelefon = new Label();
            this.txtTelefon = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            this.SuspendLayout();

            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(12, 15);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(38, 13);
            this.lblNume.TabIndex = 0;
            this.lblNume.Text = "Nume:";

            this.txtNume.Location = new System.Drawing.Point(100, 12);
            this.txtNume.Name = "txtNume";
            this.txtNume.Size = new System.Drawing.Size(250, 20);
            this.txtNume.TabIndex = 1;

            this.lblAdresa.AutoSize = true;
            this.lblAdresa.Location = new System.Drawing.Point(12, 45);
            this.lblAdresa.Name = "lblAdresa";
            this.lblAdresa.Size = new System.Drawing.Size(48, 13);
            this.lblAdresa.TabIndex = 2;
            this.lblAdresa.Text = "Adresa:";

            this.txtAdresa.Location = new System.Drawing.Point(100, 42);
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(250, 20);
            this.txtAdresa.TabIndex = 3;

            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(12, 75);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(53, 13);
            this.lblTelefon.TabIndex = 4;
            this.lblTelefon.Text = "Telefon:";

            this.txtTelefon.Location = new System.Drawing.Point(100, 72);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(250, 20);
            this.txtTelefon.TabIndex = 5;

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 105);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 13);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";

            this.txtEmail.Location = new System.Drawing.Point(100, 102);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 20);
            this.txtEmail.TabIndex = 7;

            this.btnSalveaza.Location = new System.Drawing.Point(100, 140);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 8;
            this.btnSalveaza.Text = "Salveaza";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(181, 140);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 9;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 180);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtTelefon);
            this.Controls.Add(this.lblTelefon);
            this.Controls.Add(this.txtAdresa);
            this.Controls.Add(this.lblAdresa);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Name = "FrmAdaugaEditura";
            this.Text = "Adaugare Editura";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblNume;
        private TextBox txtNume;
        private Label lblAdresa;
        private TextBox txtAdresa;
        private Label lblTelefon;
        private TextBox txtTelefon;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}