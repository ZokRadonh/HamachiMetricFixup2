namespace HamachiMetricFixup2
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bFixit = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioRoutingTable = new System.Windows.Forms.RadioButton();
            this.radioAdapter = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labRoutesToFix = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labTargetMetric = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labMaxMetric = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDevices
            // 
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(9, 32);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(390, 21);
            this.cmbDevices.TabIndex = 0;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Netzwerkadapter über den die Spiele laufen sollen:";
            // 
            // bFixit
            // 
            this.bFixit.Location = new System.Drawing.Point(405, 27);
            this.bFixit.Name = "bFixit";
            this.bFixit.Size = new System.Drawing.Size(88, 28);
            this.bFixit.TabIndex = 2;
            this.bFixit.Text = "Fix it baby";
            this.bFixit.UseVisualStyleBackColor = true;
            this.bFixit.Click += new System.EventHandler(this.bFixit_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(16, 45);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(258, 329);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(280, 45);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(223, 329);
            this.listBox2.TabIndex = 4;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(509, 45);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(251, 329);
            this.listBox3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioRoutingTable);
            this.groupBox1.Controls.Add(this.radioAdapter);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.listBox3);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 387);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug";
            // 
            // radioRoutingTable
            // 
            this.radioRoutingTable.AutoSize = true;
            this.radioRoutingTable.Checked = true;
            this.radioRoutingTable.Location = new System.Drawing.Point(122, 22);
            this.radioRoutingTable.Name = "radioRoutingTable";
            this.radioRoutingTable.Size = new System.Drawing.Size(91, 17);
            this.radioRoutingTable.TabIndex = 6;
            this.radioRoutingTable.TabStop = true;
            this.radioRoutingTable.Text = "Routentabelle";
            this.radioRoutingTable.UseVisualStyleBackColor = true;
            this.radioRoutingTable.CheckedChanged += new System.EventHandler(this.debugListboxSwitch);
            // 
            // radioAdapter
            // 
            this.radioAdapter.AutoSize = true;
            this.radioAdapter.Location = new System.Drawing.Point(16, 22);
            this.radioAdapter.Name = "radioAdapter";
            this.radioAdapter.Size = new System.Drawing.Size(100, 17);
            this.radioAdapter.TabIndex = 6;
            this.radioAdapter.TabStop = true;
            this.radioAdapter.Text = "Netzwerkkarten";
            this.radioAdapter.UseVisualStyleBackColor = true;
            this.radioAdapter.CheckedChanged += new System.EventHandler(this.debugListboxSwitch);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbDevices);
            this.groupBox2.Controls.Add(this.bFixit);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 67);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Einstellungen";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labRoutesToFix);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.labTargetMetric);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.labMaxMetric);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(528, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 73);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Daten";
            // 
            // labRoutesToFix
            // 
            this.labRoutesToFix.Location = new System.Drawing.Point(89, 54);
            this.labRoutesToFix.Name = "labRoutesToFix";
            this.labRoutesToFix.Size = new System.Drawing.Size(42, 13);
            this.labRoutesToFix.TabIndex = 5;
            this.labRoutesToFix.Text = "0";
            this.labRoutesToFix.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Routes to fix:";
            // 
            // labTargetMetric
            // 
            this.labTargetMetric.Location = new System.Drawing.Point(89, 35);
            this.labTargetMetric.Name = "labTargetMetric";
            this.labTargetMetric.Size = new System.Drawing.Size(42, 13);
            this.labTargetMetric.TabIndex = 3;
            this.labTargetMetric.Text = "0";
            this.labTargetMetric.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target Metric:";
            // 
            // labMaxMetric
            // 
            this.labMaxMetric.Location = new System.Drawing.Point(89, 16);
            this.labMaxMetric.Name = "labMaxMetric";
            this.labMaxMetric.Size = new System.Drawing.Size(42, 13);
            this.labMaxMetric.TabIndex = 1;
            this.labMaxMetric.Text = "0";
            this.labMaxMetric.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Max Metric:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 488);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Zok\'s Hamachi Metric Fixer - arctics.net";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bFixit;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labMaxMetric;
        private System.Windows.Forms.Label labTargetMetric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labRoutesToFix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioRoutingTable;
        private System.Windows.Forms.RadioButton radioAdapter;
    }
}

