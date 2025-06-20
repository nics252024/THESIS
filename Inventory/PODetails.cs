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
    public partial class PODetails : Form
    {
        SqlConnection con = new SqlConnection();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlCommand com = new SqlCommand();
        private string PONum, supname, date, status;
        


        public PODetails(string PONO, string suppliername, string transdate, string stats)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            PONum = PONO;
            supname = suppliername;
            date = transdate;
            status = stats;
        }
        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PODetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click_1(sender, e);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StockLevelsList list = (StockLevelsList)Application.OpenForms["StockLevelsList"];
            try
            {
                // Ensure at least one row is selected in the DataGridView
                if (dgvPODetails.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select rows to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(dbcon.DataConnection()))
                {
                    connection.Open();
                    decimal totalAmountPO = 0; // To calculate the total amount for lblAmountPO

                    foreach (DataGridViewRow selectedRow in dgvPODetails.SelectedRows)
                    {
                        // Retrieve the ID value (Primary Key) from the selected row
                        string id = selectedRow.Cells["id"].Value?.ToString();
                        if (string.IsNullOrWhiteSpace(id))
                        {
                            MessageBox.Show("One of the selected rows does not have a valid ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue; // Skip this row and process the next one
                        }

                        // Retrieve and validate qtyPO
                        if (!int.TryParse(selectedRow.Cells["qty"].Value?.ToString(), out int qtyPO) || qtyPO < 0)
                        {
                            MessageBox.Show($"Invalid quantity in row with ID: {id}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue; // Skip this row and process the next one
                        }

                        // Retrieve and validate pricePO
                        string priceText = selectedRow.Cells["price"].Value?.ToString()?.Replace("₱", "").Trim();
                        if (!decimal.TryParse(priceText, out decimal pricePO))
                        {
                            MessageBox.Show($"Invalid price format in row with ID: {id}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue; // Skip this row and process the next one
                        }

                        // Calculate new amountPO
                        decimal newAmountPO = qtyPO * pricePO;

                        // Update the database
                        string query = @"
                    UPDATE tablePODetails
                    SET qtyPO = @QtyPO,
                        amountPO = pricePO * @QtyPO
                    WHERE id = @ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@QtyPO", qtyPO);
                            command.Parameters.AddWithValue("@ID", id);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Update the DataGridView
                                selectedRow.Cells["amountPO"].Value = "₱" + newAmountPO.ToString("N2");
                                totalAmountPO += newAmountPO; // Add to total
                            }
                            else
                            {
                                MessageBox.Show($"No record found with ID: {id}", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    // Update the total amount in lblAmountPO
                  

                    // Trigger any dependent updates
                    list.ReceiveOrder();
                }

                // Refresh the DataGridView after updating all rows
                PurchaseOrderDetails("In Progress");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvPODetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked column is the "Delete" column
                if (dgvPODetails.Columns[e.ColumnIndex].Name == "delete" && e.RowIndex >= 0)
                {
                    // Confirm deletion
                    DialogResult result = MessageBox.Show(
                        "Are you sure you want to delete this PO detail?",
                        "Confirm Deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Get the ID or unique key of the row to delete
                        string poDetailId = dgvPODetails.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        string poNumber = dgvPODetails.Rows[e.RowIndex].Cells["PoNO"].Value.ToString(); // Assuming the PO Number column is available

                        using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                        {
                            con.Open();

                            // Delete the record from tablePODetails
                            string deleteQuery = "DELETE FROM tablePODetails WHERE id = @id";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                            {
                                deleteCmd.Parameters.AddWithValue("@id", poDetailId);
                                int rowsAffected = deleteCmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Update numItem in tablePurchaseOrders
                                    string updateQuery = @"
                                UPDATE tablePurchaseOrders
                                SET numItems = (SELECT COUNT(*) FROM tablePODetails WHERE PoNo = @PoNo)
                                WHERE PoNo = @PoNo";

                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                                    {
                                        updateCmd.Parameters.AddWithValue("@PoNo", poNumber);
                                        updateCmd.ExecuteNonQuery();
                                    }

                                    MessageBox.Show("PO detail deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Refresh the DataGridView after deletion
                                    PurchaseOrderDetails("In Progress");
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete PO detail. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PurchaseOrderDetails(string status)
        {
            int i = 0;
            decimal totalAmount = 0; // Variable to store the total amount

            dgvPODetails.Rows.Clear();

            try
            {
                con.Open();

                string query = @"
  SELECT p.id, p.PoNo, pr.productname, p.pcode, p.qtyPO, p.pricePO, p.amountPO, po.status
  FROM tablePODetails AS p
  INNER JOIN tableProductList AS pr ON p.pcode = pr.pcode
  INNER JOIN tablePurchaseOrders AS po ON po.PoNo = p.PoNo
  WHERE p.PoNo = @PoNo AND po.status = @Status";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@PoNo", lblpoNo.Text);
                    com.Parameters.AddWithValue("@Status", status);

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            string pricePO = "₱" + Convert.ToDecimal(dr["pricePO"]).ToString("N2");
                            string amountPO = "₱" + Convert.ToDecimal(dr["amountPO"]).ToString("N2");

                            // Add the amountPO value to the totalAmount
                            decimal amount = Convert.ToDecimal(dr["amountPO"]);
                            totalAmount += amount;

                            dgvPODetails.Rows.Add(i,
                                                  dr["id"].ToString(),
                                                  dr["PoNo"].ToString(),
                                                  dr["productname"].ToString(),
                                                  dr["pcode"].ToString(),
                                                  dr["qtyPO"].ToString(),
                                                  pricePO,
                                                  amountPO
                                                  );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            // Correct the variable name here
            lblAmountPO.Text = "₱" + totalAmount.ToString("N2");
        }

    }
}
