using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaElev : Form
    {
        private Elev elev;

        public FrmAdaugaElev(Elev elevEdit = null)
        {
            InitializeComponent();
            ApplyTheme();
            elev = elevEdit ?? new Elev();
            LoadClase();
            if (elevEdit != null)
            {
                PopulateForm();
                this.Text = "Editare Elev";
            }
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleButtonPrimary(btnSalveaza);
            ThemeHelper.StyleButtonNeutral(btnAnuleaza);
        }

        private void LoadClase()
        {
            cmbClasa.DataSource = Constante.CLASE;
        }

        private void PopulateForm()
        {
            txtNume.Text = elev.Nume;
            txtPrenume.Text = elev.Prenume;
            cmbClasa.SelectedItem = elev.Clasa;
            txtEmail.Text = elev.Email;
            txtTelefon.Text = elev.Telefon;
            cmbStatus.SelectedItem = elev.Status;
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            elev.Nume = txtNume.Text.Trim();
            elev.Prenume = txtPrenume.Text.Trim();
            elev.Clasa = cmbClasa.SelectedItem.ToString();
            elev.Email = txtEmail.Text.Trim();
            elev.Telefon = txtTelefon.Text.Trim();
            elev.Status = cmbStatus.SelectedItem.ToString();

            try
            {
                bool succes;
                if (elev.IDElev == 0)
                    succes = ElevManager.Insert(elev);
                else
                    succes = ElevManager.Update(elev);

                if (succes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    Mesaje.Eroare("Operația nu a reușit!");
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
            if (Validari.EsteGol(txtNume.Text))
            {
                Mesaje.Validare("Introduceți numele elevului!");
                txtNume.Focus();
                return false;
            }

            if (Validari.EsteGol(txtPrenume.Text))
            {
                Mesaje.Validare("Introduceți prenumele elevului!");
                txtPrenume.Focus();
                return false;
            }

            if (cmbClasa.SelectedItem == null)
            {
                Mesaje.Validare("Selectați clasa elevului!");
                return false;
            }

            if (!string.IsNullOrEmpty(txtEmail.Text) && !Validari.ValidareEmail(txtEmail.Text))
            {
                Mesaje.Validare("Email invalid!");
                return false;
            }

            if (!string.IsNullOrEmpty(txtTelefon.Text) && !Validari.ValidareTelefon(txtTelefon.Text))
            {
                Mesaje.Validare("Număr de telefon invalid!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblNume = new Label();
            this.txtNume = new TextBox();
            this.lblPrenume = new Label();
            this.txtPrenume = new TextBox();
            this.lblClasa = new Label();
            this.cmbClasa = new ComboBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblTelefon = new Label();
            this.txtTelefon = new TextBox();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
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

            this.lblPrenume.AutoSize = true;
            this.lblPrenume.Location = new System.Drawing.Point(12, 45);
            this.lblPrenume.Name = "lblPrenume";
            this.lblPrenume.Size = new System.Drawing.Size(59, 13);
            this.lblPrenume.TabIndex = 2;
            this.lblPrenume.Text = "Prenume:";

            this.txtPrenume.Location = new System.Drawing.Point(100, 42);
            this.txtPrenume.Name = "txtPrenume";
            this.txtPrenume.Size = new System.Drawing.Size(250, 20);
            this.txtPrenume.TabIndex = 3;

            this.lblClasa.AutoSize = true;
            this.lblClasa.Location = new System.Drawing.Point(12, 75);
            this.lblClasa.Name = "lblClasa";
            this.lblClasa.Size = new System.Drawing.Size(39, 13);
            this.lblClasa.TabIndex = 4;
            this.lblClasa.Text = "Clasa:";

            this.cmbClasa.FormattingEnabled = true;
            this.cmbClasa.Location = new System.Drawing.Point(100, 72);
            this.cmbClasa.Name = "cmbClasa";
            this.cmbClasa.Size = new System.Drawing.Size(100, 21);
            this.cmbClasa.TabIndex = 5;

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

            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(12, 135);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(50, 13);
            this.lblTelefon.TabIndex = 8;
            this.lblTelefon.Text = "Telefon:";

            this.txtTelefon.Location = new System.Drawing.Point(100, 132);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(250, 20);
            this.txtTelefon.TabIndex = 9;

            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 165);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:";

            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            Constante.StareElev.ACTIV,
            Constante.StareElev.INACTIV,
            Constante.StareElev.SUSPENDAT});
            this.cmbStatus.Location = new System.Drawing.Point(100, 162);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 21);
            this.cmbStatus.TabIndex = 11;
            this.cmbStatus.SelectedIndex = 0;

            this.btnSalveaza.Location = new System.Drawing.Point(100, 200);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 12;
            this.btnSalveaza.Text = "Salveaza";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(181, 200);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 13;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 240);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtTelefon);
            this.Controls.Add(this.lblTelefon);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.cmbClasa);
            this.Controls.Add(this.lblClasa);
            this.Controls.Add(this.txtPrenume);
            this.Controls.Add(this.lblPrenume);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Name = "FrmAdaugaElev";
            this.Text = "Adauga Elev";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblNume;
        private TextBox txtNume;
        private Label lblPrenume;
        private TextBox txtPrenume;
        private Label lblClasa;
        private ComboBox cmbClasa;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblTelefon;
        private TextBox txtTelefon;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}