namespace Ranji3._0
{
    partial class Baobiao
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_machine_num = new System.Windows.Forms.ComboBox();
            this.radioButton_simaple_jigang = new System.Windows.Forms.RadioButton();
            this.radioButton_all_jigang = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_banzu = new System.Windows.Forms.ComboBox();
            this.radioButton_simple_banzu = new System.Windows.Forms.RadioButton();
            this.radioButton_all_banzu = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(25, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(549, 817);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dateTimePicker_end);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker_start);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(616, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 503);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作区";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox_machine_num);
            this.groupBox4.Controls.Add(this.radioButton_simaple_jigang);
            this.groupBox4.Controls.Add(this.radioButton_all_jigang);
            this.groupBox4.Location = new System.Drawing.Point(37, 292);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 100);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "机缸选择";
            // 
            // comboBox_machine_num
            // 
            this.comboBox_machine_num.FormattingEnabled = true;
            this.comboBox_machine_num.Location = new System.Drawing.Point(80, 61);
            this.comboBox_machine_num.Name = "comboBox_machine_num";
            this.comboBox_machine_num.Size = new System.Drawing.Size(121, 20);
            this.comboBox_machine_num.TabIndex = 2;
            this.comboBox_machine_num.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // radioButton_simaple_jigang
            // 
            this.radioButton_simaple_jigang.AutoSize = true;
            this.radioButton_simaple_jigang.Location = new System.Drawing.Point(16, 62);
            this.radioButton_simaple_jigang.Name = "radioButton_simaple_jigang";
            this.radioButton_simaple_jigang.Size = new System.Drawing.Size(47, 16);
            this.radioButton_simaple_jigang.TabIndex = 1;
            this.radioButton_simaple_jigang.TabStop = true;
            this.radioButton_simaple_jigang.Text = "单缸";
            this.radioButton_simaple_jigang.UseVisualStyleBackColor = true;
            // 
            // radioButton_all_jigang
            // 
            this.radioButton_all_jigang.AutoSize = true;
            this.radioButton_all_jigang.Location = new System.Drawing.Point(16, 29);
            this.radioButton_all_jigang.Name = "radioButton_all_jigang";
            this.radioButton_all_jigang.Size = new System.Drawing.Size(47, 16);
            this.radioButton_all_jigang.TabIndex = 0;
            this.radioButton_all_jigang.TabStop = true;
            this.radioButton_all_jigang.Text = "所有";
            this.radioButton_all_jigang.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_banzu);
            this.groupBox2.Controls.Add(this.radioButton_simple_banzu);
            this.groupBox2.Controls.Add(this.radioButton_all_banzu);
            this.groupBox2.Location = new System.Drawing.Point(37, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 113);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "班组选择";
            // 
            // comboBox_banzu
            // 
            this.comboBox_banzu.FormattingEnabled = true;
            this.comboBox_banzu.Location = new System.Drawing.Point(81, 65);
            this.comboBox_banzu.Name = "comboBox_banzu";
            this.comboBox_banzu.Size = new System.Drawing.Size(121, 20);
            this.comboBox_banzu.TabIndex = 2;
            // 
            // radioButton_simple_banzu
            // 
            this.radioButton_simple_banzu.AutoSize = true;
            this.radioButton_simple_banzu.Location = new System.Drawing.Point(16, 69);
            this.radioButton_simple_banzu.Name = "radioButton_simple_banzu";
            this.radioButton_simple_banzu.Size = new System.Drawing.Size(59, 16);
            this.radioButton_simple_banzu.TabIndex = 1;
            this.radioButton_simple_banzu.TabStop = true;
            this.radioButton_simple_banzu.Text = "单班组";
            this.radioButton_simple_banzu.UseVisualStyleBackColor = true;
            // 
            // radioButton_all_banzu
            // 
            this.radioButton_all_banzu.AutoSize = true;
            this.radioButton_all_banzu.Location = new System.Drawing.Point(16, 32);
            this.radioButton_all_banzu.Name = "radioButton_all_banzu";
            this.radioButton_all_banzu.Size = new System.Drawing.Size(47, 16);
            this.radioButton_all_banzu.TabIndex = 0;
            this.radioButton_all_banzu.TabStop = true;
            this.radioButton_all_banzu.Text = "所有";
            this.radioButton_all_banzu.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_end.Location = new System.Drawing.Point(132, 69);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker_end.TabIndex = 3;
            this.dateTimePicker_end.ValueChanged += new System.EventHandler(this.dateTimePicker_end_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间";
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_start.Location = new System.Drawing.Point(132, 39);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker_start.TabIndex = 0;
            this.dateTimePicker_start.Value = new System.DateTime(2016, 8, 12, 8, 27, 20, 0);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "站号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "时间";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "班组";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "流量值（t）";
            this.Column4.Name = "Column4";
            // 
            // Baobiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 869);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Baobiao";
            this.Text = "Baobiao";
            this.Load += new System.EventHandler(this.Baobiao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_all_jigang;
        private System.Windows.Forms.RadioButton radioButton_simaple_jigang;
        private System.Windows.Forms.ComboBox comboBox_machine_num;
        private System.Windows.Forms.RadioButton radioButton_all_banzu;
        private System.Windows.Forms.RadioButton radioButton_simple_banzu;
        private System.Windows.Forms.ComboBox comboBox_banzu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}