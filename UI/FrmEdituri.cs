using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmEdituri : Form
    {
        private List<Editura> edituriList;

        public FrmEdituri()
        {
            InitializeComponent();
            LoadEdituri();
        }

        private void LoadEdituri()
        {
            try
            {
                edituriList = EdituraManager.GetAll();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea editurilor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewEdituri.DataSource = edituriList;
            AdjustColumns();
        }

        private void AdjustColumns()
        {
            if (dataGridViewEdituri.Columns.Count > 0)
            {
                dataGridViewEdituri.Columns["IDEditura"].Width = 50;
                dataGridViewEdituri.Columns["NumeEditura"].Width = 150;
                dataGridViewEdituri.Columns["Adresa"].Width = 200;
                dataGridViewEdituri.Columns["Telefon"].Width = 100;
            }
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaEditura frm = new FrmAdaugaEditura();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadEdituri();
                Mesaje.Succes("Editura a fost adăugată cu succes!");
            }
        }

        private void BtnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewEdituri.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați o editură!");
                return;
            }

            Editura editura = (Editura)dataGridViewEdituri.SelectedRows[0].DataBoundItem;
            FrmAdaugaEditura frm = new FrmAdaugaEditura(editura);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadEdituri();
                Mesaje.Succes("Editura a fost actualizată cu succes!");
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewEdituri.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați o editură!");
                return;
            }

            if (Mesaje.ConfirmareSt() == DialogResult.Yes)
            {
                Editura editura = (Editura)dataGridViewEdituri.SelectedRows[0].DataBoundItem;
                try
                {
                    if (EdituraManager.Delete(editura.IDEditura))
                    {
                        LoadEdituri();
                        Mesaje.Succes("Editura a fost ștearsă cu succes!");
                    }
                    else
                        Mesaje.Eroare("Nu s-a putut șterge editura!");
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
                edituriList = EdituraManager.Search(txtCauta.Text);
                RefreshGrid();
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewEdituri = new DataGridView();
            this.btnAdauga = new Button();
            this.btnEditeaza = new Button();
            this.btnSterge = new Button();
            this.txtCauta = new TextBox();
            this.lblCauta = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEdituri)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewEdituri.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEdituri.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewEdituri.Name = "dataGridViewEdituri";
            this.dataGridViewEdituri.ReadOnly = true;
            this.dataGridViewEdituri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEdituri.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewEdituri.TabIndex = 0;

            this.txtCauta.Location = new System.Drawing.Point(100, 20);
            this.txtCauta.Name = "txtCauta";
            this.txtCauta.Size = new System.Drawing.Size(250, 20);
            this.txtCauta.TabIndex = 1;
            this.txtCauta.TextChanged += new EventHandler(this.TxtCauta_TextChanged);

            this.lblCauta.AutoSize = true;
            this.lblCauta.Location = new System.Drawing.Point(12, 23);
            this.lblCauta.Name = "lblCauta";
            this.lblCauta.Size = new System.Drawing.Size(71, 13);
            this.lblCauta.TabIndex = 2;
            this.lblCauta.Text = "Cauta editura:";

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

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnEditeaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblCauta);
            this.Controls.Add(this.txtCauta);
            this.Controls.Add(this.dataGridViewEdituri);
            this.Name = "FrmEdituri";
            this.Text = "Gestionare Edituri";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEdituri)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dataGridViewEdituri;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private TextBox txtCauta;
        private Label lblCauta;
    }
}