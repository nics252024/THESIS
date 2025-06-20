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
    public partial class PasswordVerification : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        public string EnteredPassword { get; private set; }

        public PasswordVerification()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            
          
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConfirmAdmin_Click(object sender, EventArgs e)
        {
            EnteredPassword = txtpassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PasswordVerification_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
