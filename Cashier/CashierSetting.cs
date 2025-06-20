using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class CashierSetting : Form
    {
        private Form currentForm;
        public CashierSetting()
        {
            InitializeComponent();
        }
        private void ChangeForm(Form newForm)
        {
            if (currentForm != null)
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
        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            LogHistory lh = new LogHistory();
            lh.cbActions.Enabled = false;
            lh.cbuser.Enabled = false;
            lh.datefrom.Enabled = false;
            lh.dateto.Enabled = false;
            lh.dtadjust1.Enabled = false;
            lh.dtadjust2.Enabled = false;
            lh.cbPayment.Enabled = false;
            lh.cbCashier.Enabled = false;
            lh.metroTabControl1.TabPages.Remove(lh.tabPage2);
            lh.metroTabControl1.TabPages.Remove(lh.tabPage3);
            lh.metroTabControl1.TabPages.Remove(lh.tabPage4);
            
            lh.cbuser.Text = lblUser.Text;
            ChangeForm(lh);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CreateDiscount dc = new CreateDiscount();
            dc.btnsave.Hide();
            dc.dgvDiscount.Columns["Edit"].Visible = false;
            dc.dgvDiscount.Columns["Delete"].Visible = false;
            ChangeForm(dc);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDateee.Text = DateTime.Now.ToLongDateString();
        }
    }
}
