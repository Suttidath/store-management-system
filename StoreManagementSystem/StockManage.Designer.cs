namespace StoreManagementSystem
{
    partial class StockManage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.lblRecent = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtUsedFor = new System.Windows.Forms.TextBox();
            this.lblUsedFor = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.rbOut = new System.Windows.Forms.RadioButton();
            this.rbIn = new System.Windows.Forms.RadioButton();
            this.lblType = new System.Windows.Forms.Label();
            this.lblCurrentStock = new System.Windows.Forms.Label();
            this.lblStockCaption = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.dgvRecent);
            this.panelTop.Controls.Add(this.lblRecent);
            this.panelTop.Location = new System.Drawing.Point(23, 369);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1169, 308);
            this.panelTop.TabIndex = 1;
            // 
            // dgvRecent
            // 
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecent.EnableHeadersVisualStyles = false;
            this.dgvRecent.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRecent.ColumnHeadersHeight = 36;
            this.dgvRecent.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dgvRecent.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.dgvRecent.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.dgvRecent.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.dgvRecent.RowTemplate.Height = 30;
            this.dgvRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecent.Location = new System.Drawing.Point(21, 55);
            this.dgvRecent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.RowHeadersWidth = 51;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.Size = new System.Drawing.Size(1127, 234);
            this.dgvRecent.TabIndex = 0;
            // 
            // lblRecent
            // 
            this.lblRecent.AutoSize = true;
            this.lblRecent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecent.Location = new System.Drawing.Point(16, 15);
            this.lblRecent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecent.Name = "lblRecent";
            this.lblRecent.Size = new System.Drawing.Size(203, 24);
            this.lblRecent.TabIndex = 0;
            this.lblRecent.Text = "Recent Movements";
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelForm.Controls.Add(this.btnSave);
            this.panelForm.Controls.Add(this.btnClear);
            this.panelForm.Controls.Add(this.txtNote);
            this.panelForm.Controls.Add(this.lblNote);
            this.panelForm.Controls.Add(this.txtUsedFor);
            this.panelForm.Controls.Add(this.lblUsedFor);
            this.panelForm.Controls.Add(this.cmbDepartment);
            this.panelForm.Controls.Add(this.lblDepartment);
            this.panelForm.Controls.Add(this.numQuantity);
            this.panelForm.Controls.Add(this.lblQuantity);
            this.panelForm.Controls.Add(this.rbOut);
            this.panelForm.Controls.Add(this.rbIn);
            this.panelForm.Controls.Add(this.lblType);
            this.panelForm.Controls.Add(this.lblCurrentStock);
            this.panelForm.Controls.Add(this.lblStockCaption);
            this.panelForm.Controls.Add(this.cmbProduct);
            this.panelForm.Controls.Add(this.lblProduct);
            this.panelForm.Controls.Add(this.lblFormTitle);
            this.panelForm.Location = new System.Drawing.Point(23, 17);
            this.panelForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1169, 345);
            this.panelForm.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(791, 271);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(173, 52);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save Movement";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(977, 271);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(153, 52);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNote.Location = new System.Drawing.Point(791, 126);
            this.txtNote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(339, 110);
            this.txtNote.TabIndex = 7;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNote.Location = new System.Drawing.Point(714, 129);
            this.lblNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(45, 21);
            this.lblNote.TabIndex = 0;
            this.lblNote.Text = "Note";
            // 
            // txtUsedFor
            // 
            this.txtUsedFor.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtUsedFor.Location = new System.Drawing.Point(791, 65);
            this.txtUsedFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsedFor.Name = "txtUsedFor";
            this.txtUsedFor.Size = new System.Drawing.Size(339, 27);
            this.txtUsedFor.TabIndex = 6;
            // 
            // lblUsedFor
            // 
            this.lblUsedFor.AutoSize = true;
            this.lblUsedFor.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblUsedFor.Location = new System.Drawing.Point(627, 68);
            this.lblUsedFor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsedFor.Name = "lblUsedFor";
            this.lblUsedFor.Size = new System.Drawing.Size(132, 21);
            this.lblUsedFor.TabIndex = 0;
            this.lblUsedFor.Text = "Used For / Place";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Items.AddRange(new object[] {
            "Production",
            "Sales",
            "Marketing",
            "IT",
            "HR",
            "Finance",
            "Warehouse",
            "Maintenance",
            "Other"});
            this.cmbDepartment.Location = new System.Drawing.Point(216, 221);
            this.cmbDepartment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(319, 27);
            this.cmbDepartment.TabIndex = 5;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDepartment.Location = new System.Drawing.Point(56, 225);
            this.lblDepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(99, 21);
            this.lblDepartment.TabIndex = 0;
            this.lblDepartment.Text = "Department";
            // 
            // numQuantity
            // 
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.numQuantity.Location = new System.Drawing.Point(216, 171);
            this.numQuantity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(167, 27);
            this.numQuantity.TabIndex = 4;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblQuantity.Location = new System.Drawing.Point(56, 174);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(73, 21);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity";
            // 
            // rbOut
            // 
            this.rbOut.AutoSize = true;
            this.rbOut.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rbOut.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.rbOut.Location = new System.Drawing.Point(376, 124);
            this.rbOut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbOut.Name = "rbOut";
            this.rbOut.Size = new System.Drawing.Size(167, 25);
            this.rbOut.TabIndex = 3;
            this.rbOut.Text = "OUT (ลด/นำไปใช้)";
            this.rbOut.UseVisualStyleBackColor = true;
            this.rbOut.CheckedChanged += new System.EventHandler(this.MovementType_Changed);
            // 
            // rbIn
            // 
            this.rbIn.AutoSize = true;
            this.rbIn.Checked = true;
            this.rbIn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rbIn.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.rbIn.Location = new System.Drawing.Point(216, 124);
            this.rbIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbIn.Name = "rbIn";
            this.rbIn.Size = new System.Drawing.Size(144, 25);
            this.rbIn.TabIndex = 2;
            this.rbIn.TabStop = true;
            this.rbIn.Text = "IN (เพิ่ม/รับเข้า)";
            this.rbIn.UseVisualStyleBackColor = true;
            this.rbIn.CheckedChanged += new System.EventHandler(this.MovementType_Changed);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblType.Location = new System.Drawing.Point(56, 126);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(46, 21);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Type";
            // 
            // lblCurrentStock
            // 
            this.lblCurrentStock.AutoSize = true;
            this.lblCurrentStock.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblCurrentStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblCurrentStock.Location = new System.Drawing.Point(1108, 15);
            this.lblCurrentStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentStock.Name = "lblCurrentStock";
            this.lblCurrentStock.Size = new System.Drawing.Size(22, 23);
            this.lblCurrentStock.TabIndex = 0;
            this.lblCurrentStock.Text = "0";
            // 
            // lblStockCaption
            // 
            this.lblStockCaption.AutoSize = true;
            this.lblStockCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblStockCaption.Location = new System.Drawing.Point(984, 15);
            this.lblStockCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStockCaption.Name = "lblStockCaption";
            this.lblStockCaption.Size = new System.Drawing.Size(116, 21);
            this.lblStockCaption.TabIndex = 0;
            this.lblStockCaption.Text = "Current Stock:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(216, 65);
            this.cmbProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(319, 27);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblProduct.Location = new System.Drawing.Point(53, 68);
            this.lblProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(66, 21);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.Location = new System.Drawing.Point(16, 15);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(218, 24);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Add / Remove Stock";
            // 
            // StockManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelForm);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "StockManage";
            this.Size = new System.Drawing.Size(1213, 695);
            this.Load += new System.EventHandler(this.StockManage_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.Label lblRecent;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtUsedFor;
        private System.Windows.Forms.Label lblUsedFor;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.RadioButton rbOut;
        private System.Windows.Forms.RadioButton rbIn;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblCurrentStock;
        private System.Windows.Forms.Label lblStockCaption;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblFormTitle;
    }
}
