using PIERANGELO.Inventory;
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

    public partial class RODetails : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
       
        

        public RODetails()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            ReceiveOrder();
        }
        private void btnROreversal_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to reverse this RO?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open the connection to the database
                    con.Open();

                    // Get the selected PO number from the label or DataGridView
                    string poNo = lblPoNo.Text;  // Assuming you have lblPoNo showing the current PO number

                    // Revert the RO details to the original state
                    string updateQuery = @"
                        UPDATE tablePODetails
                        SET qtyRO = 0, amountRO = 0, priceRO = 0
                        WHERE PoNo = @PoNo";

                    using (SqlCommand com = new SqlCommand(updateQuery, con))
                    {
                        // Pass the selected PoNo to the query
                        com.Parameters.AddWithValue("@PoNo", poNo);

                        // Execute the update query to reset qtyRO, amountRO, and priceRO
                        com.ExecuteNonQuery();
                    }

                    // Optionally, update the status of the Purchase Order to "In Progress" or some other state
                    string updateStatusQuery = @"
                    UPDATE tablePurchaseOrders
                    SET status = 'In Progress'
                    WHERE PoNo = @PoNo";

                    using (SqlCommand statusCommand = new SqlCommand(updateStatusQuery, con))
                    {
                        // Pass the PoNo to update the status
                        statusCommand.Parameters.AddWithValue("@PoNo", poNo);
                        statusCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Receive Order Reversal has been completed.", "RO Reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView after the reversal
                    ReceiveOrder();  // Assuming you have a method to reload the DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while performing RO reversal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public void ReceiveOrder()
        {
            try
            {
                int rowIndex = 0; // Counter for row numbers
                decimal totalAmount = 0; // To calculate the total amount
                dgvRO.Rows.Clear(); // Clear previous rows from DataGridView

                // Check and open the connection
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Updated query without SUM calculation
                string query = @"
        SELECT p.PoNo, p.productname, p.pcode, p.qtyPO, p.pricePO, p.amountPO,
               pr.qty AS qty, p.priceRO, p.expirydate, p.status
        FROM tablePODetails AS p
        INNER JOIN tableProductList AS pr ON p.pcode = pr.pcode
        WHERE p.PoNo = @PoNo";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@PoNo", lblPoNo.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rowIndex++;

                            // Retrieve monetary values and handle nulls
                            decimal pricePO = reader["pricePO"] != DBNull.Value ? Convert.ToDecimal(reader["pricePO"]) : 0;
                            decimal amountPO = reader["amountPO"] != DBNull.Value ? Convert.ToDecimal(reader["amountPO"]) : 0;
                            decimal priceRO = reader["priceRO"] != DBNull.Value ? Convert.ToDecimal(reader["priceRO"]) : 0;

                            // Retrieve qty and calculate amountRO
                            int qty = reader["qty"] != DBNull.Value ? Convert.ToInt32(reader["qty"]) : 0;
                            decimal amountRO = qty * priceRO;

                            // Add amountRO to totalAmount
                            totalAmount += amountRO;

                            // Format monetary values for display
                            string formattedPricePO = pricePO.ToString("C2");
                            string formattedAmountPO = amountPO.ToString("C2");
                            string formattedPriceRO = priceRO.ToString("C2");
                            string formattedAmountRO = amountRO.ToString("C2");

                            // Handle and format expiry date
                            string expiry;
                            if (reader["expirydate"] != DBNull.Value)
                            {
                                expiry = reader["expirydate"].ToString() == "No Expiry"
                                    ? "No Expiry"
                                    : Convert.ToDateTime(reader["expirydate"]).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                string status = reader["status"].ToString();
                                expiry = (status == "Partial" || status == "Fulfilled") ? "No Expiry" : "";
                            }

                            // Add row to the DataGridView
                            dgvRO.Rows.Add(
                                rowIndex,
                                reader["PoNo"].ToString(),
                                reader["productname"].ToString(),
                                reader["pcode"].ToString(),
                                reader["qtyPO"].ToString(),
                                formattedPricePO,
                                formattedAmountPO,
                                qty.ToString(),
                                formattedPriceRO,
                                formattedAmountRO,
                                expiry,
                                reader["status"].ToString()
                            );
                        }
                    }
                }

                // Display the total amount in the label
                lbltotalamountolRO.Text = "₱" + totalAmount.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message); // Display error message
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close(); // Ensure the connection is closed after operation
                }
            }
        }



        private void btnSaveRODetails_Click(object sender, EventArgs e)
        {
            StockLevelsList stock = (StockLevelsList)Application.OpenForms["StockLevelsList"];
            try
            {
                if (MessageBox.Show("Are you sure you want to save the RO details?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    decimal totalROAmount = 0;
                    string currentPoNo = null;
                    foreach (DataGridViewRow row in dgvRO.Rows)
                    {
                        if (row.Cells["PoNo"].Value != null && row.Cells["productname"].Value != null)
                        {
                            string poNo = row.Cells["PoNo"].Value.ToString();
                            currentPoNo = poNo;
                            string productName = row.Cells["productname"].Value.ToString();
                            string productcode = row.Cells["pcode"].Value.ToString();
                            int qtyRO = row.Cells["qtyRO"].Value != null ? Convert.ToInt32(row.Cells["qtyRO"].Value) : 0;
                            decimal priceRO = row.Cells["priceRO"].Value != null ? Convert.ToDecimal(row.Cells["priceRO"].Value) : 0;
                            decimal amountRO = row.Cells["amountRO"].Value != null ? Convert.ToDecimal(row.Cells["amountRO"].Value) : 0;
                            string expiryValue = row.Cells["expirydate"].Value != null ? row.Cells["expirydate"].Value.ToString() : "";
                            DateTime? expiryDate = null; // Use nullable DateTime

                            // Set expiryDate based on the input value
                            if (expiryValue.Equals("No Expiry", StringComparison.OrdinalIgnoreCase) ||
                                expiryValue.Equals("NA", StringComparison.OrdinalIgnoreCase))
                            {
                                expiryDate = null; // Store NULL for "No Expiry" or "NA"
                            }
                            else if (!string.IsNullOrEmpty(expiryValue))
                            {
                                // Try to parse the expiry date in different formats
                                if (!DateTime.TryParse(expiryValue, out DateTime parsedDate))
                                {
                                    try
                                    {
                                        // If user entered a worded date (e.g., January 1, 2025), try to parse it
                                        expiryDate = DateTime.Parse(expiryValue);
                                    }
                                    catch
                                    {
                                        MessageBox.Show($"Invalid date format for expiry date: {expiryValue}. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    expiryDate = parsedDate; // Assign the parsed date
                                }
                            }

                            totalROAmount += amountRO;

                            string status = "";
                            DataGridViewCheckBoxCell checkBoxPartial = row.Cells["partialselect"] as DataGridViewCheckBoxCell;
                            DataGridViewCheckBoxCell checkBoxFull = row.Cells["allselect"] as DataGridViewCheckBoxCell;
                            if (checkBoxFull != null && checkBoxFull.Value != null && (bool)checkBoxFull.Value)
                            {
                                status = "Fulfilled";
                            }
                            else if (checkBoxPartial != null && checkBoxPartial.Value != null && (bool)checkBoxPartial.Value)
                            {
                                status = "Partial";
                            }
                            else
                            {
                                status = "Pending";
                            }

                            string query = @"
                    UPDATE tablePODetails
                    SET qtyRO = @qtyRO, priceRO = @priceRO, amountRO = @amountRO, expirydate = @expirydate, status = @status
                    WHERE PoNo = @PoNo AND productname = @productname";

                            using (SqlCommand com = new SqlCommand(query, con))
                            {
                                com.Parameters.AddWithValue("@qtyRO", qtyRO);
                                com.Parameters.AddWithValue("@priceRO", priceRO);
                                com.Parameters.AddWithValue("@amountRO", amountRO);
                                com.Parameters.AddWithValue("@expirydate", (object)expiryDate ?? DBNull.Value);  // Store NULL for "No Expiry" or use parsed date
                                com.Parameters.AddWithValue("@PoNo", poNo);
                                com.Parameters.AddWithValue("@productname", productName);
                                com.Parameters.AddWithValue("@status", status);  // Update with the determined status

                                // Execute the update query for tablePODetails
                                com.ExecuteNonQuery();
                            }

                            // Update the qty in tableProductList based on pcode
                            string updateQtyInProductListQuery = @"
                    UPDATE tableProductList
                    SET qty = CASE WHEN qty + @qtyRO < 0 THEN 0 ELSE qty + @qtyRO END
                    WHERE pcode = @pcode";

                            using (SqlCommand updateQtyCmd = new SqlCommand(updateQtyInProductListQuery, con))
                            {
                                updateQtyCmd.Parameters.AddWithValue("@qtyRO", qtyRO);
                                updateQtyCmd.Parameters.AddWithValue("@pcode", productcode);

                                // Execute the update query for tableProductList
                                updateQtyCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update tablePurchaseOrders: total amount, delivered date, and status
                    if (!string.IsNullOrEmpty(currentPoNo))  // Ensure PoNo is available before updating
                    {
                        string updatePurchaseOrdersQuery = @"
                    UPDATE tablePurchaseOrders
                    SET totalamountRO = @totalAmountRO, delivereddate = @deliveredDate, status = 'Delivered'
                    WHERE PoNo = @PoNo";

                        using (SqlCommand updatePurchaseOrdersCmd = new SqlCommand(updatePurchaseOrdersQuery, con))
                        {
                            updatePurchaseOrdersCmd.Parameters.AddWithValue("@totalAmountRO", totalROAmount);
                            updatePurchaseOrdersCmd.Parameters.AddWithValue("@deliveredDate", DateTime.Now);  // Set the delivered date to the current date
                            updatePurchaseOrdersCmd.Parameters.AddWithValue("@PoNo", currentPoNo);  // Use the last valid PoNo

                            // Execute the update query for tablePurchaseOrders
                            updatePurchaseOrdersCmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("RO details have been saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvRO.Columns["partialselect"].Visible = false;
                    dgvRO.Columns["allselect"].Visible = false;
                    CalculateTotalAmounts();
                    stock.PurchaseOrder();
                    stock.ReceiveOrder();
                    ReceiveOrder();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving RO details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }
        }

        private void dgvRO_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
            // Check if the cell that was edited is either priceRO or qtyRO
            if (e.RowIndex >= 0 && (e.ColumnIndex == dgvRO.Columns["priceRO"].Index || e.ColumnIndex == dgvRO.Columns["qtyRO"].Index))
            {
                DataGridViewRow row = dgvRO.Rows[e.RowIndex];

                // Ensure qtyRO and priceRO are valid values
                if (row.Cells["qtyRO"].Value != null && row.Cells["priceRO"].Value != null &&
                    int.TryParse(row.Cells["qtyRO"].Value.ToString(), out int qtyRO) &&
                    decimal.TryParse(row.Cells["priceRO"].Value.ToString(), out decimal priceRO))
                {
                    // Calculate amountRO as qtyRO * priceRO
                    decimal amountRO = qtyRO * priceRO;

                    // Set the calculated amount to the amountRO cell
                    row.Cells["amountRO"].Value = amountRO;
                }
                else
                {
                    // If qtyRO or priceRO is invalid, set amountRO to 0
                    row.Cells["amountRO"].Value = 0;
                }   
            }

            // Optional: Reset the read-only status of QtyRO cell here if needed
            // This is if you want to revert back to read-only after editing
            if (e.ColumnIndex == dgvRO.Columns["qtyRO"].Index)
            {
                dgvRO.Rows[e.RowIndex].Cells["qtyRO"].ReadOnly = true;
            }
        }
        public void CalculateTotalAmounts()
        {
            decimal totalAmount = 0;  // Initialize the total amount variable

            // Loop through all rows in the DataGridView
            foreach (DataGridViewRow row in dgvRO.Rows)
            {
                if (row.Cells["amountRO"].Value != null)  // Ensure the cell is not null
                {
                    // Remove the currency symbol and parse the amount to decimal
                    decimal amountPo = Convert.ToDecimal(row.Cells["amountRO"].Value.ToString().Replace("₱", ""));
                    totalAmount += amountPo;  // Add the amount to the total
                }
            }

        }

        private void dgvRO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Partial select condition
                if (e.ColumnIndex == dgvRO.Columns["partialselect"].Index)
                {
                    // Toggle the checkbox state for partial select
                    DataGridViewCheckBoxCell partialCheckBoxCell = (DataGridViewCheckBoxCell)dgvRO.Rows[e.RowIndex].Cells["partialselect"];
                    bool isPartialSelected = !(partialCheckBoxCell.Value != null && (bool)partialCheckBoxCell.Value);
                    partialCheckBoxCell.Value = isPartialSelected;

                    if (isPartialSelected)
                    {
                        // Enable editing for qtyRO and priceRO
                        dgvRO.Rows[e.RowIndex].Cells["qtyRO"].ReadOnly = false;
                        dgvRO.Rows[e.RowIndex].Cells["priceRO"].ReadOnly = false;

                        // Disable the allselect checkbox
                        dgvRO.Rows[e.RowIndex].Cells["allselect"].Value = false;
                        dgvRO.Rows[e.RowIndex].Cells["allselect"].ReadOnly = true;

                        // Set the status to "Partial"
                        dgvRO.Rows[e.RowIndex].Cells["status"].Value = "Partial";

                        // Focus on qtyRO for editing
                        dgvRO.CurrentCell = dgvRO.Rows[e.RowIndex].Cells["qtyRO"];
                        dgvRO.BeginEdit(true);
                    }
                    else
                    {
                        // Reset to readonly
                        dgvRO.Rows[e.RowIndex].Cells["qtyRO"].ReadOnly = true;
                        dgvRO.Rows[e.RowIndex].Cells["priceRO"].ReadOnly = true;

                        // Re-enable the allselect checkbox
                        dgvRO.Rows[e.RowIndex].Cells["allselect"].ReadOnly = false;

                        // Clear the status if both checkboxes are unchecked
                        dgvRO.Rows[e.RowIndex].Cells["status"].Value = "";
                    }
                }

                // All select condition
                if (e.ColumnIndex == dgvRO.Columns["allselect"].Index)
                {
                    // Toggle the checkbox state for all select
                    DataGridViewCheckBoxCell allSelectCheckBoxCell = (DataGridViewCheckBoxCell)dgvRO.Rows[e.RowIndex].Cells["allselect"];
                    bool isAllSelected = !(allSelectCheckBoxCell.Value != null && (bool)allSelectCheckBoxCell.Value);
                    allSelectCheckBoxCell.Value = isAllSelected;

                    if (isAllSelected)
                    {
                        // Set qtyRO value equal to qtyPO, or 0 if qtyPO is null
                        if (dgvRO.Rows[e.RowIndex].Cells["qtyPO"].Value != null &&
                            int.TryParse(dgvRO.Rows[e.RowIndex].Cells["qtyPO"].Value.ToString(), out int qtyPO))
                        {
                            dgvRO.Rows[e.RowIndex].Cells["qtyRO"].Value = qtyPO;
                        }
                        else
                        {
                            dgvRO.Rows[e.RowIndex].Cells["qtyRO"].Value = 0;
                        }

                        // Enable editing for qtyRO and priceRO
                        dgvRO.Rows[e.RowIndex].Cells["qtyRO"].ReadOnly = false;
                        dgvRO.Rows[e.RowIndex].Cells["priceRO"].ReadOnly = false;

                        // Disable the partialselect checkbox
                        dgvRO.Rows[e.RowIndex].Cells["partialselect"].Value = false;
                        dgvRO.Rows[e.RowIndex].Cells["partialselect"].ReadOnly = true;

                        // Set the status to "Fulfilled"
                        dgvRO.Rows[e.RowIndex].Cells["status"].Value = "Fulfilled";

                        // Focus on priceRO for editing
                        dgvRO.CurrentCell = dgvRO.Rows[e.RowIndex].Cells["priceRO"];
                        dgvRO.BeginEdit(true);
                    }
                    else
                    {
                        // Reset to readonly
                        dgvRO.Rows[e.RowIndex].Cells["qtyRO"].ReadOnly = true;
                        dgvRO.Rows[e.RowIndex].Cells["priceRO"].ReadOnly = true;

                        // Re-enable the partialselect checkbox
                        dgvRO.Rows[e.RowIndex].Cells["partialselect"].ReadOnly = false;

                        // Clear the status if both checkboxes are unchecked
                        dgvRO.Rows[e.RowIndex].Cells["status"].Value = "";
                    }
                }
            }
        }
        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
