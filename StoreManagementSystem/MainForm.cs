using System;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class MainForm : Form
    {
        private Button activeButton;
        private readonly Color activeColor = Color.FromArgb(37, 99, 235);
        private readonly Color normalColor = Color.FromArgb(30, 58, 138);

        public MainForm() : this("User")
        {
        }

        public MainForm(string username)
        {
            InitializeComponent();

            Session.CurrentUser = username;
            label1.Text = "Welcome, " + username;

            // Wire up navigation.
            button1.Click += (s, e) => Navigate(button1, new Dashboard());
            button2.Click += (s, e) => Navigate(button2, new Products());
            button3.Click += (s, e) => Navigate(button3, new Categories());
            button4.Click += (s, e) => Navigate(button4, new StockManage());
            button5.Click += (s, e) => Navigate(button5, new StockLog());
            signoutBtn.Click += signoutBtn_Click;

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Database.EnsureSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเตรียมฐานข้อมูลได้: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Show the dashboard first.
            Navigate(button1, new Dashboard());
        }

        private void Navigate(Button source, UserControl control)
        {
            panel3.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel3.Controls.Add(control);
            control.BringToFront();
            HighlightButton(source);
        }

        private void HighlightButton(Button source)
        {
            if (activeButton != null)
                activeButton.BackColor = normalColor;
            activeButton = source;
            activeButton.BackColor = activeColor;
        }

        private void signoutBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ต้องการออกจากระบบใช่หรือไม่?", "Sign out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
