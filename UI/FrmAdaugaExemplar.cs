using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmAdaugaExemplar : Form
    {
        private Exemplar exemplar;
        private int carteId;

        public FrmAdaugaExemplar(int idCarte, Exemplar exemplarEdit = null)
        {
            InitializeComponent();
            ApplyTheme();
            carteId = idCarte;
            exemplar = exemplarEdit ?? new Exemplar { IDCarte = idCarte, DataAchizitiei = DateTime.Today };
            LoadStari();
            if (exemplarEdit != null)
            {
                PopulateForm();
                this.Text = "Editare Exemplar";
            }
        }

        private void ApplyTheme()
        {
            ThemeHelper.ApplyTheme(this);
            ThemeHelper.StyleButtonPrimary(btnSalveaza);
            ThemeHelper.StyleButtonNeutral(btnAnuleaza);
        }

        private void LoadStari()
        {
            cmbStare.Items.Add(Constante.StareExemplar.BUNA);
            cmbStare.Items.Add(Constante.StareExemplar.DETERIORATA);
            cmbStare.Items.Add(Constante.StareExemplar.PIERDUTA);
            cmbStare.SelectedIndex = 0;
        }

        private void PopulateForm()
        {
            txtCod.Text = exemplar.CoduInventar;
            cmbStare.SelectedItem = exemplar.StareExemplar;
            dateTimePickerData.Value = exemplar.DataAchizitiei;
            if (exemplar.Pret.HasValue)
                nudPret.Value = exemplar.Pret.Value;
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (!Valideaza())
                return;

            exemplar.CoduInventar = txtCod.Text.Trim();
            exemplar.StareExemplar = cmbStare.SelectedItem.ToString();
            exemplar.DataAchizitiei = dateTimePickerData.Value;
            exemplar.Pret = nudPret.Value == 0 ? null : (decimal?)nudPret.Value;

            try
            {
                bool succes;
                if (exemplar.IDExemplar == 0)
                    succes = ExemplarManager.Insert(exemplar);
                else
                    succes = ExemplarManager.Update(exemplar);

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
            if (Validari.EsteGol(txtCod.Text))
            {
                Mesaje.Validare("Introduceți codul inventar!");
                txtCod.Focus();
                return false;
            }

            if (cmbStare.SelectedItem == null)
            {
                Mesaje.Validare("Selectați starea exemplarului!");
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            this.lblCod = new Label();
            this.txtCod = new TextBox();
            this.lblStare = new Label();
            this.cmbStare = new ComboBox();
            this.lblData = new Label();
            this.dateTimePickerData = new DateTimePicker();
            this.lblPret = new Label();
            this.nudPret = new NumericUpDown();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPret)).BeginInit();
            this.SuspendLayout();

            this.lblCod.AutoSize = true;
            this.lblCod.Location = new System.Drawing.Point(12, 15);
            this.lblCod.Name = "lblCod";
            this.lblCod.Size = new System.Drawing.Size(93, 13);
            this.lblCod.TabIndex = 0;
            this.lblCod.Text = "Cod Inventar:";

            this.txtCod.Location = new System.Drawing.Point(120, 12);
            this.txtCod.Name = "txtCod";
            this.txtCod.Size = new System.Drawing.Size(200, 20);
            this.txtCod.TabIndex = 1;

            this.lblStare.AutoSize = true;
            this.lblStare.Location = new System.Drawing.Point(12, 45);
            this.lblStare.Name = "lblStare";
            this.lblStare.Size = new System.Drawing.Size(102, 13);
            this.lblStare.TabIndex = 2;
            this.lblStare.Text = "Stare Exemplar:";

            this.cmbStare.FormattingEnabled = true;
            this.cmbStare.Location = new System.Drawing.Point(120, 42);
            this.cmbStare.Name = "cmbStare";
            this.cmbStare.Size = new System.Drawing.Size(200, 21);
            this.cmbStare.TabIndex = 3;

            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(12, 75);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(93, 13);
            this.lblData.TabIndex = 4;
            this.lblData.Text = "Data Achizitie:";

            this.dateTimePickerData.Location = new System.Drawing.Point(120, 72);
            this.dateTimePickerData.Name = "dateTimePickerData";
            this.dateTimePickerData.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerData.TabIndex = 5;

            this.lblPret.AutoSize = true;
            this.lblPret.Location = new System.Drawing.Point(12, 105);
            this.lblPret.Name = "lblPret";
            this.lblPret.Size = new System.Drawing.Size(37, 13);
            this.lblPret.TabIndex = 6;
            this.lblPret.Text = "Preț:";

            this.nudPret.DecimalPlaces = 2;
            this.nudPret.Location = new System.Drawing.Point(120, 102);
            this.nudPret.Maximum = new decimal(new int[] { 999999, 0, 0, 131072 });
            this.nudPret.Name = "nudPret";
            this.nudPret.Size = new System.Drawing.Size(100, 20);
            this.nudPret.TabIndex = 7;

            this.btnSalveaza.Location = new System.Drawing.Point(120, 140);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 23);
            this.btnSalveaza.TabIndex = 8;
            this.btnSalveaza.Text = "Salveaza";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new EventHandler(this.BtnSalveaza_Click);

            this.btnAnuleaza.Location = new System.Drawing.Point(201, 140);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 23);
            this.btnAnuleaza.TabIndex = 9;
            this.btnAnuleaza.Text = "Anuleaza";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new EventHandler(this.BtnAnuleaza_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 180);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.nudPret);
            this.Controls.Add(this.lblPret);
            this.Controls.Add(this.dateTimePickerData);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.cmbStare);
            this.Controls.Add(this.lblStare);
            this.Controls.Add(this.txtCod);
            this.Controls.Add(this.lblCod);
            this.Name = "FrmAdaugaExemplar";
            this.Text = "Adauga Exemplar";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.nudPret)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblCod;
        private TextBox txtCod;
        private Label lblStare;
        private ComboBox cmbStare;
        private Label lblData;
        private DateTimePicker dateTimePickerData;
        private Label lblPret;
        private NumericUpDown nudPret;
        private Button btnSalveaza;
        private Button btnAnuleaza;
    }
}