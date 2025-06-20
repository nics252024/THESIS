namespace PIERANGELO
{
    partial class ReceiptPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rvReceipt = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvReceipt
            // 
            this.rvReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvReceipt.Location = new System.Drawing.Point(0, 0);
            this.rvReceipt.Name = "rvReceipt";
            this.rvReceipt.ServerReport.BearerToken = null;
            this.rvReceipt.Size = new System.Drawing.Size(391, 471);
            this.rvReceipt.TabIndex = 0;
            // 
            // ReceiptPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 471);
            this.Controls.Add(this.rvReceipt);
            this.Name = "ReceiptPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt";
            this.Load += new System.EventHandler(this.Receipt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceiptPrint_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReceipt;
    }
}