using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using PIERANGELO;

namespace PIERANGELO
{
    public partial class AdminMainForm : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;

        public string password_, username_;
        private Form currentForm;
        private string _username;


        public AdminMainForm(string username)
        {
            InitializeComponent();
            username_ = username;
            _username = username;
            con = new SqlConnection(dbcon.DataConnection());

            //CriticalNotif();
            DashboardMain();
            CheckNotifications();
            LoadProfilePicture(username_);
            LoadUserProfile();

        }
        private void ChangeForm(Form newForm)
        {
            if(currentForm != null)
            {
                currentForm.Dispose();
            }
            currentForm = newForm;
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;
            panelcontent.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();
        }
        private void LoadUserProfile()
        {
            // Assuming you fetch the profile data based on _username
            using (SqlConnection conn = new SqlConnection(dbcon.DataConnection()))
            {
                string query = "SELECT name FROM tableUserAccount WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", _username);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblname.Text = reader["name"].ToString(); // Assuming lblName is where the name is displayed
                    }
                }
            }
        }
        public void LoadProfilePicture(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username is empty. Cannot load profile picture.");
                return;
            }

            string query = "SELECT profilePicture FROM tableUserAccount WHERE username = @username";

            using (SqlConnection conn = new SqlConnection(dbcon.DataConnection()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();

                try
                {
                    byte[] imageBytes = cmd.ExecuteScalar() as byte[];

                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            profilepicture.Image = Image.FromStream(ms);
                        }
                    }
                    // If no profile picture is found, simply don't set the image
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile picture: " + ex.Message);
                }
            }
        }




        private void CheckNotifications()
        {
            bool hasNotifications = false;

            // Check for expired products today
            con.Open();
            com = new SqlCommand("SELECT COUNT(*) FROM tableStock WHERE CONVERT(date, expirydate) = CONVERT(date, GETDATE())", con);
            int expiredTodayCount = (int)com.ExecuteScalar();
            con.Close();
            if (expiredTodayCount > 0)
            {
                hasNotifications = true;
            }

            // Check for out-of-stock products
            con.Open();
            com = new SqlCommand("SELECT COUNT(*) FROM tableProductList WHERE qty = 0", con);
            int outOfStockCount = (int)com.ExecuteScalar();
            con.Close();
            if (outOfStockCount > 0)
            {
                hasNotifications = true;
            }

            // Check for critical stock levels (threshold is 5)
            con.Open();
            com = new SqlCommand("SELECT COUNT(*) FROM tableProductList WHERE qty < reorder", con);
            int criticalStockCount = (int)com.ExecuteScalar();
            con.Close();
            if (criticalStockCount > 0)
            {
                hasNotifications = true;
            }

            // Notify admin if there are any notifications
            if (hasNotifications)
            {
                picboxnotif.Image = Properties.Resources.notification__2_; // Set the bell icon image
                picboxnotif.Visible = true;
            }
            else
            {
                picboxnotif.Visible = false;
            }
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult message = MessageBox.Show("LOGOUT APPLICATION", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (message == DialogResult.Yes)
            {
                // Get the current user's username 
                string name = lblname.Text;

                // Log the logout action in tableLogHistory
                Log(name, "The Admin has successfully logged out.", "Log Out");
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
                this.Close(); // Optionally close the current form after showing the login form
            }
        }


        private void Log(string user, string activity, string action)
        {
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO tableLogHistory (username, Date, activity ,action) VALUES (@user, @Date, @activity, @action)", con);
            command.Parameters.AddWithValue("@user", user);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@activity", activity);
            command.Parameters.AddWithValue("@action", action);
            command.ExecuteNonQuery();
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            DashboardMain();
            BackColor = Color.FromArgb(143, 151, 184);



        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ReportTransition.Start();

        }
        private void btninventory_Click(object sender, EventArgs e)
        {
            InventoryTransition.Start();
            BackColor = Color.FromArgb(143, 151, 184);
            btndashboard.BackColor = Color.FromArgb(22, 33, 61);
        }

        bool inventoryexpand = false;
        private void InventoryTransition_Tick(object sender, EventArgs e)
        {
            if (inventoryexpand == false)
            {
                InventoryContainer.Height += 15;
                if (InventoryContainer.Height >= InventoryContainer.MaximumSize.Height)
                {
                    InventoryTransition.Stop();
                    inventoryexpand = true;

                }
            }
            else
            {
                InventoryContainer.Height -= 15;
                if (InventoryContainer.Height <= InventoryContainer.MinimumSize.Height)
                {
                    InventoryTransition.Stop();
                    inventoryexpand = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductList product = new ProductList();
            product.ShowProductList();
            ChangeForm(product);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.CategoryList();
            ChangeForm(category);
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            StockLevelsList stock = new StockLevelsList();
            stock.ShowAdjustmentStock();
            ChangeForm(stock);
        }
        
        bool reportExpand = false;
        private void ReportTransition_Tick(object sender, EventArgs e)
        {
            if (reportExpand == false)
            {
                ReportContainer.Height += 15;
                if (ReportContainer.Height >= ReportContainer.MaximumSize.Height)
                {
                    ReportTransition.Stop();
                    reportExpand = true;
                }
            }
            else
            {
                ReportContainer.Height -= 15;
                if (ReportContainer.Height <= ReportContainer.MinimumSize.Height)
                {
                    ReportTransition.Stop();
                    reportExpand = false;
                }
            }
        }

        private void btnsalesreport_Click(object sender, EventArgs e)
        {

            SalesReport report = new SalesReport();
            ChangeForm(report);
            
        }

        private void btninventoryReports_Click(object sender, EventArgs e)
        {
            InventoryReports inreport = new InventoryReports();
            inreport.ShowInventory();
            ChangeForm(inreport);
           
        }

        private void btnrandex_Click(object sender, EventArgs e)
        {

            ReturnExchangeAdmin cancel = new ReturnExchangeAdmin();
            cancel.ShowReturnExchange();
            ChangeForm(cancel);
;
        }
        bool sidebarExpand;
        private void sidebartransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebartransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebartransition.Stop();
                }
            }
        }

        private void btnsidebar_Click(object sender, EventArgs e)
        {
            sidebartransition.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Notification no = new Notification();
            no.TopLevel = false;
            int panelWidth = panelcontent.Width;
            int panelHeight = panelcontent.Height;
            int formWidth = no.Width;
            int formHeight = no.Height;
            no.Location = new Point(panelWidth - formWidth, (panelHeight - formHeight) / 2);
            no.Size = new Size(formWidth, formHeight);
            panelcontent.Controls.Add(no);
            no.BringToFront();
            no.Show();
        }


        private void btnSoldProducts_Click(object sender, EventArgs e)
        {
            POReports pro = new POReports();
            pro.PurchaseOrder();
            ChangeForm(pro);
        }


        private void useraccount_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            //account.ShowUser();
            ChangeForm(account);
        }

        private void loghistory_Click(object sender, EventArgs e)
        {
            LogHistory log = new LogHistory();
            ChangeForm(log);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateDiscount cd = new CreateDiscount();;
            cd.Discount();
            ChangeForm(cd);
          
        }

        private void supplierbtn_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.ShowSupplier();
            ChangeForm(supplier);
        }

        bool SettingExpand = false;
        private void Settings_Tick(object sender, EventArgs e)
        {
            if (SettingExpand == false)
            {
                SettingContainer.Height += 15;
                if (SettingContainer.Height >= SettingContainer.MaximumSize.Height)
                {
                    Settingstransition.Stop();
                    SettingExpand = true;
                }
            }
            else
            {
                SettingContainer.Height -= 15;
                if (SettingContainer.Height <= SettingContainer.MinimumSize.Height)
                {
                    Settingstransition.Stop();
                    SettingExpand = false;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Settingstransition.Start();
        }

        private void btnArchives_Click(object sender, EventArgs e)
        {
            RecycleBin arch = new RecycleBin();
            ChangeForm(arch);
        }
        private void profilepicture_Click(object sender, EventArgs e)
        {
           
            EditProfile no = new EditProfile(username_);
            no.txtUsername.Text = lblUser.Text;
            no.TopLevel = false;

            // Calculate the location and size for the form to be placed on the right side
            int panelWidth = panelcontent.Width;
            int panelHeight = panelcontent.Height;
            int formWidth = no.Width;
            int formHeight = no.Height;

            // Set the location of the form to the right side of the panel
            no.Location = new Point(panelWidth - formWidth, (panelHeight - formHeight) / 2);

            // Optionally, you can set the size of the form if needed
            no.Size = new Size(formWidth, formHeight);

            // Add the form to the panel and display it
            panelcontent.Controls.Add(no);
            no.BringToFront();
            no.Show();
          
        }

        private void profilepicture_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0,profilepicture.Width - 1, profilepicture.Height - 1);
                Region rg = new Region(gp);
                profilepicture.Region = rg;

                g.DrawEllipse(new Pen(Color.Black, 1), 0, 0, profilepicture.Width - 1, profilepicture.Height - 1);
            }
        }

        private void lblname_Click(object sender, EventArgs e)
        {
            EditProfile no = new EditProfile(username_);
            no.txtUsername.Text = lblUser.Text;
            no.TopLevel = false;

            // Calculate the location and size for the form to be placed on the right side
            int panelWidth = panelcontent.Width;
            int panelHeight = panelcontent.Height;
            int formWidth = no.Width;
            int formHeight = no.Height;

            // Set the location of the form to the right side of the panel
            no.Location = new Point(panelWidth - formWidth, (panelHeight - formHeight) / 2);

            // Optionally, you can set the size of the form if needed
            no.Size = new Size(formWidth, formHeight);

            // Add the form to the panel and display it
            panelcontent.Controls.Add(no);
            no.BringToFront();
            no.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDateee.Text = DateTime.Now.ToLongDateString();
        }

        private void panelcontent_Paint(object sender, PaintEventArgs e)
        {

        }

        public void DashboardMain()
        {
            
            DashBoard dash = new DashBoard();
            dash.TopLevel = false;
            dash.lbltotaldailysales.Text = "₱" + dbcon.DailySales().ToString("#,##0.00");
            dash.lblTotalSales.Text = "₱" + dbcon.TotalSales().ToString(" #,##0.00");
            dash.lblreturnitems.Text =  dbcon.ReturnandExchange().ToString("#,##0");
            dash.lbltotaltrans.Text = dbcon.TotalTransaction().ToString("#,##0");        
            dash.ShowTopSelling();
            dash.ShowTopCategory();
            dash.ShowExpiry();
            panelcontent.Controls.Add(dash);
            dash.BringToFront();
            dash.Show();

        }
    }
}
