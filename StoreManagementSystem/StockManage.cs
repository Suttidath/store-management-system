using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class StockManage : UserControl
    {
        public StockManage()
        {
            InitializeComponent();
        }

        private void StockManage_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadRecent();
            MovementType_Changed(null, EventArgs.Empty);
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT id, name, ISNULL(product_code,'') AS code, quantity FROM products ORDER BY name", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Friendly label combining name + code.
                        table.Columns.Add("label", typeof(string));
                        foreach (DataRow r in table.Rows)
                        {
                            string code = r["code"].ToString();
                            r["label"] = string.IsNullOrEmpty(code)
                                ? r["name"].ToString()
                                : r["name"] + " (" + code + ")";
                        }

                        int previous = cmbProduct.SelectedValue != null
                            ? Convert.ToInt32(cmbProduct.SelectedValue) : -1;

                        cmbProduct.DataSource = table;
                        cmbProduct.DisplayMember = "label";
                        cmbProduct.ValueMember = "id";

                        if (previous != -1)
                            cmbProduct.SelectedValue = previous;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateCurrentStock();
        }

        private void LoadRecent()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query =
                        "SELECT TOP 50 m.date_moved AS [Date], p.name AS [Product], " +
                        "m.movement_type AS [Type], m.quantity AS [Qty], " +
                        "m.department AS [Department], m.used_for AS [Used For], " +
                        "m.moved_by AS [By] " +
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

        private void UpdateCurrentStock()
        {
            DataRowView drv = cmbProduct.SelectedItem as DataRowView;
            lblCurrentStock.Text = drv != null ? drv["quantity"].ToString() : "0";
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentStock();
        }

        private void MovementType_Changed(object sender, EventArgs e)
        {
            // Department / usage only make sense when stock goes OUT.
            bool isOut = rbOut.Checked;
            lblDepartment.Enabled = isOut;
            cmbDepartment.Enabled = isOut;
            lblUsedFor.Enabled = isOut;
            txtUsedFor.Enabled = isOut;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("กรุณาเลือกสินค้า", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int qty = (int)numQuantity.Value;
            if (qty <= 0)
            {
                MessageBox.Show("กรุณาระบุจำนวนมากกว่า 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(cmbProduct.SelectedValue);
            string type = rbOut.Checked ? "OUT" : "IN";

            if (type == "OUT" && string.IsNullOrWhiteSpace(cmbDepartment.Text))
            {
                MessageBox.Show("กรุณาระบุแผนกที่นำไปใช้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // Read current quantity (locked within the transaction).
                            int current;
                            using (SqlCommand getQty = new SqlCommand(
                                "SELECT quantity FROM products WHERE id = @id", conn, tran))
                            {
                                getQty.Parameters.AddWithValue("@id", productId);
                                current = Convert.ToInt32(getQty.ExecuteScalar());
                            }

                            if (type == "OUT" && qty > current)
                            {
                                tran.Rollback();
                                MessageBox.Show("สต๊อกไม่พอ! คงเหลือ " + current + " ชิ้น",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            int newQty = type == "IN" ? current + qty : current - qty;

                            using (SqlCommand updateProduct = new SqlCommand(
                                "UPDATE products SET quantity = @qty WHERE id = @id", conn, tran))
                            {
                                updateProduct.Parameters.AddWithValue("@qty", newQty);
                                updateProduct.Parameters.AddWithValue("@id", productId);
                                updateProduct.ExecuteNonQuery();
                            }

                            string insert =
                                "INSERT INTO stock_movements " +
                                "(product_id, movement_type, quantity, department, used_for, note, moved_by) " +
                                "VALUES (@pid, @type, @qty, @dept, @used, @note, @by)";
                            using (SqlCommand insertMove = new SqlCommand(insert, conn, tran))
                            {
                                insertMove.Parameters.AddWithValue("@pid", productId);
                                insertMove.Parameters.AddWithValue("@type", type);
                                insertMove.Parameters.AddWithValue("@qty", qty);
                                insertMove.Parameters.AddWithValue("@dept",
                                    type == "OUT" && !string.IsNullOrWhiteSpace(cmbDepartment.Text)
                                        ? (object)cmbDepartment.Text.Trim() : DBNull.Value);
                                insertMove.Parameters.AddWithValue("@used",
                                    type == "OUT" && !string.IsNullOrWhiteSpace(txtUsedFor.Text)
                                        ? (object)txtUsedFor.Text.Trim() : DBNull.Value);
                                insertMove.Parameters.AddWithValue("@note",
                                    string.IsNullOrWhiteSpace(txtNote.Text)
                                        ? DBNull.Value : (object)txtNote.Text.Trim());
                                insertMove.Parameters.AddWithValue("@by", Session.CurrentUser);
                                insertMove.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("บันทึกการเคลื่อนไหวสต๊อกเรียบร้อย", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
                LoadRecent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            numQuantity.Value = 0;
            cmbDepartment.Text = "";
            txtUsedFor.Clear();
            txtNote.Clear();
            rbIn.Checked = true;
        }
    }
}
