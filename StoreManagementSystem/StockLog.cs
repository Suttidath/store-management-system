using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class StockLog : UserControl
    {
        public StockLog()
        {
            InitializeComponent();
        }

        private void StockLog_Load(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;                       // All
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query =
                        "SELECT m.id AS [ID], m.date_moved AS [Date], p.name AS [Product], " +
                        "m.movement_type AS [Type], m.quantity AS [Qty], " +
                        "m.department AS [Department], m.used_for AS [Used For], " +
                        "m.note AS [Note], m.moved_by AS [By] " +
                        "FROM stock_movements m INNER JOIN products p ON m.product_id = p.id " +
                        "WHERE m.date_moved >= @from AND m.date_moved < @to";

                    string type = cmbType.SelectedItem?.ToString() ?? "All";
                    if (type == "IN" || type == "OUT")
                        query += " AND m.movement_type = @type";

                    string search = txtSearch.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(search))
                        query += " AND (p.name LIKE @s OR m.department LIKE @s OR m.used_for LIKE @s OR m.moved_by LIKE @s)";

                    query += " ORDER BY m.id DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@from", dtpFrom.Value.Date);
                        // Include the whole "To" day.
                        cmd.Parameters.AddWithValue("@to", dtpTo.Value.Date.AddDays(1));
                        if (type == "IN" || type == "OUT")
                            cmd.Parameters.AddWithValue("@type", type);
                        if (!string.IsNullOrWhiteSpace(search))
                            cmd.Parameters.AddWithValue("@s", "%" + search + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;

                        UpdateSummary(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary(DataTable table)
        {
            int totalIn = 0, totalOut = 0;
            foreach (DataRow r in table.Rows)
            {
                int qty = Convert.ToInt32(r["Qty"]);
                if (r["Type"].ToString() == "IN") totalIn += qty;
                else totalOut += qty;
            }
            lblSummary.Text = string.Format(
                "Records: {0}    |    Total IN: {1}    |    Total OUT: {2}",
                table.Rows.Count, totalIn, totalOut);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbType.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;
            LoadData();
        }
    }
}
