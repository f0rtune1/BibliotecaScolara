using System.Drawing;
using System.Windows.Forms;

namespace BibliotecaScolara.Utilities
{
    /// <summary>
    /// Helper class for consistent school-themed UI styling
    /// </summary>
    public static class ThemeHelper
    {
        // School color palette
        public static readonly Color PrimaryBlue = Color.FromArgb(26, 82, 118);       // Dark blue
        public static readonly Color SecondaryBlue = Color.FromArgb(52, 152, 219);    // Medium blue
        public static readonly Color LightBlue = Color.FromArgb(214, 234, 248);       // Light blue background
        public static readonly Color AccentGreen = Color.FromArgb(39, 174, 96);       // Green for Add/Save
        public static readonly Color AccentRed = Color.FromArgb(192, 57, 43);         // Red for Delete
        public static readonly Color AccentOrange = Color.FromArgb(211, 84, 0);       // Orange for Edit
        public static readonly Color NeutralGray = Color.FromArgb(149, 165, 166);     // Gray for Cancel/Close
        public static readonly Color FormBackground = Color.FromArgb(236, 240, 241);  // Light gray form bg
        public static readonly Color HeaderBackground = Color.FromArgb(26, 82, 118);  // Header bg
        public static readonly Color GridAlternateRow = Color.FromArgb(235, 245, 251); // Grid alt row
        public static readonly Color GridHeaderBack = Color.FromArgb(52, 73, 94);     // Grid header
        public static readonly Color GridHeaderFore = Color.White;

        /// <summary>
        /// Applies the school theme to a form
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            form.BackColor = FormBackground;
            form.Font = new Font("Segoe UI", 9F);
        }

        /// <summary>
        /// Styles a DataGridView with school theme
        /// </summary>
        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.AlternatingRowsDefaultCellStyle.BackColor = GridAlternateRow;
            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBack;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderFore;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.GridColor = Color.FromArgb(189, 195, 199);
            grid.DefaultCellStyle.SelectionBackColor = SecondaryBlue;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.EnableHeadersVisualStyles = false;
        }

        /// <summary>
        /// Styles an "Add" button
        /// </summary>
        public static void StyleButtonAdd(Button btn)
        {
            btn.BackColor = AccentGreen;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles an "Edit" button
        /// </summary>
        public static void StyleButtonEdit(Button btn)
        {
            btn.BackColor = SecondaryBlue;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles a "Delete" button
        /// </summary>
        public static void StyleButtonDelete(Button btn)
        {
            btn.BackColor = AccentRed;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles a "Neutral/Cancel/Close" button
        /// </summary>
        public static void StyleButtonNeutral(Button btn)
        {
            btn.BackColor = NeutralGray;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles a "Primary Action" button (Save, Confirm, Login)
        /// </summary>
        public static void StyleButtonPrimary(Button btn)
        {
            btn.BackColor = PrimaryBlue;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles a "Warning/Orange" button
        /// </summary>
        public static void StyleButtonWarning(Button btn)
        {
            btn.BackColor = AccentOrange;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Creates a header panel for MDI child forms
        /// </summary>
        public static Panel CreateHeaderPanel(Form form, string title)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = PrimaryBlue
            };

            Label lbl = new Label
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            panel.Controls.Add(lbl);
            return panel;
        }
    }
}
