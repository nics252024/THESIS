namespace PIERANGELO
{
    partial class Notification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notification));
            this.notificationtimer = new System.Windows.Forms.Timer(this.components);
            this.buttonBack = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.notifcationtimer = new System.Windows.Forms.Timer(this.components);
            this.listnofitication = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // notificationtimer
            // 
            this.notificationtimer.Enabled = true;
            this.notificationtimer.Interval = 6000;
            this.notificationtimer.Tick += new System.EventHandler(this.notificationtimer_Tick);
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(44, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 33);
            this.button1.TabIndex = 73;
            this.button1.Text = "Notification";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // notifcationtimer
            // 
            this.notifcationtimer.Enabled = true;
            // 
            // listnofitication
            // 
            this.listnofitication.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listnofitication.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listnofitication.FormattingEnabled = true;
            this.listnofitication.ItemHeight = 17;
            this.listnofitication.Location = new System.Drawing.Point(0, 47);
            this.listnofitication.Name = "listnofitication";
            this.listnofitication.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listnofitication.Size = new System.Drawing.Size(386, 612);
            this.listnofitication.TabIndex = 0;
            this.listnofitication.Click += new System.EventHandler(this.listnofitication_Click);
            this.listnofitication.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listnofitication_DrawItem);
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 656);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listnofitication);
            this.Controls.Add(this.buttonBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Notification_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Notification_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer notificationtimer;
        private System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer notifcationtimer;
        private System.Windows.Forms.ListBox listnofitication;
    }
}