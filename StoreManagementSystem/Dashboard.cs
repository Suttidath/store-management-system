using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class Dashboard : UserControl
    {
        private DataGridView dgvRecent;
        private Label lblRecentTitle;

        public Dashboard()
        {
            InitializeComponent();
            BuildRecentGrid();
        }

        private void BuildRecentGrid()
        {
            // panel2 is an empty white area in the designer; fill it with a
            // "recent stock movements" list so the dashboard is informative.
            lblRecentTitle = new Label
            {
                Text = "Recent Stock Movements",
                Font = new Font("Segoe UI", 11.25F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Location = new Point(12, 10)
            };

            dgvRecent = new DataGridView
            {
                Location = new Point(12, 40),
                Size = new Size(panel2.Width - 24, panel2.Height - 52),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(226, 232, 240),
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                ColumnHeadersHeight = 36,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 138);
            dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRecent.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgvRecent.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgvRecent.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvRecent.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dgvRecent.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvRecent.RowTemplate.Height = 30;

            panel2.Controls.Add(lblRecentTitle);
            panel2.Controls.Add(dgvRecent);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadStats();
            LoadRecent();
        }

        private void LoadStats()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    label2.Text = Scalar(conn, "SELECT COUNT(*) FROM products");
                    label3.Text = Scalar(conn, "SELECT COUNT(*) FROM categories");
                    label5.Text = Scalar(conn, "SELECT ISNULL(SUM(quantity), 0) FROM products");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string Scalar(SqlConnection conn, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                object value = cmd.ExecuteScalar();
                return value == null || value == DBNull.Value ? "0" : value.ToString();
            }
        }

        private void LoadRecent()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query =
                        "SELECT TOP 20 m.date_moved AS [Date], p.name AS [Product], " +
                        "m.movement_type AS [Type], m.quantity AS [Qty], " +
                        "m.department AS [Department], m.used_for AS [Used For] " +
                        "FROM stock_movements m INNER JOIN products p ON m.product_id = p.id " +
                        "ORDER BY m.id DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvRecent.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
