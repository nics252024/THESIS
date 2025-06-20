namespace PIERANGELO
{
    partial class SalesReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesReport));
            this.dgvSalesRecord = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cashierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paymentmethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancel = new System.Windows.Forms.DataGridViewImageColumn();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCashier = new System.Windows.Forms.ComboBox();
            this.lblAdminUser = new System.Windows.Forms.Label();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPayment = new System.Windows.Forms.ComboBox();
            this.btndispose = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.buttonBack = new System.Windows.Forms.Button();
            this.txtsearchproduct = new MetroFramework.Controls.MetroTextBox();
            this.btnPrintSales = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesRecord)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSalesRecord
            // 
            this.dgvSalesRecord.AllowUserToAddRows = false;
            this.dgvSalesRecord.AllowUserToResizeColumns = false;
            this.dgvSalesRecord.AllowUserToResizeRows = false;
            this.dgvSalesRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvSalesRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSalesRecord.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSalesRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSalesRecord.ColumnHeadersHeight = 30;
            this.dgvSalesRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSalesRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.transNo,
            this.cashierName,
            this.paymentmethod,
            this.Column2,
            this.Column5,
            this.Column9,
            this.cancel});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesRecord.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSalesRecord.EnableHeadersVisualStyles = false;
            this.dgvSalesRecord.Location = new System.Drawing.Point(1, 183);
            this.dgvSalesRecord.Name = "dgvSalesRecord";
            this.dgvSalesRecord.ReadOnly = true;
            this.dgvSalesRecord.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvSalesRecord.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSalesRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSalesRecord.Size = new System.Drawing.Size(1097, 513);
            this.dgvSalesRecord.TabIndex = 50;
            this.dgvSalesRecord.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalesRecord_CellContentDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 41;
            // 
            // transNo
            // 
            this.transNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.transNo.HeaderText = "TRANSACTION NO";
            this.transNo.Name = "transNo";
            this.transNo.ReadOnly = true;
            // 
            // cashierName
            // 
            this.cashierName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cashierName.HeaderText = "CASHIER";
            this.cashierName.Name = "cashierName";
            this.cashierName.ReadOnly = true;
            this.cashierName.Width = 94;
            // 
            // paymentmethod
            // 
            this.paymentmethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.paymentmethod.DefaultCellStyle = dataGridViewCellStyle2;
            this.paymentmethod.HeaderText = "PAYMENT METHOD";
            this.paymentmethod.Name = "paymentmethod";
            this.paymentmethod.ReadOnly = true;
            this.paymentmethod.Width = 170;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "DATE/TIME";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 113;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column5.HeaderText = "TOTAL QUANTITY";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 158;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column9.HeaderText = "TOTAL AMOUNT";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 149;
            // 
            // cancel
            // 
            this.cancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cancel.HeaderText = "";
            this.cancel.Image = ((System.Drawing.Image)(resources.GetObject("cancel.Image")));
            this.cancel.Name = "cancel";
            this.cancel.ReadOnly = true;
            this.cancel.Visible = false;
            this.cancel.Width = 5;
            // 
            // dateto
            // 
            this.dateto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateto.Location = new System.Drawing.Point(952, 142);
            this.dateto.MaxDate = new System.DateTime(2029, 12, 31, 0, 0, 0, 0);
            this.dateto.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(118, 22);
            this.dateto.TabIndex = 53;
            this.dateto.Value = new System.DateTime(2024, 12, 1, 0, 0, 0, 0);
            this.dateto.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // datefrom
            // 
            this.datefrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datefrom.Location = new System.Drawing.Point(828, 143);
            this.datefrom.MaxDate = new System.DateTime(2029, 12, 31, 0, 0, 0, 0);
            this.datefrom.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(118, 22);
            this.datefrom.TabIndex = 52;
            this.datefrom.Value = new System.DateTime(2024, 12, 1, 0, 0, 0, 0);
            this.datefrom.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(825, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 15);
            this.label2.TabIndex = 51;
            this.label2.Text = "Filter by Date (From - To)";
            // 
            // cbCashier
            // 
            this.cbCashier.BackColor = System.Drawing.Color.White;
            this.cbCashier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCashier.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCashier.FormattingEnabled = true;
            this.cbCashier.Location = new System.Drawing.Point(448, 83);
            this.cbCashier.Name = "cbCashier";
            this.cbCashier.Size = new System.Drawing.Size(242, 23);
            this.cbCashier.TabIndex = 55;
            this.cbCashier.SelectedIndexChanged += new System.EventHandler(this.cbCashier_SelectedIndexChanged);
            this.cbCashier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCashier_KeyPress);
            // 
            // lblAdminUser
            // 
            this.lblAdminUser.AutoSize = true;
            this.lblAdminUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminUser.ForeColor = System.Drawing.Color.Black;
            this.lblAdminUser.Location = new System.Drawing.Point(445, 61);
            this.lblAdminUser.Name = "lblAdminUser";
            this.lblAdminUser.Size = new System.Drawing.Size(121, 15);
            this.lblAdminUser.TabIndex = 56;
            this.lblAdminUser.Text = "Select Cashier Name";
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.lblTotalSales.Location = new System.Drawing.Point(17, 27);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(56, 37);
            this.lblTotalSales.TabIndex = 57;
            this.lblTotalSales.Text = "0.0";
            this.lblTotalSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblTotalSales);
            this.panel2.Location = new System.Drawing.Point(17, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 74);
            this.panel2.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(8, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "Total Sales";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(44, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 33);
            this.button1.TabIndex = 77;
            this.button1.Text = "Sales Report";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(445, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 15);
            this.label1.TabIndex = 79;
            this.label1.Text = "Select Payment Method";
            // 
            // cbPayment
            // 
            this.cbPayment.BackColor = System.Drawing.Color.White;
            this.cbPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPayment.FormattingEnabled = true;
            this.cbPayment.Location = new System.Drawing.Point(448, 152);
            this.cbPayment.Name = "cbPayment";
            this.cbPayment.Size = new System.Drawing.Size(242, 23);
            this.cbPayment.TabIndex = 78;
            this.cbPayment.SelectedIndexChanged += new System.EventHandler(this.cbPayment_SelectedIndexChanged);
            // 
            // btndispose
            // 
            this.btndispose.BackColor = System.Drawing.Color.Snow;
            this.btndispose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btndispose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndispose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndispose.ForeColor = System.Drawing.Color.Black;
            this.btndispose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndispose.Location = new System.Drawing.Point(959, 8);
            this.btndispose.Name = "btndispose";
            this.btndispose.Size = new System.Drawing.Size(128, 25);
            this.btndispose.TabIndex = 80;
            this.btndispose.Text = "Pending Transaction";
            this.btndispose.UseVisualStyleBackColor = false;
            this.btndispose.Click += new System.EventHandler(this.btndispose_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Visible = false;
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(0, -1);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.buttonBack.Size = new System.Drawing.Size(38, 40);
            this.buttonBack.TabIndex = 76;
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click_1);
            // 
            // txtsearchproduct
            // 
            // 
            // 
            // 
            this.txtsearchproduct.CustomButton.Image = null;
            this.txtsearchproduct.CustomButton.Location = new System.Drawing.Point(387, 1);
            this.txtsearchproduct.CustomButton.Name = "";
            this.txtsearchproduct.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtsearchproduct.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtsearchproduct.CustomButton.TabIndex = 1;
            this.txtsearchproduct.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtsearchproduct.CustomButton.UseSelectable = true;
            this.txtsearchproduct.CustomButton.Visible = false;
            this.txtsearchproduct.DisplayIcon = true;
            this.txtsearchproduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchproduct.Icon = ((System.Drawing.Image)(resources.GetObject("txtsearchproduct.Icon")));
            this.txtsearchproduct.Lines = new string[0];
            this.txtsearchproduct.Location = new System.Drawing.Point(17, 140);
            this.txtsearchproduct.MaxLength = 32767;
            this.txtsearchproduct.Name = "txtsearchproduct";
            this.txtsearchproduct.PasswordChar = '\0';
            this.txtsearchproduct.PromptText = "Search Transaction";
            this.txtsearchproduct.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtsearchproduct.SelectedText = "";
            this.txtsearchproduct.SelectionLength = 0;
            this.txtsearchproduct.SelectionStart = 0;
            this.txtsearchproduct.ShortcutsEnabled = true;
            this.txtsearchproduct.Size = new System.Drawing.Size(411, 25);
            this.txtsearchproduct.TabIndex = 61;
            this.txtsearchproduct.UseCustomBackColor = true;
            this.txtsearchproduct.UseSelectable = true;
            this.txtsearchproduct.WaterMark = "Search Transaction";
            this.txtsearchproduct.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchproduct.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtsearchproduct.TextChanged += new System.EventHandler(this.txtsearchproduct_TextChanged);
            // 
            // btnPrintSales
            // 
            this.btnPrintSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnPrintSales.FlatAppearance.BorderSize = 0;
            this.btnPrintSales.ForeColor = System.Drawing.Color.White;
            this.btnPrintSales.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintSales.Image")));
            this.btnPrintSales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintSales.Location = new System.Drawing.Point(959, 41);
            this.btnPrintSales.Name = "btnPrintSales";
            this.btnPrintSales.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPrintSales.Size = new System.Drawing.Size(128, 35);
            this.btnPrintSales.TabIndex = 54;
            this.btnPrintSales.Text = "Print Preview";
            this.btnPrintSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintSales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintSales.UseVisualStyleBackColor = false;
            this.btnPrintSales.Click += new System.EventHandler(this.btnPrintSales_Click);
            // 
            // SalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1099, 658);
            this.ControlBox = false;
            this.Controls.Add(this.btndispose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPayment);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.txtsearchproduct);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblAdminUser);
            this.Controls.Add(this.cbCashier);
            this.Controls.Add(this.btnPrintSales);
            this.Controls.Add(this.dateto);
            this.Controls.Add(this.datefrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvSalesRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesRecord)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvSalesRecord;
        public System.Windows.Forms.DateTimePicker dateto;
        public System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cbCashier;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lblAdminUser;
        private MetroFramework.Controls.MetroTextBox txtsearchproduct;
        public System.Windows.Forms.Button btnPrintSales;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn transNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cashierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentmethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewImageColumn cancel;
        public System.Windows.Forms.Button btndispose;
    }
}