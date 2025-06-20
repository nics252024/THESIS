namespace PIERANGELO
{
    partial class AdminMainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Timer notificationTimer;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminMainForm));
            this.panelbgmain = new System.Windows.Forms.Panel();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btndashboard = new System.Windows.Forms.Button();
            this.InventoryContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btninventory = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnProductList = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCategory = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnStock = new System.Windows.Forms.Button();
            this.ReportContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnReports = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnsalesreport = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btninventoryReports = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnrandex = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnSoldProducts = new System.Windows.Forms.Button();
            this.SettingContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.useraccount = new System.Windows.Forms.Button();
            this.panel17 = new System.Windows.Forms.Panel();
            this.supplierbtn = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel19 = new System.Windows.Forms.Panel();
            this.btnArchives = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.loghistory = new System.Windows.Forms.Button();
            this.panelcontent = new System.Windows.Forms.Panel();
            this.panelwel = new System.Windows.Forms.Panel();
            this.lblDateee = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.lblname = new System.Windows.Forms.Label();
            this.profilepicture = new System.Windows.Forms.PictureBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.picboxnotif = new System.Windows.Forms.PictureBox();
            this.InventoryTransition = new System.Windows.Forms.Timer(this.components);
            this.ReportTransition = new System.Windows.Forms.Timer(this.components);
            this.sidebartransition = new System.Windows.Forms.Timer(this.components);
            this.Settingstransition = new System.Windows.Forms.Timer(this.components);
            this.Time = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            notificationTimer = new System.Windows.Forms.Timer(this.components);
            this.panelbgmain.SuspendLayout();
            this.sidebar.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.InventoryContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.ReportContainer.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel12.SuspendLayout();
            this.SettingContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panelwel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxnotif)).BeginInit();
            this.SuspendLayout();
            // 
            // notificationTimer
            // 
            notificationTimer.Interval = 60000;
            // 
            // panelbgmain
            // 
            this.panelbgmain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.panelbgmain.Controls.Add(this.sidebar);
            this.panelbgmain.Controls.Add(this.panelcontent);
            this.panelbgmain.Controls.Add(this.panelwel);
            this.panelbgmain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelbgmain.Location = new System.Drawing.Point(0, 0);
            this.panelbgmain.Name = "panelbgmain";
            this.panelbgmain.Size = new System.Drawing.Size(1337, 717);
            this.panelbgmain.TabIndex = 0;
            // 
            // sidebar
            // 
            this.sidebar.AutoScroll = true;
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.sidebar.Controls.Add(this.panel11);
            this.sidebar.Controls.Add(this.btndashboard);
            this.sidebar.Controls.Add(this.InventoryContainer);
            this.sidebar.Controls.Add(this.ReportContainer);
            this.sidebar.Controls.Add(this.SettingContainer);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 59);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(238, 658);
            this.sidebar.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.pictureBox1);
            this.panel11.Location = new System.Drawing.Point(3, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(235, 190);
            this.panel11.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-3, -6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 213);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // btndashboard
            // 
            this.btndashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btndashboard.FlatAppearance.BorderSize = 0;
            this.btndashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(151)))), ((int)(((byte)(184)))));
            this.btndashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndashboard.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndashboard.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btndashboard.Image = ((System.Drawing.Image)(resources.GetObject("btndashboard.Image")));
            this.btndashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndashboard.Location = new System.Drawing.Point(3, 199);
            this.btndashboard.Name = "btndashboard";
            this.btndashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btndashboard.Size = new System.Drawing.Size(230, 46);
            this.btndashboard.TabIndex = 4;
            this.btndashboard.Text = "      Dashboard";
            this.btndashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btndashboard.UseVisualStyleBackColor = false;
            this.btndashboard.Click += new System.EventHandler(this.btndashboard_Click);
            // 
            // InventoryContainer
            // 
            this.InventoryContainer.Controls.Add(this.panel2);
            this.InventoryContainer.Controls.Add(this.panel3);
            this.InventoryContainer.Controls.Add(this.panel4);
            this.InventoryContainer.Controls.Add(this.panel5);
            this.InventoryContainer.Location = new System.Drawing.Point(3, 251);
            this.InventoryContainer.MaximumSize = new System.Drawing.Size(230, 181);
            this.InventoryContainer.MinimumSize = new System.Drawing.Size(230, 46);
            this.InventoryContainer.Name = "InventoryContainer";
            this.InventoryContainer.Size = new System.Drawing.Size(230, 46);
            this.InventoryContainer.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btninventory);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 45);
            this.panel2.TabIndex = 0;
            // 
            // btninventory
            // 
            this.btninventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btninventory.FlatAppearance.BorderSize = 0;
            this.btninventory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btninventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btninventory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btninventory.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btninventory.Image = ((System.Drawing.Image)(resources.GetObject("btninventory.Image")));
            this.btninventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btninventory.Location = new System.Drawing.Point(0, 0);
            this.btninventory.Name = "btninventory";
            this.btninventory.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btninventory.Size = new System.Drawing.Size(234, 45);
            this.btninventory.TabIndex = 12;
            this.btninventory.Text = "       Inventory";
            this.btninventory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btninventory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btninventory.UseVisualStyleBackColor = false;
            this.btninventory.Click += new System.EventHandler(this.btninventory_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(61)))));
            this.panel3.Controls.Add(this.btnProductList);
            this.panel3.Location = new System.Drawing.Point(0, 45);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(230, 45);
            this.panel3.TabIndex = 1;
            // 
            // btnProductList
            // 
            this.btnProductList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnProductList.FlatAppearance.BorderSize = 0;
            this.btnProductList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnProductList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductList.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnProductList.Image = ((System.Drawing.Image)(resources.GetObject("btnProductList.Image")));
            this.btnProductList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductList.Location = new System.Drawing.Point(-2, 0);
            this.btnProductList.Name = "btnProductList";
            this.btnProductList.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnProductList.Size = new System.Drawing.Size(232, 45);
            this.btnProductList.TabIndex = 12;
            this.btnProductList.Text = "       Product List";
            this.btnProductList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProductList.UseVisualStyleBackColor = false;
            this.btnProductList.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(61)))));
            this.panel4.Controls.Add(this.btnCategory);
            this.panel4.Location = new System.Drawing.Point(0, 90);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(230, 45);
            this.panel4.TabIndex = 2;
            // 
            // btnCategory
            // 
            this.btnCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnCategory.FlatAppearance.BorderSize = 0;
            this.btnCategory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategory.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
            this.btnCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategory.Location = new System.Drawing.Point(-2, 0);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnCategory.Size = new System.Drawing.Size(232, 45);
            this.btnCategory.TabIndex = 12;
            this.btnCategory.Text = "       Category List";
            this.btnCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCategory.UseVisualStyleBackColor = false;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(61)))));
            this.panel5.Controls.Add(this.btnStock);
            this.panel5.Location = new System.Drawing.Point(0, 135);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(230, 45);
            this.panel5.TabIndex = 3;
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnStock.FlatAppearance.BorderSize = 0;
            this.btnStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStock.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnStock.Image = ((System.Drawing.Image)(resources.GetObject("btnStock.Image")));
            this.btnStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStock.Location = new System.Drawing.Point(-2, 0);
            this.btnStock.Name = "btnStock";
            this.btnStock.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnStock.Size = new System.Drawing.Size(232, 45);
            this.btnStock.TabIndex = 12;
            this.btnStock.Text = "       Stock";
            this.btnStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStock.UseVisualStyleBackColor = false;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // ReportContainer
            // 
            this.ReportContainer.Controls.Add(this.panel6);
            this.ReportContainer.Controls.Add(this.panel7);
            this.ReportContainer.Controls.Add(this.panel8);
            this.ReportContainer.Controls.Add(this.panel9);
            this.ReportContainer.Controls.Add(this.panel12);
            this.ReportContainer.Location = new System.Drawing.Point(3, 303);
            this.ReportContainer.MaximumSize = new System.Drawing.Size(231, 225);
            this.ReportContainer.MinimumSize = new System.Drawing.Size(231, 46);
            this.ReportContainer.Name = "ReportContainer";
            this.ReportContainer.Size = new System.Drawing.Size(231, 46);
            this.ReportContainer.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnReports);
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(232, 45);
            this.panel6.TabIndex = 0;
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReports.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnReports.Image = ((System.Drawing.Image)(resources.GetObject("btnReports.Image")));
            this.btnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.Location = new System.Drawing.Point(0, 0);
            this.btnReports.Name = "btnReports";
            this.btnReports.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnReports.Size = new System.Drawing.Size(232, 45);
            this.btnReports.TabIndex = 18;
            this.btnReports.Text = "       Reports";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnsalesreport);
            this.panel7.Location = new System.Drawing.Point(0, 45);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(232, 45);
            this.panel7.TabIndex = 1;
            // 
            // btnsalesreport
            // 
            this.btnsalesreport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnsalesreport.FlatAppearance.BorderSize = 0;
            this.btnsalesreport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnsalesreport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsalesreport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsalesreport.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnsalesreport.Image = ((System.Drawing.Image)(resources.GetObject("btnsalesreport.Image")));
            this.btnsalesreport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsalesreport.Location = new System.Drawing.Point(0, 0);
            this.btnsalesreport.Name = "btnsalesreport";
            this.btnsalesreport.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnsalesreport.Size = new System.Drawing.Size(232, 45);
            this.btnsalesreport.TabIndex = 18;
            this.btnsalesreport.Text = "       Sales Reports";
            this.btnsalesreport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsalesreport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnsalesreport.UseVisualStyleBackColor = false;
            this.btnsalesreport.Click += new System.EventHandler(this.btnsalesreport_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btninventoryReports);
            this.panel8.Location = new System.Drawing.Point(0, 90);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(232, 45);
            this.panel8.TabIndex = 2;
            // 
            // btninventoryReports
            // 
            this.btninventoryReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btninventoryReports.FlatAppearance.BorderSize = 0;
            this.btninventoryReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btninventoryReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btninventoryReports.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btninventoryReports.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btninventoryReports.Image = ((System.Drawing.Image)(resources.GetObject("btninventoryReports.Image")));
            this.btninventoryReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btninventoryReports.Location = new System.Drawing.Point(0, 0);
            this.btninventoryReports.Name = "btninventoryReports";
            this.btninventoryReports.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btninventoryReports.Size = new System.Drawing.Size(232, 45);
            this.btninventoryReports.TabIndex = 18;
            this.btninventoryReports.Text = "       Inventory Reports";
            this.btninventoryReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btninventoryReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btninventoryReports.UseVisualStyleBackColor = false;
            this.btninventoryReports.Click += new System.EventHandler(this.btninventoryReports_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnrandex);
            this.panel9.Location = new System.Drawing.Point(0, 135);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(232, 45);
            this.panel9.TabIndex = 3;
            // 
            // btnrandex
            // 
            this.btnrandex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnrandex.FlatAppearance.BorderSize = 0;
            this.btnrandex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnrandex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrandex.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrandex.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnrandex.Image = ((System.Drawing.Image)(resources.GetObject("btnrandex.Image")));
            this.btnrandex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrandex.Location = new System.Drawing.Point(0, 0);
            this.btnrandex.Name = "btnrandex";
            this.btnrandex.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnrandex.Size = new System.Drawing.Size(232, 45);
            this.btnrandex.TabIndex = 18;
            this.btnrandex.Text = "       Returns and \r\n       Exchanges Report";
            this.btnrandex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrandex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnrandex.UseVisualStyleBackColor = false;
            this.btnrandex.Click += new System.EventHandler(this.btnrandex_Click);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.btnSoldProducts);
            this.panel12.Location = new System.Drawing.Point(0, 180);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(232, 45);
            this.panel12.TabIndex = 5;
            // 
            // btnSoldProducts
            // 
            this.btnSoldProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnSoldProducts.FlatAppearance.BorderSize = 0;
            this.btnSoldProducts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSoldProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSoldProducts.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoldProducts.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSoldProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnSoldProducts.Image")));
            this.btnSoldProducts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSoldProducts.Location = new System.Drawing.Point(0, 0);
            this.btnSoldProducts.Name = "btnSoldProducts";
            this.btnSoldProducts.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnSoldProducts.Size = new System.Drawing.Size(232, 45);
            this.btnSoldProducts.TabIndex = 18;
            this.btnSoldProducts.Text = "        PO Reports";
            this.btnSoldProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSoldProducts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSoldProducts.UseVisualStyleBackColor = false;
            this.btnSoldProducts.Click += new System.EventHandler(this.btnSoldProducts_Click);
            // 
            // SettingContainer
            // 
            this.SettingContainer.Controls.Add(this.panel1);
            this.SettingContainer.Controls.Add(this.panel14);
            this.SettingContainer.Controls.Add(this.panel17);
            this.SettingContainer.Controls.Add(this.panel15);
            this.SettingContainer.Controls.Add(this.panel19);
            this.SettingContainer.Controls.Add(this.panel16);
            this.SettingContainer.Location = new System.Drawing.Point(3, 355);
            this.SettingContainer.MaximumSize = new System.Drawing.Size(231, 271);
            this.SettingContainer.MinimumSize = new System.Drawing.Size(231, 46);
            this.SettingContainer.Name = "SettingContainer";
            this.SettingContainer.Size = new System.Drawing.Size(231, 46);
            this.SettingContainer.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 45);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(234, 45);
            this.button1.TabIndex = 18;
            this.button1.Text = "        Settings";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.useraccount);
            this.panel14.Location = new System.Drawing.Point(0, 45);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(230, 45);
            this.panel14.TabIndex = 1;
            // 
            // useraccount
            // 
            this.useraccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.useraccount.FlatAppearance.BorderSize = 0;
            this.useraccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.useraccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.useraccount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useraccount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.useraccount.Image = ((System.Drawing.Image)(resources.GetObject("useraccount.Image")));
            this.useraccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.useraccount.Location = new System.Drawing.Point(0, 0);
            this.useraccount.Name = "useraccount";
            this.useraccount.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.useraccount.Size = new System.Drawing.Size(231, 45);
            this.useraccount.TabIndex = 18;
            this.useraccount.Text = "       Account";
            this.useraccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.useraccount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.useraccount.UseVisualStyleBackColor = false;
            this.useraccount.Click += new System.EventHandler(this.useraccount_Click);
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.supplierbtn);
            this.panel17.Location = new System.Drawing.Point(0, 90);
            this.panel17.Margin = new System.Windows.Forms.Padding(0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(230, 45);
            this.panel17.TabIndex = 4;
            // 
            // supplierbtn
            // 
            this.supplierbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.supplierbtn.FlatAppearance.BorderSize = 0;
            this.supplierbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.supplierbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.supplierbtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supplierbtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.supplierbtn.Image = ((System.Drawing.Image)(resources.GetObject("supplierbtn.Image")));
            this.supplierbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.supplierbtn.Location = new System.Drawing.Point(0, 0);
            this.supplierbtn.Name = "supplierbtn";
            this.supplierbtn.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.supplierbtn.Size = new System.Drawing.Size(231, 45);
            this.supplierbtn.TabIndex = 18;
            this.supplierbtn.Text = "       Supplier";
            this.supplierbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.supplierbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.supplierbtn.UseVisualStyleBackColor = false;
            this.supplierbtn.Click += new System.EventHandler(this.supplierbtn_Click);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.button4);
            this.panel15.Location = new System.Drawing.Point(0, 135);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(230, 45);
            this.panel15.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(231, 45);
            this.button4.TabIndex = 18;
            this.button4.Text = "       Discount";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.btnArchives);
            this.panel19.Location = new System.Drawing.Point(0, 180);
            this.panel19.Margin = new System.Windows.Forms.Padding(0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(230, 45);
            this.panel19.TabIndex = 6;
            // 
            // btnArchives
            // 
            this.btnArchives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnArchives.FlatAppearance.BorderSize = 0;
            this.btnArchives.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnArchives.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArchives.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArchives.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnArchives.Image = ((System.Drawing.Image)(resources.GetObject("btnArchives.Image")));
            this.btnArchives.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArchives.Location = new System.Drawing.Point(0, 0);
            this.btnArchives.Name = "btnArchives";
            this.btnArchives.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnArchives.Size = new System.Drawing.Size(230, 45);
            this.btnArchives.TabIndex = 18;
            this.btnArchives.Text = "       Recycle Bin";
            this.btnArchives.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArchives.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnArchives.UseVisualStyleBackColor = false;
            this.btnArchives.Click += new System.EventHandler(this.btnArchives_Click);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.loghistory);
            this.panel16.Location = new System.Drawing.Point(0, 225);
            this.panel16.Margin = new System.Windows.Forms.Padding(0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(230, 45);
            this.panel16.TabIndex = 3;
            // 
            // loghistory
            // 
            this.loghistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.loghistory.FlatAppearance.BorderSize = 0;
            this.loghistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.loghistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loghistory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loghistory.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.loghistory.Image = ((System.Drawing.Image)(resources.GetObject("loghistory.Image")));
            this.loghistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loghistory.Location = new System.Drawing.Point(0, 0);
            this.loghistory.Name = "loghistory";
            this.loghistory.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.loghistory.Size = new System.Drawing.Size(230, 45);
            this.loghistory.TabIndex = 18;
            this.loghistory.Text = "       Activity Log";
            this.loghistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loghistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.loghistory.UseVisualStyleBackColor = false;
            this.loghistory.Click += new System.EventHandler(this.loghistory_Click);
            // 
            // panelcontent
            // 
            this.panelcontent.BackColor = System.Drawing.Color.White;
            this.panelcontent.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelcontent.Location = new System.Drawing.Point(238, 59);
            this.panelcontent.Name = "panelcontent";
            this.panelcontent.Size = new System.Drawing.Size(1099, 658);
            this.panelcontent.TabIndex = 5;
            this.panelcontent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelcontent_Paint);
            // 
            // panelwel
            // 
            this.panelwel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.panelwel.Controls.Add(this.lblDateee);
            this.panelwel.Controls.Add(this.lblTime);
            this.panelwel.Controls.Add(this.buttonLogout);
            this.panelwel.Controls.Add(this.lblname);
            this.panelwel.Controls.Add(this.profilepicture);
            this.panelwel.Controls.Add(this.lblRole);
            this.panelwel.Controls.Add(this.lblUser);
            this.panelwel.Controls.Add(this.picboxnotif);
            this.panelwel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelwel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panelwel.Location = new System.Drawing.Point(0, 0);
            this.panelwel.Name = "panelwel";
            this.panelwel.Size = new System.Drawing.Size(1337, 59);
            this.panelwel.TabIndex = 4;
            // 
            // lblDateee
            // 
            this.lblDateee.AutoSize = true;
            this.lblDateee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.lblDateee.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateee.ForeColor = System.Drawing.Color.White;
            this.lblDateee.Location = new System.Drawing.Point(10, 37);
            this.lblDateee.Name = "lblDateee";
            this.lblDateee.Size = new System.Drawing.Size(56, 17);
            this.lblDateee.TabIndex = 16;
            this.lblDateee.Text = "00:00:00";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.lblTime.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(6, 1);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(121, 37);
            this.lblTime.TabIndex = 15;
            this.lblTime.Text = "00:00:00";
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.buttonLogout.FlatAppearance.BorderSize = 0;
            this.buttonLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogout.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLogout.Image = ((System.Drawing.Image)(resources.GetObject("buttonLogout.Image")));
            this.buttonLogout.Location = new System.Drawing.Point(1280, 5);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.buttonLogout.Size = new System.Drawing.Size(54, 48);
            this.buttonLogout.TabIndex = 13;
            this.buttonLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // lblname
            // 
            this.lblname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblname.ForeColor = System.Drawing.Color.White;
            this.lblname.Location = new System.Drawing.Point(1107, 18);
            this.lblname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(106, 21);
            this.lblname.TabIndex = 2;
            this.lblname.Text = "Abel Tesfaye";
            this.lblname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblname.Click += new System.EventHandler(this.lblname_Click);
            // 
            // profilepicture
            // 
            this.profilepicture.Image = global::PIERANGELO.Properties.Resources.bell__1_;
            this.profilepicture.Location = new System.Drawing.Point(1052, 5);
            this.profilepicture.Name = "profilepicture";
            this.profilepicture.Size = new System.Drawing.Size(48, 45);
            this.profilepicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilepicture.TabIndex = 14;
            this.profilepicture.TabStop = false;
            this.profilepicture.Click += new System.EventHandler(this.profilepicture_Click);
            this.profilepicture.Paint += new System.Windows.Forms.PaintEventHandler(this.profilepicture_Paint);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.Black;
            this.lblRole.Location = new System.Drawing.Point(394, 23);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(84, 15);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Administrator";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRole.Visible = false;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(341, 19);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(0, 25);
            this.lblUser.TabIndex = 9;
            this.lblUser.Visible = false;
            // 
            // picboxnotif
            // 
            this.picboxnotif.Image = global::PIERANGELO.Properties.Resources.bell__1_;
            this.picboxnotif.Location = new System.Drawing.Point(1004, 13);
            this.picboxnotif.Name = "picboxnotif";
            this.picboxnotif.Size = new System.Drawing.Size(34, 32);
            this.picboxnotif.TabIndex = 0;
            this.picboxnotif.TabStop = false;
            this.picboxnotif.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // InventoryTransition
            // 
            this.InventoryTransition.Interval = 1;
            this.InventoryTransition.Tick += new System.EventHandler(this.InventoryTransition_Tick);
            // 
            // ReportTransition
            // 
            this.ReportTransition.Interval = 1;
            this.ReportTransition.Tick += new System.EventHandler(this.ReportTransition_Tick);
            // 
            // sidebartransition
            // 
            this.sidebartransition.Interval = 1;
            this.sidebartransition.Tick += new System.EventHandler(this.sidebartransition_Tick);
            // 
            // Settingstransition
            // 
            this.Settingstransition.Interval = 1;
            this.Settingstransition.Tick += new System.EventHandler(this.Settings_Tick);
            // 
            // Time
            // 
            this.Time.Interval = 1000;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 717);
            this.Controls.Add(this.panelbgmain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminMainForm";
            this.panelbgmain.ResumeLayout(false);
            this.sidebar.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.InventoryContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ReportContainer.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.SettingContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panelwel.ResumeLayout(false);
            this.panelwel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxnotif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelbgmain;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button btninventory;
        public System.Windows.Forms.Label lblname;
        public System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Panel panelcontent;
        private System.Windows.Forms.FlowLayoutPanel InventoryContainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnProductList;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Timer InventoryTransition;
        private System.Windows.Forms.FlowLayoutPanel ReportContainer;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnsalesreport;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btninventoryReports;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnrandex;
        private System.Windows.Forms.Timer ReportTransition;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Timer sidebartransition;
        private System.Windows.Forms.Button btnSoldProducts;
        private System.Windows.Forms.FlowLayoutPanel SettingContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button useraccount;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button loghistory;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Button supplierbtn;
        private System.Windows.Forms.Timer Settingstransition;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Button btnArchives;
        private System.Windows.Forms.Timer Time;
        public System.Windows.Forms.Button btndashboard;
        private System.Windows.Forms.PictureBox profilepicture;
        private System.Windows.Forms.PictureBox picboxnotif;
        private System.Windows.Forms.Panel panelwel;
        public System.Windows.Forms.Label lblUser;
        public System.Windows.Forms.Label lblDateee;
        public System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel12;
    }
}