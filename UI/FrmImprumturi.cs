using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaScolara.Managers;
using BibliotecaScolara.Models;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.UI
{
    public partial class FrmImprumturi : Form
    {
        private List<Imprumut> imprumturiList;
        private int? elevIdFilter = null;

        public FrmImprumturi(int? elevId = null)
        {
            InitializeComponent();
            elevIdFilter = elevId;
            LoadImprumturi();
        }

        private void LoadImprumturi()
        {
            try
            {
                if (elevIdFilter.HasValue)
                {
                    imprumturiList = ImprumutManager.GetActiveByElev(elevIdFilter.Value);
                    this.Text = "Împrumuturi Elev";
                }
                else
                {
                    imprumturiList = ImprumutManager.GetAll();
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare la încărcarea împrumuturilor: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridViewImprumturi.DataSource = imprumturiList;
            AdjustColumns();
        }

        private void AdjustColumns()
        {
            if (dataGridViewImprumturi.Columns.Count > 0)
            {
                dataGridViewImprumturi.Columns["IDImprumut"].Width = 50;
                dataGridViewImprumturi.Columns["NumeElev"].Width = 150;
                dataGridViewImprumturi.Columns["TitluCarte"].Width = 200;
                dataGridViewImprumturi.Columns["DataScadenta"].Width = 100;
                dataGridViewImprumturi.Columns["ZileRamase"].Width = 80;
            }
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            FrmAdaugaImprumut frm = new FrmAdaugaImprumut();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadImprumturi();
                Mesaje.Succes("Împrumutul a fost înregistrat cu succes!");
            }
        }

        private void BtnReturneaza_Click(object sender, EventArgs e)
        {
            if (dataGridViewImprumturi.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un împrumut!");
                return;
            }

            Imprumut imprumut = (Imprumut)dataGridViewImprumturi.SelectedRows[0].DataBoundItem;
            if (imprumut.DataRestituire.HasValue)
            {
                Mesaje.Avertisment("Această carte a fost deja returnată!");
                return;
            }

            FrmReturnareImprumut frm = new FrmReturnareImprumut(imprumut);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadImprumturi();
                Mesaje.Succes("Cartea a fost returnată cu succes!");
            }
        }

        private void BtnPrelungeste_Click(object sender, EventArgs e)
        {
            if (dataGridViewImprumturi.SelectedRows.Count == 0)
            {
                Mesaje.Avertisment("Selectați un împrumut!");
                return;
            }

            Imprumut imprumut = (Imprumut)dataGridViewImprumturi.SelectedRows[0].DataBoundItem;
            if (imprumut.DataRestituire.HasValue)
            {
                Mesaje.Avertisment("Nu puteți prelungi o carte deja returnată!");
                return;
            }

            if (Mesaje.Confirmare("Doriți să prelungiți termenul pentru 30 de zile?") == DialogResult.Yes)
            {
                try
                {
                    if (ImprumutManager.ExtendLoan(imprumut.IDImprumut))
                    {
                        LoadImprumturi();
                        Mesaje.Succes("Termenul a fost prelungit cu succes!");
                    }
                    else
                        Mesaje.Eroare("Nu s-a putut prelungi termenul!");
                }
                catch (Exception ex)
                {
                    Mesaje.Eroare(ex.Message);
                }
            }
        }

        private void BtnIntarziate_Click(object sender, EventArgs e)
        {
            try
            {
                var intarziate = ImprumutManager.GetOverdueLoans();
                FrmRaportIntarziate frm = new FrmRaportIntarziate(intarziate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Mesaje.Eroare("Eroare: " + ex.Message);
            }
        }

        private void TxtCauta_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCauta.Text))
            {
                LoadImprumturi();
            }
            else
            {
                imprumturiList = ImprumutManager.Search(txtCauta.Text);
                RefreshGrid();
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewImprumturi = new DataGridView();
            this.btnAdauga = new Button();
            this.btnReturneaza = new Button();
            this.btnPrelungeste = new Button();
            this.btnIntarziate = new Button();
            this.txtCauta = new TextBox();
            this.lblCauta = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImprumturi)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewImprumturi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImprumturi.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewImprumturi.Name = "dataGridViewImprumturi";
            this.dataGridViewImprumturi.ReadOnly = true;
            this.dataGridViewImprumturi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewImprumturi.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewImprumturi.TabIndex = 0;

            this.txtCauta.Location = new System.Drawing.Point(100, 20);
            this.txtCauta.Name = "txtCauta";
            this.txtCauta.Size = new System.Drawing.Size(250, 20);
            this.txtCauta.TabIndex = 1;
            this.txtCauta.TextChanged += new EventHandler(this.TxtCauta_TextChanged);

            this.lblCauta.AutoSize = true;
            this.lblCauta.Location = new System.Drawing.Point(12, 23);
            this.lblCauta.Name = "lblCauta";
            this.lblCauta.Size = new System.Drawing.Size(83, 13);
            this.lblCauta.TabIndex = 2;
            this.lblCauta.Text = "Cauta imprumut:";

            this.btnAdauga.Location = new System.Drawing.Point(12, 410);
            this.btnAdauga.Name = "btnAdauga";
            this.btnAdauga.Size = new System.Drawing.Size(75, 23);
            this.btnAdauga.TabIndex = 3;
            this.btnAdauga.Text = "Imprumuta";
            this.btnAdauga.UseVisualStyleBackColor = true;
            this.btnAdauga.Click += new EventHandler(this.BtnAdauga_Click);

            this.btnReturneaza.Location = new System.Drawing.Point(93, 410);
            this.btnReturneaza.Name = "btnReturneaza";
            this.btnReturneaza.Size = new System.Drawing.Size(75, 23);
            this.btnReturneaza.TabIndex = 4;
            this.btnReturneaza.Text = "Returneaza";
            this.btnReturneaza.UseVisualStyleBackColor = true;
            this.btnReturneaza.Click += new EventHandler(this.BtnReturneaza_Click);

            this.btnPrelungeste.Location = new System.Drawing.Point(174, 410);
            this.btnPrelungeste.Name = "btnPrelungeste";
            this.btnPrelungeste.Size = new System.Drawing.Size(75, 23);
            this.btnPrelungeste.TabIndex = 5;
            this.btnPrelungeste.Text = "Prelungeste";
            this.btnPrelungeste.UseVisualStyleBackColor = true;
            this.btnPrelungeste.Click += new EventHandler(this.BtnPrelungeste_Click);

            this.btnIntarziate.Location = new System.Drawing.Point(255, 410);
            this.btnIntarziate.Name = "btnIntarziate";
            this.btnIntarziate.Size = new System.Drawing.Size(90, 23);
            this.btnIntarziate.TabIndex = 6;
            this.btnIntarziate.Text = "Cărți Întârziate";
            this.btnIntarziate.UseVisualStyleBackColor = true;
            this.btnIntarziate.Click += new EventHandler(this.BtnIntarziate_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.btnIntarziate);
            this.Controls.Add(this.btnPrelungeste);
            this.Controls.Add(this.btnReturneaza);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.lblCauta);
            this.Controls.Add(this.txtCauta);
            this.Controls.Add(this.dataGridViewImprumturi);
            this.Name = "FrmImprumturi";
            this.Text = "Gestionare Împrumuturi";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImprumturi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dataGridViewImprumturi;
        private Button btnAdauga;
        private Button btnReturneaza;
        private Button btnPrelungeste;
        private Button btnIntarziate;
        private TextBox txtCauta;
        private Label lblCauta;
    }
}