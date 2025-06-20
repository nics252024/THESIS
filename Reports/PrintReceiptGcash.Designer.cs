namespace PIERANGELO
{
    partial class PrintReceiptGcash
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
            this.rvReceiptGcash = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvReceiptGcash
            // 
            this.rvReceiptGcash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvReceiptGcash.Location = new System.Drawing.Point(0, 0);
            this.rvReceiptGcash.Name = "rvReceiptGcash";
            this.rvReceiptGcash.ServerReport.BearerToken = null;
            this.rvReceiptGcash.Size = new System.Drawing.Size(391, 471);
            this.rvReceiptGcash.TabIndex = 0;
            // 
            // PrintReceiptGcash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 471);
            this.Controls.Add(this.rvReceiptGcash);
            this.Name = "PrintReceiptGcash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintReceiptGcash";
            this.Load += new System.EventHandler(this.PrintReceiptGcash_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReceiptGcash;
    }
}