using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace PIERANGELO
{
    public partial class CancelOrder : Form
    {
        ReturnExchangeForm ex;
        public CancelOrder(ReturnExchangeForm ex)
        {
            InitializeComponent();
            this.ex = ex;
            this.KeyPreview = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbATI_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if ((cbATI.Text != String.Empty) && (lblqty.Text != String.Empty) && (txtReasons.Text != String.Empty))
                {
                    if (int.Parse(lblqty.Text) >= int.Parse(txtCancelQty.Text))
                    {
                        CancelVerification admin = new CancelVerification(this);
                        admin.ShowDialog();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCancelQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void RefreshProductList()
        {
            ex.ShowTransactionNO();
        }

        private void CancelOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCancelOrder_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
