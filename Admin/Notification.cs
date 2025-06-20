  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class Notification : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public Notification()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            LoadNotifications();
           
        }

        private void LoadNotifications()
        {
            listnofitication.Items.Clear(); // Clear existing items in the ListBox

            try
            {
                HashSet<string> flaggedProducts = new HashSet<string>();
                var connectionString = dbcon.DataConnection();

                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Helper method to handle query execution and list population
                    void AddNotifications(string query, string header, string countLabel, string itemLabel, Func<SqlDataReader, string> formatItem)
                    {
                        using (var cmd = new SqlCommand(query, con))
                        using (var dr = cmd.ExecuteReader())
                        {
                            int totalCount = 0;
                            bool headerAdded = false;
                            while (dr.Read())
                            {
                                var productName = dr["productname"].ToString();
                                if (!flaggedProducts.Contains(productName))
                                {
                                    if (!headerAdded)
                                    {
                                        listnofitication.Items.Add(header);
                                        headerAdded = true;
                                    }

                                    totalCount += (int)dr[$"Count{itemLabel}"];
                                    listnofitication.Items.Add(formatItem(dr));
                                    flaggedProducts.Add(productName);
                                }
                            }

                            if (totalCount > 0 && headerAdded)
                            {
                                listnofitication.Items.Insert(
                                    listnofitication.Items.IndexOf(header) + 1,
                                    $"{countLabel}: {totalCount}"
                                );
                            }
                        }
                    }

                    // Queries for notifications
                    string expiredQuery = @"
                SELECT COUNT(*) AS CountExpired, p.productname 
                FROM tableProductList AS p
                INNER JOIN tablePODetails AS s ON p.pcode = s.pcode
                WHERE CONVERT(date, s.expirydate) = CONVERT(date, GETDATE())
                GROUP BY p.productname";

                    string outOfStockQuery = @"
                SELECT COUNT(*) AS CountOutOfStock, p.productname 
                FROM tableProductList AS p
                INNER JOIN tablePODetails AS s ON p.pcode = s.pcode
                WHERE ISNULL(s.qtyRO, 0) = 0
                GROUP BY p.productname";

                    string criticalStockQuery = @"
                SELECT COUNT(*) AS CountCritical, p.productname 
                FROM tableProductList AS p
                INNER JOIN tablePODetails AS s ON p.pcode = s.pcode
                WHERE ISNULL(s.qtyRO, 0) <= p.criticallevel
                GROUP BY p.productname";

                    string lowStockQuery = @"
                SELECT COUNT(*) AS CountLowStock, p.productname 
                FROM tableProductList AS p
                INNER JOIN tablePODetails AS s ON p.pcode = s.pcode
                WHERE ISNULL(s.qtyRO, 0) <= p.reorder
                GROUP BY p.productname";

                    // Populate notifications
                    AddNotifications(
                        expiredQuery,
                        "Expired Today:",
                        "Total Expired",
                        "Expired",
                        dr => "  - " + dr["productname"]
                    );

                    AddNotifications(
                        outOfStockQuery,
                        "Out of Stock:",
                        "Total Out of Stock",
                        "OutOfStock",
                        dr => "  - " + dr["productname"]
                    );

                    AddNotifications(
                        criticalStockQuery,
                        "Critical Stock:",
                        "Total Critical Stock",
                        "Critical",
                        dr => "  - " + dr["productname"]
                    );

                    AddNotifications(
                        lowStockQuery,
                        "Low Stock:",
                        "Total Low Stock",
                        "LowStock",
                        dr => "  - " + dr["productname"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading notifications: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listnofitication_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            // Get the text of the current item
            string text = listnofitication.Items[e.Index].ToString();
            Font font;
            Color color;

            if (text.EndsWith(":")) // Check for the ":" to identify titles
            {
                // Title: Bold, font size 11, and different color (e.g., Blue)
                font = new Font(e.Font.FontFamily, 10, FontStyle.Bold);
                color = Color.FromArgb(22, 33, 61); 
            }
            else
            {
                font = new Font(e.Font.FontFamily, 10, FontStyle.Regular);
                color = e.ForeColor;
            }

            e.Graphics.DrawString(text, font, new SolidBrush(color), e.Bounds);

            e.DrawFocusRectangle();
        }

        private void listnofitication_Click(object sender, EventArgs e)
        {
            if (listnofitication.SelectedItem != null)
            {
                string selectedItem = listnofitication.SelectedItem.ToString();

                // Extract the product name if it's not a category title
                if (!selectedItem.EndsWith(":") && !selectedItem.StartsWith("Total"))
                {
                    string productName = selectedItem.Replace("  - ", "").Trim();
                    OpenProductListForm(productName);
                }
            }
        }
        private void OpenProductListForm(string productName)
        {
            ProductList productListForm = new ProductList();
            productListForm.SelectedProductName = productName; // Pass the product name
            productListForm.Show();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            LoadNotifications();
            notificationtimer.Interval = 60000; 
            notificationtimer.Tick += new EventHandler(notificationtimer_Tick);
            notificationtimer.Start();
        }

        private void notificationtimer_Tick(object sender, EventArgs e)
        {
            LoadNotifications();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Notification_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click_1(sender, e);
            }
        }
    }
}
