using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    internal class DatabaseConnection
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        private double getdailysales;
        private int critical;
        private double totalsales;
        private int stockonhand;
        private int totalProduct;
        public int totalTrans;
        public string connection;


        
        public string DataConnection()
        {
            connection = "Data Source=DESKTOP-E99V653\\SQLEXPRESS;Initial Catalog=PIERANGELO;Integrated Security=True";
            return connection;
        }


        public double DailySales()
        {
            double getdailysales = 0; // Initialize the variable
            string transdate = DateTime.Now.ToString("yyyy-MM-dd"); // Current date

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(
                    "SELECT ISNULL(SUM(total), 0) as total " +
                    "FROM tableSales " +
                    "WHERE CAST(transdate AS DATE) = @transdate AND status LIKE 'Sold'", con))
                {
                    com.Parameters.AddWithValue("@transdate", transdate);

                    getdailysales = Convert.ToDouble(com.ExecuteScalar());
                }
            }

            return getdailysales;
        }


        public double ReturnandExchange()
        {
            int count = 0;
            string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM tableCancel WHERE transdate >= @todayStart AND transdate < @tomorrow AND status = 'Complete'", con))
                {
                    com.Parameters.AddWithValue("@todayStart", todayDate);
                    com.Parameters.AddWithValue("@tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

                    count = int.Parse(com.ExecuteScalar().ToString());
                }
                con.Close();
            }

            return count;
        }
        public double TotalSales()
        {

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("SELECT ISNULL(SUM(total),0) as total FROM tableSales WHERE status LIKE 'Sold'", con))
                {
                    totalsales = Convert.ToDouble(com.ExecuteScalar().ToString());
                    con.Close();
                }
            }

            return totalsales;
        }

        public int TotalTransaction()
        {
            int totalTrans = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(
                        "SELECT COUNT(DISTINCT transactionNo) FROM tableSales WHERE status = 'Sold' AND CONVERT(date, transdate) = CONVERT(date, GETDATE())", con))
                    {
                        totalTrans = (int)com.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log it)
                    throw new ApplicationException("Error fetching total transactions for today", ex);
                }
                finally
                {
                    con.Close();
                }
            }

            return totalTrans;

        }  
        public double Vat()
        {
            double vat = 0;
            con.ConnectionString = DataConnection();
            con.Open();
            com = new SqlCommand("SELECT * FROM tableVAT", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                vat = Double.Parse(dr["vat"].ToString());
            }
            dr.Close();
            con.Close();
            return vat;

        }
        public string PasswordValue(string user)
        {
            string password = "";
            con.ConnectionString = DataConnection();
            con.Open();
            com = new SqlCommand("SELECT * FROM tableUserAccount WHERE username = @username", con);
            com.Parameters.AddWithValue("@username", user);
            dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                password = dr["password"].ToString();
            }
            dr.Close();
            con.Close();
            return password;
        }
        public string GenerateTransID()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd");

            Random random = new Random();
            int randomnumber = random.Next(1000,9999);

            string transactionId = timestamp + randomnumber.ToString();

            return transactionId;
        }
    }
}
