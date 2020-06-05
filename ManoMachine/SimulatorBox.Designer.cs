namespace ManoMachine
{
    partial class SimulatorBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.memoryGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.uOpsList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nextuOpBox = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.sCheckBox = new System.Windows.Forms.CheckBox();
            this.iCheckBox = new System.Windows.Forms.CheckBox();
            this.eCheckBox = new System.Windows.Forms.CheckBox();
            this.fgiCheckBox = new System.Windows.Forms.CheckBox();
            this.fgoCheckBox = new System.Windows.Forms.CheckBox();
            this.scTextBox = new System.Windows.Forms.TextBox();
            this.arTextBox = new System.Windows.Forms.TextBox();
            this.drTextBox = new System.Windows.Forms.TextBox();
            this.inprTextBox = new System.Windows.Forms.TextBox();
            this.trTextBox = new System.Windows.Forms.TextBox();
            this.pcTextBox = new System.Windows.Forms.TextBox();
            this.irTextBox = new System.Windows.Forms.TextBox();
            this.acTextBox = new System.Windows.Forms.TextBox();
            this.outrTextBox = new System.Windows.Forms.TextBox();
            this.rCheckBox = new System.Windows.Forms.CheckBox();
            this.ienCheckBox = new System.Windows.Forms.CheckBox();
            this.ticksCountLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.resetButton = new System.Windows.Forms.Button();
            this.tickButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.CheckBox();
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.pcPointerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.memoryGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // memoryGridView
            // 
            this.memoryGridView.AllowUserToResizeRows = false;
            this.memoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memoryGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pcPointerColumn,
            this.addressColumn,
            this.contentColumn,
            this.commentColumn});
            this.memoryGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryGridView.Location = new System.Drawing.Point(5, 23);
            this.memoryGridView.Name = "memoryGridView";
            this.memoryGridView.RowHeadersVisible = false;
            this.memoryGridView.Size = new System.Drawing.Size(231, 231);
            this.memoryGridView.TabIndex = 0;
            this.memoryGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.memoryGridView_CellValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.label1.Size = new System.Drawing.Size(54, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Memory";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.memoryGridView);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(503, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(241, 259);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.inputBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.outputBox, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 166);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(503, 93);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 2);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label2.Size = new System.Drawing.Size(41, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 2);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Output";
            // 
            // inputBox
            // 
            this.inputBox.AcceptsReturn = true;
            this.inputBox.AcceptsTab = true;
            this.inputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputBox.Location = new System.Drawing.Point(5, 23);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(243, 65);
            this.inputBox.TabIndex = 2;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyDown);
            this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputBox_KeyPress);
            // 
            // outputBox
            // 
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Location = new System.Drawing.Point(254, 23);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(244, 65);
            this.outputBox.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 166);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.uOpsList);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.nextuOpBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(342, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(161, 166);
            this.panel4.TabIndex = 1;
            // 
            // uOpsList
            // 
            this.uOpsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uOpsList.FormattingEnabled = true;
            this.uOpsList.Location = new System.Drawing.Point(5, 23);
            this.uOpsList.Name = "uOpsList";
            this.uOpsList.Size = new System.Drawing.Size(151, 95);
            this.uOpsList.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(5, 118);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(102, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Next µOperations:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(5, 5);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.label4.Size = new System.Drawing.Size(122, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Executed µOperations";
            // 
            // nextuOpBox
            // 
            this.nextuOpBox.BackColor = System.Drawing.Color.White;
            this.nextuOpBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nextuOpBox.ForeColor = System.Drawing.Color.Red;
            this.nextuOpBox.Location = new System.Drawing.Point(5, 141);
            this.nextuOpBox.Name = "nextuOpBox";
            this.nextuOpBox.ReadOnly = true;
            this.nextuOpBox.Size = new System.Drawing.Size(151, 20);
            this.nextuOpBox.TabIndex = 3;
            this.nextuOpBox.Text = "- Machine Initialized";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel3);
            this.panel5.Controls.Add(this.tableLayoutPanel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(342, 166);
            this.panel5.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label11, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label12, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label13, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label14, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.sCheckBox, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.iCheckBox, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.eCheckBox, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.fgiCheckBox, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.fgoCheckBox, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.scTextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.arTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.drTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.inprTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.trTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.pcTextBox, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.irTextBox, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.acTextBox, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.outrTextBox, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.rCheckBox, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.ienCheckBox, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.ticksCountLabel, 4, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(342, 137);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label6.Size = new System.Drawing.Size(26, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "SC:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label7.Size = new System.Drawing.Size(27, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "AR:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label8.Size = new System.Drawing.Size(28, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "DR:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 80);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label9.Size = new System.Drawing.Size(38, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "INPR:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 105);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label10.Size = new System.Drawing.Size(27, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "TR:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(172, 5);
            this.label11.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label11.Size = new System.Drawing.Size(26, 20);
            this.label11.TabIndex = 5;
            this.label11.Text = "PC:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(172, 30);
            this.label12.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label12.Size = new System.Drawing.Size(23, 20);
            this.label12.TabIndex = 6;
            this.label12.Text = "IR:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(172, 55);
            this.label13.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label13.Size = new System.Drawing.Size(26, 20);
            this.label13.TabIndex = 7;
            this.label13.Text = "AC:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(172, 80);
            this.label14.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.label14.Size = new System.Drawing.Size(43, 20);
            this.label14.TabIndex = 8;
            this.label14.Text = "OUTR:";
            // 
            // sCheckBox
            // 
            this.sCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.sCheckBox.AutoSize = true;
            this.sCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.sCheckBox.Location = new System.Drawing.Point(125, 8);
            this.sCheckBox.Name = "sCheckBox";
            this.sCheckBox.Size = new System.Drawing.Size(34, 19);
            this.sCheckBox.TabIndex = 9;
            this.sCheckBox.Text = "S";
            this.sCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sCheckBox.UseVisualStyleBackColor = true;
            this.sCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // iCheckBox
            // 
            this.iCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.iCheckBox.AutoSize = true;
            this.iCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.iCheckBox.Location = new System.Drawing.Point(294, 33);
            this.iCheckBox.Name = "iCheckBox";
            this.iCheckBox.Size = new System.Drawing.Size(40, 19);
            this.iCheckBox.TabIndex = 10;
            this.iCheckBox.Text = "I";
            this.iCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.iCheckBox.UseVisualStyleBackColor = true;
            this.iCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // eCheckBox
            // 
            this.eCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.eCheckBox.AutoSize = true;
            this.eCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.eCheckBox.Location = new System.Drawing.Point(294, 58);
            this.eCheckBox.Name = "eCheckBox";
            this.eCheckBox.Size = new System.Drawing.Size(40, 19);
            this.eCheckBox.TabIndex = 11;
            this.eCheckBox.Text = "E";
            this.eCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.eCheckBox.UseVisualStyleBackColor = true;
            this.eCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // fgiCheckBox
            // 
            this.fgiCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fgiCheckBox.AutoSize = true;
            this.fgiCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.fgiCheckBox.Location = new System.Drawing.Point(125, 83);
            this.fgiCheckBox.Name = "fgiCheckBox";
            this.fgiCheckBox.Size = new System.Drawing.Size(34, 19);
            this.fgiCheckBox.TabIndex = 12;
            this.fgiCheckBox.Text = "FGI";
            this.fgiCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fgiCheckBox.UseVisualStyleBackColor = true;
            this.fgiCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // fgoCheckBox
            // 
            this.fgoCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fgoCheckBox.AutoSize = true;
            this.fgoCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.fgoCheckBox.Location = new System.Drawing.Point(294, 83);
            this.fgoCheckBox.Name = "fgoCheckBox";
            this.fgoCheckBox.Size = new System.Drawing.Size(40, 19);
            this.fgoCheckBox.TabIndex = 13;
            this.fgoCheckBox.Text = "FGO";
            this.fgoCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fgoCheckBox.UseVisualStyleBackColor = true;
            this.fgoCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // scTextBox
            // 
            this.scTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.scTextBox.Location = new System.Drawing.Point(49, 8);
            this.scTextBox.Name = "scTextBox";
            this.scTextBox.Size = new System.Drawing.Size(70, 20);
            this.scTextBox.TabIndex = 16;
            this.scTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // arTextBox
            // 
            this.arTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.arTextBox.Location = new System.Drawing.Point(49, 33);
            this.arTextBox.Name = "arTextBox";
            this.arTextBox.Size = new System.Drawing.Size(70, 20);
            this.arTextBox.TabIndex = 17;
            this.arTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // drTextBox
            // 
            this.drTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.drTextBox.Location = new System.Drawing.Point(49, 58);
            this.drTextBox.Name = "drTextBox";
            this.drTextBox.Size = new System.Drawing.Size(70, 20);
            this.drTextBox.TabIndex = 18;
            this.drTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // inprTextBox
            // 
            this.inprTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.inprTextBox.Location = new System.Drawing.Point(49, 83);
            this.inprTextBox.Name = "inprTextBox";
            this.inprTextBox.Size = new System.Drawing.Size(70, 20);
            this.inprTextBox.TabIndex = 19;
            this.inprTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // trTextBox
            // 
            this.trTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.trTextBox.Location = new System.Drawing.Point(49, 108);
            this.trTextBox.Name = "trTextBox";
            this.trTextBox.Size = new System.Drawing.Size(70, 20);
            this.trTextBox.TabIndex = 20;
            this.trTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // pcTextBox
            // 
            this.pcTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTextBox.Location = new System.Drawing.Point(218, 8);
            this.pcTextBox.Name = "pcTextBox";
            this.pcTextBox.Size = new System.Drawing.Size(70, 20);
            this.pcTextBox.TabIndex = 21;
            this.pcTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // irTextBox
            // 
            this.irTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.irTextBox.Location = new System.Drawing.Point(218, 33);
            this.irTextBox.Name = "irTextBox";
            this.irTextBox.Size = new System.Drawing.Size(70, 20);
            this.irTextBox.TabIndex = 22;
            this.irTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // acTextBox
            // 
            this.acTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.acTextBox.Location = new System.Drawing.Point(218, 58);
            this.acTextBox.Name = "acTextBox";
            this.acTextBox.Size = new System.Drawing.Size(70, 20);
            this.acTextBox.TabIndex = 23;
            this.acTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // outrTextBox
            // 
            this.outrTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.outrTextBox.Location = new System.Drawing.Point(218, 83);
            this.outrTextBox.Name = "outrTextBox";
            this.outrTextBox.Size = new System.Drawing.Size(70, 20);
            this.outrTextBox.TabIndex = 24;
            this.outrTextBox.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // rCheckBox
            // 
            this.rCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.rCheckBox.AutoSize = true;
            this.rCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.rCheckBox.Location = new System.Drawing.Point(125, 108);
            this.rCheckBox.Name = "rCheckBox";
            this.rCheckBox.Size = new System.Drawing.Size(34, 21);
            this.rCheckBox.TabIndex = 14;
            this.rCheckBox.Text = "R";
            this.rCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rCheckBox.UseVisualStyleBackColor = true;
            this.rCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // ienCheckBox
            // 
            this.ienCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ienCheckBox.AutoSize = true;
            this.ienCheckBox.Location = new System.Drawing.Point(165, 108);
            this.ienCheckBox.Name = "ienCheckBox";
            this.ienCheckBox.Size = new System.Drawing.Size(35, 21);
            this.ienCheckBox.TabIndex = 15;
            this.ienCheckBox.Text = "IEN";
            this.ienCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ienCheckBox.UseVisualStyleBackColor = true;
            this.ienCheckBox.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // ticksCountLabel
            // 
            this.ticksCountLabel.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.ticksCountLabel, 2);
            this.ticksCountLabel.Location = new System.Drawing.Point(218, 108);
            this.ticksCountLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ticksCountLabel.Name = "ticksCountLabel";
            this.ticksCountLabel.Padding = new System.Windows.Forms.Padding(2, 5, 0, 2);
            this.ticksCountLabel.Size = new System.Drawing.Size(44, 20);
            this.ticksCountLabel.TabIndex = 25;
            this.ticksCountLabel.Text = "0 Ticks";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.resetButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tickButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.nextButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.runButton, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 137);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(342, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // resetButton
            // 
            this.resetButton.AutoSize = true;
            this.resetButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetButton.Location = new System.Drawing.Point(3, 3);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(62, 23);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // tickButton
            // 
            this.tickButton.AutoSize = true;
            this.tickButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tickButton.Location = new System.Drawing.Point(71, 3);
            this.tickButton.Name = "tickButton";
            this.tickButton.Size = new System.Drawing.Size(96, 23);
            this.tickButton.TabIndex = 1;
            this.tickButton.Text = "Next µOperation";
            this.tickButton.UseVisualStyleBackColor = true;
            this.tickButton.Click += new System.EventHandler(this.tickButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.AutoSize = true;
            this.nextButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Location = new System.Drawing.Point(173, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(96, 23);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Next Instruction";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // runButton
            // 
            this.runButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.runButton.AutoCheck = false;
            this.runButton.AutoSize = true;
            this.runButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runButton.Location = new System.Drawing.Point(275, 3);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(64, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Start";
            this.runButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // statusTimer
            // 
            this.statusTimer.Enabled = true;
            this.statusTimer.Interval = 10;
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // pcPointerColumn
            // 
            this.pcPointerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pcPointerColumn.HeaderText = "   ";
            this.pcPointerColumn.Name = "pcPointerColumn";
            this.pcPointerColumn.ReadOnly = true;
            this.pcPointerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pcPointerColumn.Width = 20;
            // 
            // addressColumn
            // 
            this.addressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.addressColumn.HeaderText = "Address";
            this.addressColumn.Name = "addressColumn";
            this.addressColumn.ReadOnly = true;
            this.addressColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.addressColumn.Width = 51;
            // 
            // contentColumn
            // 
            this.contentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.contentColumn.HeaderText = "Content";
            this.contentColumn.Name = "contentColumn";
            this.contentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.contentColumn.Width = 50;
            // 
            // commentColumn
            // 
            this.commentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentColumn.HeaderText = "Comment";
            this.commentColumn.MinimumWidth = 100;
            this.commentColumn.Name = "commentColumn";
            this.commentColumn.ReadOnly = true;
            this.commentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SimulatorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "SimulatorBox";
            this.Size = new System.Drawing.Size(744, 259);
            ((System.ComponentModel.ISupportInitialize)(this.memoryGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView memoryGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox uOpsList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nextuOpBox;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button tickButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox sCheckBox;
        private System.Windows.Forms.CheckBox iCheckBox;
        private System.Windows.Forms.CheckBox eCheckBox;
        private System.Windows.Forms.CheckBox fgiCheckBox;
        private System.Windows.Forms.CheckBox fgoCheckBox;
        private System.Windows.Forms.TextBox scTextBox;
        private System.Windows.Forms.TextBox arTextBox;
        private System.Windows.Forms.TextBox drTextBox;
        private System.Windows.Forms.TextBox inprTextBox;
        private System.Windows.Forms.TextBox trTextBox;
        private System.Windows.Forms.TextBox pcTextBox;
        private System.Windows.Forms.TextBox irTextBox;
        private System.Windows.Forms.TextBox acTextBox;
        private System.Windows.Forms.TextBox outrTextBox;
        private System.Windows.Forms.CheckBox rCheckBox;
        private System.Windows.Forms.CheckBox ienCheckBox;
        private System.Windows.Forms.Label ticksCountLabel;
        private System.Windows.Forms.CheckBox runButton;
        private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcPointerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentColumn;
    }
}
