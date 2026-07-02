using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class Products : UserControl
    {
        private int selectedId = 0;

        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadData();
        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT id, name FROM categories ORDER BY name", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // "-- None --" option so a product can have no category.
                        DataRow none = table.NewRow();
                        none["id"] = 0;
                        none["name"] = "-- None --";
                        table.Rows.InsertAt(none, 0);

                        cmbCategory.DataSource = table;
                        cmbCategory.DisplayMember = "name";
                        cmbCategory.ValueMember = "id";
                        cmbCategory.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData(string search = "")
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query =
                        "SELECT p.id AS [ID], p.product_code AS [Code], p.name AS [Name], " +
                        "ISNULL(c.name, '-') AS [Category], p.brand AS [Brand], " +
                        "p.quantity AS [Quantity], p.description AS [Description] " +
                        "FROM products p LEFT JOIN categories c ON p.category_id = c.id";
                    if (!string.IsNullOrWhiteSpace(search))
                        query += " WHERE p.name LIKE @s OR p.product_code LIKE @s OR p.brand LIKE @s";
                    query += " ORDER BY p.id DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                            cmd.Parameters.AddWithValue("@s", "%" + search + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private object CategoryValue()
        {
            if (cmbCategory.SelectedValue == null) return DBNull.Value;
            int id = Convert.ToInt32(cmbCategory.SelectedValue);
            return id == 0 ? (object)DBNull.Value : id;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อสินค้า", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string insert =
                        "INSERT INTO products (product_code, name, category_id, brand, description, quantity) " +
                        "VALUES (@code, @name, @cat, @brand, @desc, @qty)";
                    using (SqlCommand cmd = new SqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@code", (object)txtCode.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@cat", CategoryValue());
                        cmd.Parameters.AddWithValue("@brand", (object)txtBrand.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@desc", (object)txtDescription.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@qty", (int)numQuantity.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("เพิ่มสินค้าเรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าจากตารางก่อน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อสินค้า", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string update =
                        "UPDATE products SET product_code = @code, name = @name, category_id = @cat, " +
                        "brand = @brand, description = @desc, quantity = @qty WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@code", (object)txtCode.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@cat", CategoryValue());
                        cmd.Parameters.AddWithValue("@brand", (object)txtBrand.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@desc", (object)txtDescription.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@qty", (int)numQuantity.Value);
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("แก้ไขสินค้าเรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าจากตารางก่อน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("ต้องการลบสินค้านี้ใช่หรือไม่?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Block deletion if there are stock movements referencing this product.
                    using (SqlCommand check = new SqlCommand(
                        "SELECT COUNT(*) FROM stock_movements WHERE product_id = @id", conn))
                    {
                        check.Parameters.AddWithValue("@id", selectedId);
                        int used = (int)check.ExecuteScalar();
                        if (used > 0)
                        {
                            MessageBox.Show("ไม่สามารถลบได้ เพราะมีประวัติการเคลื่อนไหวสต๊อก " + used +
                                " รายการของสินค้านี้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("ลบสินค้าเรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
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
            selectedId = 0;
            txtCode.Clear();
            txtName.Clear();
            txtBrand.Clear();
            txtDescription.Clear();
            numQuantity.Value = 0;
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells["ID"].Value == null) return;

            selectedId = Convert.ToInt32(row.Cells["ID"].Value);
            txtCode.Text = row.Cells["Code"].Value?.ToString() ?? "";
            txtName.Text = row.Cells["Name"].Value?.ToString() ?? "";
            txtBrand.Text = row.Cells["Brand"].Value?.ToString() ?? "";
            txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";

            int qty;
            numQuantity.Value = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out qty) ? qty : 0;

            // Match the category name back to the combo.
            string catName = row.Cells["Category"].Value?.ToString() ?? "-";
            SelectCategoryByName(catName);
        }

        private void SelectCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name) || name == "-")
            {
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                return;
            }
            for (int i = 0; i < cmbCategory.Items.Count; i++)
            {
                DataRowView drv = cmbCategory.Items[i] as DataRowView;
                if (drv != null && drv["name"].ToString() == name)
                {
                    cmbCategory.SelectedIndex = i;
                    return;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }
    }
}
