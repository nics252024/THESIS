namespace PIERANGELO
{
    partial class RecycleBin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecycleBin));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnDeleteall = new System.Windows.Forms.Button();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.txtsearchproduct = new MetroFramework.Controls.MetroTextBox();
            this.dgvDeleteItems = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.markupprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reorder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvdeletecategory = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectCat = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnrestorecat = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtsearchcat = new MetroFramework.Controls.MetroTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.metroTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeleteItems)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdeletecategory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRestore
            // 
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
            this.btnRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestore.Location = new System.Drawing.Point(909, 14);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(83, 36);
            this.btnRestore.TabIndex = 59;
            this.btnRestore.Text = "Restore";
            this.btnRestore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnDeleteall
            // 
            this.btnDeleteall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteall.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteall.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteall.Image")));
            this.btnDeleteall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteall.Location = new System.Drawing.Point(998, 14);
            this.btnDeleteall.Name = "btnDeleteall";
            this.btnDeleteall.Size = new System.Drawing.Size(83, 36);
            this.btnDeleteall.TabIndex = 54;
            this.btnDeleteall.Text = "Delete ";
            this.btnDeleteall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteall.UseVisualStyleBackColor = false;
            this.btnDeleteall.Click += new System.EventHandler(this.btnDeleteall_Click);
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.tabPage1);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 56);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(1099, 602);
            this.metroTabControl1.TabIndex = 60;
            this.metroTabControl1.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1091, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Products";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.cbCategory);
            this.panel2.Controls.Add(this.txtsearchproduct);
            this.panel2.Controls.Add(this.dgvDeleteItems);
            this.panel2.Controls.Add(this.btnRestore);
            this.panel2.Controls.Add(this.btnDeleteall);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1091, 560);
            this.panel2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(518, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 25);
            this.button2.TabIndex = 65;
            this.button2.Text = "Sort by Category\r\n";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // cbCategory
            // 
            this.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.cbCategory.BackColor = System.Drawing.Color.White;
            this.cbCategory.DropDownHeight = 90;
            this.cbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.IntegralHeight = false;
            this.cbCategory.Location = new System.Drawing.Point(518, 53);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(342, 25);
            this.cbCategory.TabIndex = 64;
            this.cbCategory.SelectedValueChanged += new System.EventHandler(this.cbCategory_SelectedValueChanged);
            // 
            // txtsearchproduct
            // 
            this.txtsearchproduct.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtsearchproduct.CustomButton.Image = null;
            this.txtsearchproduct.CustomButton.Location = new System.Drawing.Point(480, 1);
            this.txtsearchproduct.CustomButton.Name = "";
            this.txtsearchproduct.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtsearchproduct.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtsearchproduct.CustomButton.TabIndex = 1;
            this.txtsearchproduct.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtsearchproduct.CustomButton.UseSelectable = true;
            this.txtsearchproduct.CustomButton.Visible = false;
            this.txtsearchproduct.DisplayIcon = true;
            this.txtsearchproduct.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtsearchproduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchproduct.Icon = ((System.Drawing.Image)(resources.GetObject("txtsearchproduct.Icon")));
            this.txtsearchproduct.Lines = new string[0];
            this.txtsearchproduct.Location = new System.Drawing.Point(8, 53);
            this.txtsearchproduct.MaxLength = 32767;
            this.txtsearchproduct.Name = "txtsearchproduct";
            this.txtsearchproduct.PasswordChar = '\0';
            this.txtsearchproduct.PromptText = "Search Product here";
            this.txtsearchproduct.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtsearchproduct.SelectedText = "";
            this.txtsearchproduct.SelectionLength = 0;
            this.txtsearchproduct.SelectionStart = 0;
            this.txtsearchproduct.ShortcutsEnabled = true;
            this.txtsearchproduct.Size = new System.Drawing.Size(504, 25);
            this.txtsearchproduct.TabIndex = 63;
            this.txtsearchproduct.UseCustomBackColor = true;
            this.txtsearchproduct.UseSelectable = true;
            this.txtsearchproduct.WaterMark = "Search Product here";
            this.txtsearchproduct.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchproduct.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearchproduct.TextChanged += new System.EventHandler(this.txtsearchproduct_TextChanged);
            // 
            // dgvDeleteItems
            // 
            this.dgvDeleteItems.AllowUserToAddRows = false;
            this.dgvDeleteItems.AllowUserToDeleteRows = false;
            this.dgvDeleteItems.AllowUserToResizeColumns = false;
            this.dgvDeleteItems.AllowUserToResizeRows = false;
            this.dgvDeleteItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvDeleteItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDeleteItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeleteItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDeleteItems.ColumnHeadersHeight = 30;
            this.dgvDeleteItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDeleteItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.pcode,
            this.barcode,
            this.productname,
            this.category,
            this.supprice,
            this.markupprice,
            this.price,
            this.supplier,
            this.reorder,
            this.qty,
            this.Select});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeleteItems.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDeleteItems.EnableHeadersVisualStyles = false;
            this.dgvDeleteItems.GridColor = System.Drawing.Color.White;
            this.dgvDeleteItems.Location = new System.Drawing.Point(3, 84);
            this.dgvDeleteItems.Name = "dgvDeleteItems";
            this.dgvDeleteItems.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDeleteItems.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDeleteItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeleteItems.Size = new System.Drawing.Size(1088, 476);
            this.dgvDeleteItems.TabIndex = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 37;
            // 
            // pcode
            // 
            this.pcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pcode.DataPropertyName = "pcode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.pcode.DefaultCellStyle = dataGridViewCellStyle2;
            this.pcode.HeaderText = "PROCODE";
            this.pcode.Name = "pcode";
            this.pcode.Width = 84;
            // 
            // barcode
            // 
            this.barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.barcode.DataPropertyName = "barcode";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.barcode.DefaultCellStyle = dataGridViewCellStyle3;
            this.barcode.HeaderText = "BARCODE";
            this.barcode.Name = "barcode";
            this.barcode.Width = 83;
            // 
            // productname
            // 
            this.productname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.productname.DataPropertyName = "productname";
            this.productname.HeaderText = "PRODUCT NAME";
            this.productname.Name = "productname";
            // 
            // category
            // 
            this.category.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.category.DataPropertyName = "category";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.category.DefaultCellStyle = dataGridViewCellStyle4;
            this.category.HeaderText = "CATEGORY";
            this.category.Name = "category";
            this.category.Width = 88;
            // 
            // supprice
            // 
            this.supprice.HeaderText = "SUPPLIER PRICE";
            this.supprice.Name = "supprice";
            this.supprice.Visible = false;
            // 
            // markupprice
            // 
            this.markupprice.HeaderText = "MARKUP";
            this.markupprice.Name = "markupprice";
            this.markupprice.Visible = false;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.price.DefaultCellStyle = dataGridViewCellStyle5;
            this.price.HeaderText = "PRICE";
            this.price.Name = "price";
            this.price.Width = 61;
            // 
            // supplier
            // 
            this.supplier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.supplier.DefaultCellStyle = dataGridViewCellStyle6;
            this.supplier.HeaderText = "SUPPLIER NAME";
            this.supplier.Name = "supplier";
            this.supplier.Visible = false;
            this.supplier.Width = 121;
            // 
            // reorder
            // 
            this.reorder.HeaderText = "RE ORDER ";
            this.reorder.Name = "reorder";
            this.reorder.Visible = false;
            // 
            // qty
            // 
            this.qty.HeaderText = "QTY";
            this.qty.Name = "qty";
            this.qty.Visible = false;
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.Width = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1091, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Category";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.dgvdeletecategory);
            this.panel3.Controls.Add(this.btnrestorecat);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.txtsearchcat);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1091, 560);
            this.panel3.TabIndex = 0;
            // 
            // dgvdeletecategory
            // 
            this.dgvdeletecategory.AllowUserToAddRows = false;
            this.dgvdeletecategory.AllowUserToDeleteRows = false;
            this.dgvdeletecategory.AllowUserToResizeColumns = false;
            this.dgvdeletecategory.AllowUserToResizeRows = false;
            this.dgvdeletecategory.BackgroundColor = System.Drawing.Color.White;
            this.dgvdeletecategory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvdeletecategory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdeletecategory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvdeletecategory.ColumnHeadersHeight = 30;
            this.dgvdeletecategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvdeletecategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.id,
            this.cat,
            this.SelectCat});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdeletecategory.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvdeletecategory.EnableHeadersVisualStyles = false;
            this.dgvdeletecategory.GridColor = System.Drawing.Color.White;
            this.dgvdeletecategory.Location = new System.Drawing.Point(0, 84);
            this.dgvdeletecategory.Name = "dgvdeletecategory";
            this.dgvdeletecategory.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvdeletecategory.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvdeletecategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvdeletecategory.Size = new System.Drawing.Size(1088, 476);
            this.dgvdeletecategory.TabIndex = 68;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "#";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 39;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            // 
            // cat
            // 
            this.cat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cat.DataPropertyName = "cat";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cat.DefaultCellStyle = dataGridViewCellStyle10;
            this.cat.HeaderText = "CATEGORY";
            this.cat.Name = "cat";
            // 
            // SelectCat
            // 
            this.SelectCat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SelectCat.HeaderText = "";
            this.SelectCat.Name = "SelectCat";
            this.SelectCat.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectCat.Width = 5;
            // 
            // btnrestorecat
            // 
            this.btnrestorecat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrestorecat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrestorecat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnrestorecat.Image = ((System.Drawing.Image)(resources.GetObject("btnrestorecat.Image")));
            this.btnrestorecat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrestorecat.Location = new System.Drawing.Point(908, 14);
            this.btnrestorecat.Name = "btnrestorecat";
            this.btnrestorecat.Size = new System.Drawing.Size(83, 36);
            this.btnrestorecat.TabIndex = 67;
            this.btnrestorecat.Text = "Restore";
            this.btnrestorecat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnrestorecat.UseVisualStyleBackColor = false;
            this.btnrestorecat.Click += new System.EventHandler(this.btnrestorecat_Click);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(997, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 36);
            this.button3.TabIndex = 66;
            this.button3.Text = "Delete ";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // txtsearchcat
            // 
            this.txtsearchcat.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtsearchcat.CustomButton.Image = null;
            this.txtsearchcat.CustomButton.Location = new System.Drawing.Point(568, 1);
            this.txtsearchcat.CustomButton.Name = "";
            this.txtsearchcat.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtsearchcat.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtsearchcat.CustomButton.TabIndex = 1;
            this.txtsearchcat.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtsearchcat.CustomButton.UseSelectable = true;
            this.txtsearchcat.CustomButton.Visible = false;
            this.txtsearchcat.DisplayIcon = true;
            this.txtsearchcat.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtsearchcat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchcat.Icon = ((System.Drawing.Image)(resources.GetObject("txtsearchcat.Icon")));
            this.txtsearchcat.Lines = new string[0];
            this.txtsearchcat.Location = new System.Drawing.Point(14, 53);
            this.txtsearchcat.MaxLength = 32767;
            this.txtsearchcat.Name = "txtsearchcat";
            this.txtsearchcat.PasswordChar = '\0';
            this.txtsearchcat.PromptText = "Search Product here";
            this.txtsearchcat.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtsearchcat.SelectedText = "";
            this.txtsearchcat.SelectionLength = 0;
            this.txtsearchcat.SelectionStart = 0;
            this.txtsearchcat.ShortcutsEnabled = true;
            this.txtsearchcat.Size = new System.Drawing.Size(592, 25);
            this.txtsearchcat.TabIndex = 65;
            this.txtsearchcat.UseCustomBackColor = true;
            this.txtsearchcat.UseSelectable = true;
            this.txtsearchcat.WaterMark = "Search Product here";
            this.txtsearchcat.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtsearchcat.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearchcat.TextChanged += new System.EventHandler(this.txtsearchcat_TextChanged);
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
            this.button1.TabIndex = 73;
            this.button1.Text = "Recycle Bin";
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
            this.buttonBack.TabIndex = 72;
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click_1);
            // 
            // RecycleBin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1099, 658);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.metroTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RecycleBin";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RecycleBin_KeyDown);
            this.metroTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeleteItems)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdeletecategory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDeleteall;
        private System.Windows.Forms.Button btnRestore;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.DataGridView dgvDeleteItems;
        public MetroFramework.Controls.MetroTextBox txtsearchproduct;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        public MetroFramework.Controls.MetroTextBox txtsearchcat;
        public System.Windows.Forms.DataGridView dgvdeletecategory;
        private System.Windows.Forms.Button btnrestorecat;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn productname;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn supprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn markupprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn reorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectCat;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBack;
    }
}