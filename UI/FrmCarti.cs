using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmCarti : Form
    {
        private List<Carte> cartiList;
        private List<Autor> autoriList;
        private List<Editura> edituriList;
        private List<Categorie> categoriiList;

        public FrmCarti()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                cartiList = CarteManager.GetAll();
                autoriList = AutorManager.GetAll();
                edituriList = EdituraManager.GetAll();
                categoriiList = CategorieManager.GetAll();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea datelor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewCarti.DataSource = cartiList;
            AdjustColumns();
        }

        private void AdjustColumns()
        {
            if (dataGridViewCarti.Columns.Count > 0)
            {
                dataGridViewCarti.Columns["IDCarte"].Width = 60;
                dataGridViewCarti.Columns["Titlu"].Width = 250;
                dataGridViewCarti.Columns["NumeAutor"].Width = 150;
                dataGridViewCarti.Columns["NumeEditura"].Width = 120;
            }
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaCarte frm = new FrmAdaugaCarte(autoriList, edituriList, categoriiList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                Mesaje.Succes("Cartea a fost adăugată cu succes!");
            }
        }

        private void BtnEditeaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarti.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați o carte!");
                return;
            }

            Carte carte = (Carte)dataGridViewCarti.SelectedRows[0].DataBoundItem;
            FrmAdaugaCarte frm = new FrmAdaugaCarte(autoriList, edituriList, categoriiList, carte);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                Mesaje.Succes("Cartea a fost actualizată cu succes!");
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarti.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați o carte!");
                return;
            }

            if (Mesaje.ConfirmareSt() == DialogResult.Yes)
            {
                Carte carte = (Carte)dataGridViewCarti.SelectedRows[0].DataBoundItem;
                try
                {
                    if (CarteManager.Delete(carte.IDCarte))
                    {
                        LoadData();
                        Mesaje.Succes("Cartea a fost ștearsă cu succes!");
                    }
                    else
                        Mesaje.Eroare("Nu s-a putut șterge cartea!");
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
                cartiList = CarteManager.Search(txtCauta.Text);
                RefreshGrid();
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewCarti = new DataGridView();
            this.btnAdauga = new Button();
            this.btnEditeaza = new Button();
            this.btnSterge = new Button();
            this.txtCauta = new TextBox();
            this.lblCauta = new Label();
            this.btnExemplar = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCarti)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewCarti.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCarti.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewCarti.Name = "dataGridViewCarti";
            this.dataGridViewCarti.ReadOnly = true;
            this.dataGridViewCarti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCarti.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewCarti.TabIndex = 0;

            this.txtCauta.Location = new System.Drawing.Point(100, 20);
            this.txtCauta.Name = "txtCauta";
            this.txtCauta.Size = new System.Drawing.Size(250, 20);
            this.txtCauta.TabIndex = 1;
            this.txtCauta.TextChanged += new EventHandler(this.TxtCauta_TextChanged);

            this.lblCauta.AutoSize = true;
            this.lblCauta.Location = new System.Drawing.Point(12, 23);
            this.lblCauta.Name = "lblCauta";
            this.lblCauta.Size = new System.Drawing.Size(68, 13);
            this.lblCauta.TabIndex = 2;
            this.lblCauta.Text = "Cauta carte:";

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

            this.btnExemplar.Location = new System.Drawing.Point(255, 410);
            this.btnExemplar.Name = "btnExemplar";
            this.btnExemplar.Size = new System.Drawing.Size(100, 23);
            this.btnExemplar.TabIndex = 6;
            this.btnExemplar.Text = "Exemplare";
            this.btnExemplar.UseVisualStyleBackColor = true;
            this.btnExemplar.Click += new EventHandler(this.BtnExemplar_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.btnExemplar);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnEditeaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblCauta);
            this.Controls.Add(this.txtCauta);
            this.Controls.Add(this.dataGridViewCarti);
            this.Name = "FrmCarti";
            this.Text = "Gestionare Cărți";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCarti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnExemplar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarti.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați o carte!");
                return;
            }

            Carte carte = (Carte)dataGridViewCarti.SelectedRows[0].DataBoundItem;
            FrmExemplare frm = new FrmExemplare(carte.IDCarte);
            frm.ShowDialog();
        }

        private DataGridView dataGridViewCarti;
        private Button btnAdauga;
        private Button btnEditeaza;
        private Button btnSterge;
        private TextBox txtCauta;
        private Label lblCauta;
        private Button btnExemplar;
    }
}