namespace PIERANGELO.Inventory
{
    partial class DisposeProducts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisposeProducts));
            this.dgvDisposedProducts = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.txtsearchproduct = new MetroFramework.Controls.MetroTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisposedProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDisposedProducts
            // 
            this.dgvDisposedProducts.AllowUserToAddRows = false;
            this.dgvDisposedProducts.AllowUserToResizeColumns = false;
            this.dgvDisposedProducts.AllowUserToResizeRows = false;
            this.dgvDisposedProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvDisposedProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDisposedProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDisposedProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisposedProducts.ColumnHeadersHeight = 30;
            this.dgvDisposedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDisposedProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.productcode,
            this.proname,
            this.cat,
            this.qty,
            this.Column1,
            this.Column2,
            this.Delete});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDisposedProducts.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDisposedProducts.EnableHeadersVisualStyles = false;
            this.dgvDisposedProducts.GridColor = System.Drawing.Color.White;
            this.dgvDisposedProducts.Location = new System.Drawing.Point(0, 114);
            this.dgvDisposedProducts.Name = "dgvDisposedProducts";
            this.dgvDisposedProducts.ReadOnly = true;
            this.dgvDisposedProducts.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDisposedProducts.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDisposedProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDisposedProducts.Size = new System.Drawing.Size(1099, 542);
            this.dgvDisposedProducts.TabIndex = 58;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 39;
            // 
            // productcode
            // 
            this.productcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.productcode.DataPropertyName = "productcode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.productcode.DefaultCellStyle = dataGridViewCellStyle2;
            this.productcode.HeaderText = "PROCODE";
            this.productcode.Name = "productcode";
            this.productcode.ReadOnly = true;
            this.productcode.Width = 92;
            // 
            // proname
            // 
            this.proname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.proname.HeaderText = "PRODUCT NAME";
            this.proname.Name = "proname";
            this.proname.ReadOnly = true;
            // 
            // cat
            // 
            this.cat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cat.DefaultCellStyle = dataGridViewCellStyle3;
            this.cat.HeaderText = "CATEGORY";
            this.cat.Name = "cat";
            this.cat.ReadOnly = true;
            this.cat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cat.Width = 78;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.qty.HeaderText = "QTY";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 56;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "EXPIRY DATE";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 112;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "DISPOSAL DATE";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 130;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Delete.HeaderText = "";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Visible = false;
            this.Delete.Width = 5;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(595, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 25);
            this.button2.TabIndex = 63;
            this.button2.Text = "Sort by Category\r\n";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // cbCategory
            // 
            this.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.cbCategory.BackColor = System.Drawing.SystemColors.Control;
            this.cbCategory.DropDownHeight = 90;
            this.cbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.IntegralHeight = false;
            this.cbCategory.Location = new System.Drawing.Point(595, 76);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(238, 25);
            this.cbCategory.TabIndex = 62;
            this.cbCategory.SelectedValueChanged += new System.EventHandler(this.cbCategory_SelectedValueChanged);
            // 
            // txtsearchproduct
            // 
            // 
            // 
            // 
            this.txtsearchproduct.CustomButton.Image = null;
            this.txtsearchproduct.CustomButton.Location = new System.Drawing.Point(553, 1);
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
            this.txtsearchproduct.Location = new System.Drawing.Point(12, 76);
            this.txtsearchproduct.MaxLength = 32767;
            this.txtsearchproduct.Name = "txtsearchproduct";
            this.txtsearchproduct.PasswordChar = '\0';
            this.txtsearchproduct.PromptText = "Search Product here";
            this.txtsearchproduct.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtsearchproduct.SelectedText = "";
            this.txtsearchproduct.SelectionLength = 0;
            this.txtsearchproduct.SelectionStart = 0;
            this.txtsearchproduct.ShortcutsEnabled = true;
            this.txtsearchproduct.Size = new System.Drawing.Size(577, 25);
            this.txtsearchproduct.TabIndex = 61;
            this.txtsearchproduct.UseCustomBackColor = true;
            this.txtsearchproduct.UseSelectable = true;
            this.txtsearchproduct.WaterMark = "Search Product here";
            this.txtsearchproduct.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchproduct.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtsearchproduct.TextChanged += new System.EventHandler(this.txtsearchproduct_TextChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(44, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(172, 33);
            this.button3.TabIndex = 71;
            this.button3.Text = "Disposed Products";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = false;
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
            this.buttonBack.TabIndex = 70;
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click_1);
            // 
            // datefrom
            // 
            this.datefrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datefrom.Location = new System.Drawing.Point(852, 79);
            this.datefrom.MaxDate = new System.DateTime(2029, 12, 31, 0, 0, 0, 0);
            this.datefrom.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(103, 22);
            this.datefrom.TabIndex = 72;
            // 
            // dateto
            // 
            this.dateto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateto.Location = new System.Drawing.Point(961, 79);
            this.dateto.MaxDate = new System.DateTime(2029, 12, 31, 0, 0, 0, 0);
            this.dateto.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(103, 22);
            this.dateto.TabIndex = 73;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(849, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 15);
            this.label2.TabIndex = 74;
            this.label2.Text = "Filter by Date (From - To)";
            // 
            // DisposeProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1097, 656);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateto);
            this.Controls.Add(this.datefrom);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.txtsearchproduct);
            this.Controls.Add(this.dgvDisposedProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DisposeProducts";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DisposeProducts_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisposedProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.DataGridView dgvDisposedProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn productcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn proname;
        private System.Windows.Forms.DataGridViewTextBoxColumn cat;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbCategory;
        private MetroFramework.Controls.MetroTextBox txtsearchproduct;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.DateTimePicker datefrom;
        public System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.Label label2;
    }
}