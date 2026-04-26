using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmElevi : Form
    {
        private List<Elev> eleviList;

        public FrmElevi()
        {
            InitializeComponent();
            LoadElevi();
        }

        private void LoadElevi()
        {
            try
            {
                eleviList = ElevManager.GetAll();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea elevilor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewElevi.DataSource = eleviList;
            AdjustColumns();
        }

        private void AdjustColumns()
        {
            if (dataGridViewElevi.Columns.Count > 0)
            {
                dataGridViewElevi.Columns["IDElev"].Width = 50;
                dataGridViewElevi.Columns["Nume"].Width = 100;
                dataGridViewElevi.Columns["Prenume"].Width = 100;
                dataGridViewElevi.Columns["Clasa"].Width = 60;
            }
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaElev frm = new FrmAdaugaElev();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadElevi();
                Mesaje.Succes("Elevul a fost adăugat cu succes!");
            }
        }

        private void BtnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewElevi.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un elev!");
                return;
            }

            Elev elev = (Elev)dataGridViewElevi.SelectedRows[0].DataBoundItem;
            FrmAdaugaElev frm = new FrmAdaugaElev(elev);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadElevi();
                Mesaje.Succes("Elevul a fost actualizat cu succes!");
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewElevi.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un elev!");
                return;
            }

            if (Mesaje.ConfirmareSt() == DialogResult.Yes)
            {
                Elev elev = (Elev)dataGridViewElevi.SelectedRows[0].DataBoundItem;
                try
                {
                    if (ElevManager.Delete(elev.IDElev))
                    {
                        LoadElevi();
                        Mesaje.Succes("Elevul a fost șters cu succes!");
                    }
                    else
                        Mesaje.Eroare("Nu s-a putut șterge elevul!");
                }
                catch (Exception ex)
                {
                    Mesaje.Eroare(ex.Message);
                }
            }
        }

        private void TxtCauta_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCauta.Text))
            {
                RefreshGrid();
            }
            else
            {
                eleviList = ElevManager.Search(txtCauta.Text);
                RefreshGrid();
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewElevi = new DataGridView();
            this.btnAdauga = new Button();
            this.btnEditeaza = new Button();
            this.btnSterge = new Button();
            this.txtCauta = new TextBox();
            this.lblCauta = new Label();
            this.btnImprumuri = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewElevi)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewElevi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewElevi.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewElevi.Name = "dataGridViewElevi";
            this.dataGridViewElevi.ReadOnly = true;
            this.dataGridViewElevi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewElevi.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewElevi.TabIndex = 0;

            this.txtCauta.Location = new System.Drawing.Point(100, 20);
            this.txtCauta.Name = "txtCauta";
            this.txtCauta.Size = new System.Drawing.Size(250, 20);
            this.txtCauta.TabIndex = 1;
            this.txtCauta.TextChanged += new EventHandler(this.TxtCauta_TextChanged);

            this.lblCauta.AutoSize = true;
            this.lblCauta.Location = new System.Drawing.Point(12, 23);
            this.lblCauta.Name = "lblCauta";
            this.lblCauta.Size = new System.Drawing.Size(65, 13);
            this.lblCauta.TabIndex = 2;
            this.lblCauta.Text = "Cauta elev:";

            this.btnAdauga.Location = new System.Drawing.Point(12, 410);
            this.btnAdauga.Name = "btnAdauga";
            this.btnAdauga.Size = new System.Drawing.Size(75, 23);
            this.btnAdauga.TabIndex = 3;
            this.btnAdauga.Text = "Adauga";
            this.btnAdauga.UseVisualStyleBackColor = true;
            this.btnAdauga.Click += new EventHandler(this.BtnAdauga_Click);

            this.btnEditeaza.Location = new System.Drawing.Point(93, 410);
            this.btnEditeaza.Name = "btnEditeaza";
            this.btnEditeaza.Size = new System.Drawing.Size(75, 23);
            this.btnEditeaza.TabIndex = 4;
            this.btnEditeaza.Text = "Editeaza";
            this.btnEditeaza.UseVisualStyleBackColor = true;
            this.btnEditeaza.Click += new EventHandler(this.BtnEditeaza_Click);

            this.btnSterge.Location = new System.Drawing.Point(174, 410);
            this.btnSterge.Name = "btnSterge";
            this.btnSterge.Size = new System.Drawing.Size(75, 23);
            this.btnSterge.TabIndex = 5;
            this.btnSterge.Text = "Sterge";
            this.btnSterge.UseVisualStyleBackColor = true;
            this.btnSterge.Click += new EventHandler(this.BtnSterge_Click);

            this.btnImprumuri.Location = new System.Drawing.Point(255, 410);
            this.btnImprumuri.Name = "btnImprumuri";
            this.btnImprumuri.Size = new System.Drawing.Size(100, 23);
            this.btnImprumuri.TabIndex = 6;
            this.btnImprumuri.Text = "Imprumturi";
            this.btnImprumuri.UseVisualStyleBackColor = true;
            this.btnImprumuri.Click += new EventHandler(this.BtnImprumuri_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.btnImprumuri);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnEditeaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblCauta);
            this.Controls.Add(this.txtCauta);
            this.Controls.Add(this.dataGridViewElevi);
            this.Name = "FrmElevi";
            this.Text = "Gestionare Elevi";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewElevi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnImprumuri_Click(object sender, EventArgs e)
        {
            if (dataGridViewElevi.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un elev!");
                return;
            }

            Elev elev = (Elev)dataGridViewElevi.SelectedRows[0].DataBoundItem;
            FrmImprumuri frm = new FrmImprumturi(elev.IDElev);
            frm.ShowDialog();
        }

        private DataGridView dataGridViewElevi;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private TextBox txtCauta;
        private Label lblCauta;
        private Button btnImprumuri;
    }
}