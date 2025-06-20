namespace PIERANGELO
{
    partial class RODetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RODetails));
            this.lblPoNo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvRO = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expirydate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partialselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.dptrans = new System.Windows.Forms.DateTimePicker();
            this.btnROreversal = new System.Windows.Forms.Button();
            this.btnSaveRODetails = new System.Windows.Forms.Button();
            this.lbltotalamountolRO = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblsupplier = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRO)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPoNo
            // 
            this.lblPoNo.AutoSize = true;
            this.lblPoNo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoNo.ForeColor = System.Drawing.Color.Red;
            this.lblPoNo.Location = new System.Drawing.Point(151, 61);
            this.lblPoNo.Name = "lblPoNo";
            this.lblPoNo.Size = new System.Drawing.Size(56, 20);
            this.lblPoNo.TabIndex = 102;
            this.lblPoNo.Text = "PO NO";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(27, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 101;
            this.label4.Text = "Purchase No.";
            // 
            // dgvRO
            // 
            this.dgvRO.AllowUserToAddRows = false;
            this.dgvRO.AllowUserToResizeColumns = false;
            this.dgvRO.AllowUserToResizeRows = false;
            this.dgvRO.BackgroundColor = System.Drawing.Color.White;
            this.dgvRO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRO.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRO.ColumnHeadersHeight = 30;
            this.dgvRO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.pono,
            this.productname,
            this.pcode,
            this.qtyPO,
            this.Column3,
            this.Column4,
            this.qtyRO,
            this.priceRO,
            this.amountRO,
            this.expirydate,
            this.status,
            this.partialselect,
            this.allselect});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRO.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRO.EnableHeadersVisualStyles = false;
            this.dgvRO.Location = new System.Drawing.Point(0, 136);
            this.dgvRO.Name = "dgvRO";
            this.dgvRO.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRO.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRO.Size = new System.Drawing.Size(1095, 407);
            this.dgvRO.TabIndex = 103;
            this.dgvRO.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRO_CellContentClick);
            this.dgvRO.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRO_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.HeaderText = "#";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 37;
            // 
            // pono
            // 
            this.pono.HeaderText = "PONUMBER";
            this.pono.Name = "pono";
            this.pono.Visible = false;
            // 
            // productname
            // 
            this.productname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.productname.HeaderText = "PRODUCT NAME";
            this.productname.Name = "productname";
            this.productname.ReadOnly = true;
            // 
            // pcode
            // 
            this.pcode.HeaderText = "PCODE";
            this.pcode.Name = "pcode";
            this.pcode.Visible = false;
            // 
            // qtyPO
            // 
            this.qtyPO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.qtyPO.HeaderText = "QTY PO";
            this.qtyPO.Name = "qtyPO";
            this.qtyPO.ReadOnly = true;
            this.qtyPO.Width = 71;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "PRICE PO";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "AMOUNT PO";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 101;
            // 
            // qtyRO
            // 
            this.qtyRO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.qtyRO.HeaderText = "QTY RO";
            this.qtyRO.Name = "qtyRO";
            this.qtyRO.ReadOnly = true;
            this.qtyRO.Width = 71;
            // 
            // priceRO
            // 
            this.priceRO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.priceRO.HeaderText = "PRICE RO";
            this.priceRO.Name = "priceRO";
            this.priceRO.ReadOnly = true;
            this.priceRO.Width = 80;
            // 
            // amountRO
            // 
            this.amountRO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.amountRO.HeaderText = "AMOUNT RO";
            this.amountRO.Name = "amountRO";
            this.amountRO.Width = 101;
            // 
            // expirydate
            // 
            this.expirydate.HeaderText = "EXPIRY DATE";
            this.expirydate.Name = "expirydate";
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.status.HeaderText = "STATUS";
            this.status.Name = "status";
            this.status.Width = 72;
            // 
            // partialselect
            // 
            this.partialselect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.partialselect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.partialselect.HeaderText = "PARTIAL";
            this.partialselect.Name = "partialselect";
            this.partialselect.Width = 57;
            // 
            // allselect
            // 
            this.allselect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.allselect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.allselect.HeaderText = "ALL";
            this.allselect.Name = "allselect";
            this.allselect.Width = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 111;
            this.label3.Text = "Delivered Date";
            // 
            // dptrans
            // 
            this.dptrans.CalendarMonthBackground = System.Drawing.SystemColors.Control;
            this.dptrans.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptrans.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dptrans.Location = new System.Drawing.Point(155, 94);
            this.dptrans.MaxDate = new System.DateTime(2029, 12, 31, 0, 0, 0, 0);
            this.dptrans.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dptrans.Name = "dptrans";
            this.dptrans.Size = new System.Drawing.Size(140, 25);
            this.dptrans.TabIndex = 114;
            // 
            // btnROreversal
            // 
            this.btnROreversal.BackColor = System.Drawing.Color.LightGray;
            this.btnROreversal.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnROreversal.FlatAppearance.BorderSize = 0;
            this.btnROreversal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnROreversal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnROreversal.ForeColor = System.Drawing.Color.Black;
            this.btnROreversal.Image = ((System.Drawing.Image)(resources.GetObject("btnROreversal.Image")));
            this.btnROreversal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnROreversal.Location = new System.Drawing.Point(957, 566);
            this.btnROreversal.Name = "btnROreversal";
            this.btnROreversal.Size = new System.Drawing.Size(126, 34);
            this.btnROreversal.TabIndex = 129;
            this.btnROreversal.Text = "    Cancel";
            this.btnROreversal.UseVisualStyleBackColor = false;
            this.btnROreversal.Click += new System.EventHandler(this.btnROreversal_Click);
            // 
            // btnSaveRODetails
            // 
            this.btnSaveRODetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnSaveRODetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveRODetails.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRODetails.ForeColor = System.Drawing.Color.Lavender;
            this.btnSaveRODetails.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRODetails.Image")));
            this.btnSaveRODetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveRODetails.Location = new System.Drawing.Point(817, 566);
            this.btnSaveRODetails.Name = "btnSaveRODetails";
            this.btnSaveRODetails.Size = new System.Drawing.Size(134, 34);
            this.btnSaveRODetails.TabIndex = 130;
            this.btnSaveRODetails.Text = "    Save Details";
            this.btnSaveRODetails.UseVisualStyleBackColor = false;
            this.btnSaveRODetails.Click += new System.EventHandler(this.btnSaveRODetails_Click);
            // 
            // lbltotalamountolRO
            // 
            this.lbltotalamountolRO.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalamountolRO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbltotalamountolRO.Location = new System.Drawing.Point(176, 573);
            this.lbltotalamountolRO.Name = "lbltotalamountolRO";
            this.lbltotalamountolRO.Size = new System.Drawing.Size(114, 20);
            this.lbltotalamountolRO.TabIndex = 134;
            this.lbltotalamountolRO.Text = "Amount ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(31, 573);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 20);
            this.label10.TabIndex = 133;
            this.label10.Text = "Total RO Amount: ";
            // 
            // lblsupplier
            // 
            this.lblsupplier.AutoSize = true;
            this.lblsupplier.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsupplier.ForeColor = System.Drawing.Color.Red;
            this.lblsupplier.Location = new System.Drawing.Point(529, 61);
            this.lblsupplier.Name = "lblsupplier";
            this.lblsupplier.Size = new System.Drawing.Size(101, 20);
            this.lblsupplier.TabIndex = 136;
            this.lblsupplier.Text = "Purchase No.";
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
            this.button1.Size = new System.Drawing.Size(132, 33);
            this.button1.TabIndex = 138;
            this.button1.Text = "Receive Order";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
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
            this.buttonBack.TabIndex = 137;
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(403, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 139;
            this.label1.Text = "Supplier Name: ";
            // 
            // RODetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1095, 612);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.lblsupplier);
            this.Controls.Add(this.lbltotalamountolRO);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSaveRODetails);
            this.Controls.Add(this.btnROreversal);
            this.Controls.Add(this.dptrans);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPoNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvRO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RODetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvRO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lblPoNo;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dgvRO;
        public System.Windows.Forms.DateTimePicker dptrans;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnROreversal;
        public System.Windows.Forms.Button btnSaveRODetails;
        public System.Windows.Forms.Label lbltotalamountolRO;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label lblsupplier;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn pono;
        private System.Windows.Forms.DataGridViewTextBoxColumn productname;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn expirydate;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewCheckBoxColumn partialselect;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allselect;
    }
}