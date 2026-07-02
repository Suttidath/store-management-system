using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class Categories : UserControl
    {
        private int selectedId = 0;

        public Categories()
        {
            InitializeComponent();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(string search = "")
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query =
                        "SELECT id AS [ID], name AS [Name], description AS [Description], " +
                        "date_created AS [Created] FROM categories";
                    if (!string.IsNullOrWhiteSpace(search))
                        query += " WHERE name LIKE @s OR description LIKE @s";
                    query += " ORDER BY id DESC";

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อหมวดหมู่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string insert = "INSERT INTO categories (name, description) VALUES (@name, @desc)";
                    using (SqlCommand cmd = new SqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", (object)txtDescription.Text.Trim() ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("เพิ่มหมวดหมู่เรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("กรุณาเลือกหมวดหมู่จากตารางก่อน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อหมวดหมู่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string update = "UPDATE categories SET name = @name, description = @desc WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", (object)txtDescription.Text.Trim() ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("แก้ไขหมวดหมู่เรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("กรุณาเลือกหมวดหมู่จากตารางก่อน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("ต้องการลบหมวดหมู่นี้ใช่หรือไม่?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Prevent deleting a category that products still reference.
                    using (SqlCommand check = new SqlCommand(
                        "SELECT COUNT(*) FROM products WHERE category_id = @id", conn))
                    {
                        check.Parameters.AddWithValue("@id", selectedId);
                        int used = (int)check.ExecuteScalar();
                        if (used > 0)
                        {
                            MessageBox.Show("ไม่สามารถลบได้ เพราะมีสินค้า " + used +
                                " รายการใช้หมวดหมู่นี้อยู่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("ลบหมวดหมู่เรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtName.Clear();
            txtDescription.Clear();
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells["ID"].Value == null) return;

            selectedId = Convert.ToInt32(row.Cells["ID"].Value);
            txtName.Text = row.Cells["Name"].Value?.ToString() ?? "";
            txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }
    }
}
