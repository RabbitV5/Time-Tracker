using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Time_Tracker
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DateSelecter = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.TodayBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.mainPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MonthLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DayLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TasksPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.NotesPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NotesWindow = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.TabPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CalendarButton = new System.Windows.Forms.FlowLayoutPanel();
            this.CalendarButtonText = new System.Windows.Forms.Label();
            this.TasksButton = new System.Windows.Forms.FlowLayoutPanel();
            this.TasksButtonText = new System.Windows.Forms.Label();
            this.NotesButton = new System.Windows.Forms.FlowLayoutPanel();
            this.NotesButtonText = new System.Windows.Forms.Label();
            this.ManageUsersButton = new System.Windows.Forms.FlowLayoutPanel();
            this.ManageUsersButtonText = new System.Windows.Forms.Label();
            this.CalendarPanel = new System.Windows.Forms.Panel();
            this.MainWindow = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.NotesbindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new Time_Tracker.DataSet1();
            this.projectsTableAdapter = new Time_Tracker.DataSet1TableAdapters.ProjectsTableAdapter();
            this.workTimeTableAdapter1 = new Time_Tracker.DataSet1TableAdapters.WorkTimeTableAdapter();
            this.usersTableAdapter1 = new Time_Tracker.DataSet1TableAdapters.UsersTableAdapter();
            this.notesTableAdapter1 = new Time_Tracker.DataSet1TableAdapters.NotesTableAdapter();
            this.projectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.MonthLayoutPanel.SuspendLayout();
            this.DayLayoutPanel.SuspendLayout();
            this.TasksPanel.SuspendLayout();
            this.NotesPanel.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.TabPanel.SuspendLayout();
            this.CalendarButton.SuspendLayout();
            this.TasksButton.SuspendLayout();
            this.NotesButton.SuspendLayout();
            this.ManageUsersButton.SuspendLayout();
            this.CalendarPanel.SuspendLayout();
            this.MainWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotesbindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(143, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name";
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.projectsBindingSource;
            this.comboBox1.DisplayMember = "Project_name";
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(141, 10);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(237, 33);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.BindingContextChanged += new System.EventHandler(this.Form1_Load);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.Location = new System.Drawing.Point(6, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Current user:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label7.Location = new System.Drawing.Point(6, 13);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 24);
            this.label7.TabIndex = 12;
            this.label7.Text = "Select project:";
            // 
            // DateSelecter
            // 
            this.DateSelecter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DateSelecter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DateSelecter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateSelecter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.DateSelecter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DateSelecter.Location = new System.Drawing.Point(0, 0);
            this.DateSelecter.Margin = new System.Windows.Forms.Padding(15);
            this.DateSelecter.Name = "DateSelecter";
            this.DateSelecter.Size = new System.Drawing.Size(289, 34);
            this.DateSelecter.TabIndex = 17;
            this.DateSelecter.Text = "Selected month";
            this.DateSelecter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.buttonPrevious);
            this.panel1.Controls.Add(this.DateSelecter);
            this.panel1.Location = new System.Drawing.Point(195, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 34);
            this.panel1.TabIndex = 18;
            // 
            // buttonNext
            // 
            this.buttonNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonNext.Location = new System.Drawing.Point(254, 0);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(35, 34);
            this.buttonNext.TabIndex = 20;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevious.Location = new System.Drawing.Point(0, 0);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(35, 34);
            this.buttonPrevious.TabIndex = 19;
            this.buttonPrevious.Text = "<";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // TodayBtn
            // 
            this.TodayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TodayBtn.Location = new System.Drawing.Point(371, 55);
            this.TodayBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.TodayBtn.Name = "TodayBtn";
            this.TodayBtn.Size = new System.Drawing.Size(115, 27);
            this.TodayBtn.TabIndex = 19;
            this.TodayBtn.Text = "This month";
            this.TodayBtn.UseVisualStyleBackColor = true;
            this.TodayBtn.Click += new System.EventHandler(this.TodayBtn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(531, 63);
            this.flowLayoutPanel1.TabIndex = 23;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(28, 26);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 17);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Days";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(28, 49);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 17);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Months";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // mainPanel2
            // 
            this.mainPanel2.AutoScroll = true;
            this.mainPanel2.AutoSize = true;
            this.mainPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel2.Location = new System.Drawing.Point(3, 27);
            this.mainPanel2.MinimumSize = new System.Drawing.Size(200, 200);
            this.mainPanel2.Name = "mainPanel2";
            this.mainPanel2.Padding = new System.Windows.Forms.Padding(40);
            this.mainPanel2.Size = new System.Drawing.Size(239, 200);
            this.mainPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(177, 76);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View format";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.TodayBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 265);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(488, 110);
            this.panel3.TabIndex = 24;
            // 
            // MonthLayoutPanel
            // 
            this.MonthLayoutPanel.ColumnCount = 1;
            this.MonthLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MonthLayoutPanel.Controls.Add(this.mainPanel2, 0, 1);
            this.MonthLayoutPanel.Enabled = false;
            this.MonthLayoutPanel.Location = new System.Drawing.Point(243, 3);
            this.MonthLayoutPanel.Name = "MonthLayoutPanel";
            this.MonthLayoutPanel.RowCount = 3;
            this.MonthLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.39F));
            this.MonthLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.22F));
            this.MonthLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.39F));
            this.MonthLayoutPanel.Size = new System.Drawing.Size(245, 235);
            this.MonthLayoutPanel.TabIndex = 25;
            this.MonthLayoutPanel.Visible = false;
            // 
            // DayLayoutPanel
            // 
            this.DayLayoutPanel.ColumnCount = 1;
            this.DayLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DayLayoutPanel.Controls.Add(this.mainPanel, 0, 1);
            this.DayLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.DayLayoutPanel.Name = "DayLayoutPanel";
            this.DayLayoutPanel.RowCount = 3;
            this.DayLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DayLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.DayLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DayLayoutPanel.Size = new System.Drawing.Size(234, 256);
            this.DayLayoutPanel.TabIndex = 25;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 18);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(234, 220);
            this.mainPanel.TabIndex = 15;
            this.mainPanel.WrapContents = false;
            // 
            // TasksPanel
            // 
            this.TasksPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.TasksPanel.ColumnCount = 1;
            this.TasksPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TasksPanel.Controls.Add(this.flowLayoutPanel4, 0, 1);
            this.TasksPanel.Controls.Add(this.label2, 0, 0);
            this.TasksPanel.Location = new System.Drawing.Point(58, 26);
            this.TasksPanel.Name = "TasksPanel";
            this.TasksPanel.RowCount = 2;
            this.TasksPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.TasksPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TasksPanel.Size = new System.Drawing.Size(245, 225);
            this.TasksPanel.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoScroll = true;
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(20, 72);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(20, 20, 20, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(205, 150);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 52);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tasks";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotesPanel
            // 
            this.NotesPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.NotesPanel.ColumnCount = 1;
            this.NotesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.NotesPanel.Controls.Add(this.NotesWindow, 0, 1);
            this.NotesPanel.Controls.Add(this.label3, 0, 0);
            this.NotesPanel.Location = new System.Drawing.Point(316, 379);
            this.NotesPanel.Name = "NotesPanel";
            this.NotesPanel.RowCount = 2;
            this.NotesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.NotesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.NotesPanel.Size = new System.Drawing.Size(594, 451);
            this.NotesPanel.TabIndex = 0;
            // 
            // NotesWindow
            // 
            this.NotesWindow.AutoScroll = true;
            this.NotesWindow.AutoSize = true;
            this.NotesWindow.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NotesWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesWindow.Location = new System.Drawing.Point(20, 72);
            this.NotesWindow.Margin = new System.Windows.Forms.Padding(20, 20, 20, 3);
            this.NotesWindow.Name = "NotesWindow";
            this.NotesWindow.Size = new System.Drawing.Size(554, 376);
            this.NotesWindow.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(588, 52);
            this.label3.TabIndex = 2;
            this.label3.Text = "Notes";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Controls.Add(this.comboBox1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(531, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(614, 63);
            this.flowLayoutPanel2.TabIndex = 22;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.35317F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.64683F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 641F));
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1787, 63);
            this.tableLayoutPanel5.TabIndex = 27;
            // 
            // TabPanel
            // 
            this.TabPanel.AutoScroll = true;
            this.TabPanel.AutoSize = true;
            this.TabPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TabPanel.Controls.Add(this.CalendarButton);
            this.TabPanel.Controls.Add(this.TasksButton);
            this.TabPanel.Controls.Add(this.NotesButton);
            this.TabPanel.Controls.Add(this.ManageUsersButton);
            this.TabPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TabPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.TabPanel.Location = new System.Drawing.Point(0, 63);
            this.TabPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TabPanel.Name = "TabPanel";
            this.TabPanel.Size = new System.Drawing.Size(263, 951);
            this.TabPanel.TabIndex = 29;
            // 
            // CalendarButton
            // 
            this.CalendarButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CalendarButton.Controls.Add(this.CalendarButtonText);
            this.CalendarButton.Location = new System.Drawing.Point(0, 0);
            this.CalendarButton.Margin = new System.Windows.Forms.Padding(0);
            this.CalendarButton.Name = "CalendarButton";
            this.CalendarButton.Padding = new System.Windows.Forms.Padding(10);
            this.CalendarButton.Size = new System.Drawing.Size(263, 56);
            this.CalendarButton.TabIndex = 30;
            this.CalendarButton.Click += new System.EventHandler(this.CalendarButton_Click);
            // 
            // CalendarButtonText
            // 
            this.CalendarButtonText.AutoSize = true;
            this.CalendarButtonText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.CalendarButtonText.Location = new System.Drawing.Point(13, 13);
            this.CalendarButtonText.Margin = new System.Windows.Forms.Padding(3);
            this.CalendarButtonText.Name = "CalendarButtonText";
            this.CalendarButtonText.Padding = new System.Windows.Forms.Padding(3);
            this.CalendarButtonText.Size = new System.Drawing.Size(169, 32);
            this.CalendarButtonText.TabIndex = 0;
            this.CalendarButtonText.Text = "CalendarButton";
            this.CalendarButtonText.Click += new System.EventHandler(this.CalendarButton_Click);
            // 
            // TasksButton
            // 
            this.TasksButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TasksButton.Controls.Add(this.TasksButtonText);
            this.TasksButton.Location = new System.Drawing.Point(0, 56);
            this.TasksButton.Margin = new System.Windows.Forms.Padding(0);
            this.TasksButton.Name = "TasksButton";
            this.TasksButton.Padding = new System.Windows.Forms.Padding(10);
            this.TasksButton.Size = new System.Drawing.Size(263, 56);
            this.TasksButton.TabIndex = 31;
            this.TasksButton.Click += new System.EventHandler(this.TasksButton_Click);
            // 
            // TasksButtonText
            // 
            this.TasksButtonText.AutoSize = true;
            this.TasksButtonText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.TasksButtonText.Location = new System.Drawing.Point(13, 13);
            this.TasksButtonText.Margin = new System.Windows.Forms.Padding(3);
            this.TasksButtonText.Name = "TasksButtonText";
            this.TasksButtonText.Padding = new System.Windows.Forms.Padding(3);
            this.TasksButtonText.Size = new System.Drawing.Size(138, 32);
            this.TasksButtonText.TabIndex = 0;
            this.TasksButtonText.Text = "TasksButton";
            // 
            // NotesButton
            // 
            this.NotesButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotesButton.Controls.Add(this.NotesButtonText);
            this.NotesButton.Location = new System.Drawing.Point(0, 112);
            this.NotesButton.Margin = new System.Windows.Forms.Padding(0);
            this.NotesButton.Name = "NotesButton";
            this.NotesButton.Padding = new System.Windows.Forms.Padding(10);
            this.NotesButton.Size = new System.Drawing.Size(263, 56);
            this.NotesButton.TabIndex = 31;
            this.NotesButton.Click += new System.EventHandler(this.NotesButton_Click);
            // 
            // NotesButtonText
            // 
            this.NotesButtonText.AutoSize = true;
            this.NotesButtonText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.NotesButtonText.Location = new System.Drawing.Point(13, 13);
            this.NotesButtonText.Margin = new System.Windows.Forms.Padding(3);
            this.NotesButtonText.Name = "NotesButtonText";
            this.NotesButtonText.Padding = new System.Windows.Forms.Padding(3);
            this.NotesButtonText.Size = new System.Drawing.Size(138, 32);
            this.NotesButtonText.TabIndex = 0;
            this.NotesButtonText.Text = "NotesButton";
            // 
            // ManageUsersButton
            // 
            this.ManageUsersButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManageUsersButton.Controls.Add(this.ManageUsersButtonText);
            this.ManageUsersButton.Location = new System.Drawing.Point(0, 168);
            this.ManageUsersButton.Margin = new System.Windows.Forms.Padding(0);
            this.ManageUsersButton.Name = "ManageUsersButton";
            this.ManageUsersButton.Padding = new System.Windows.Forms.Padding(10);
            this.ManageUsersButton.Size = new System.Drawing.Size(263, 56);
            this.ManageUsersButton.TabIndex = 31;
            // 
            // ManageUsersButtonText
            // 
            this.ManageUsersButtonText.AutoSize = true;
            this.ManageUsersButtonText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.ManageUsersButtonText.Location = new System.Drawing.Point(13, 13);
            this.ManageUsersButtonText.Margin = new System.Windows.Forms.Padding(3);
            this.ManageUsersButtonText.Name = "ManageUsersButtonText";
            this.ManageUsersButtonText.Padding = new System.Windows.Forms.Padding(3);
            this.ManageUsersButtonText.Size = new System.Drawing.Size(216, 32);
            this.ManageUsersButtonText.TabIndex = 0;
            this.ManageUsersButtonText.Text = "ManageUsersButton";
            // 
            // CalendarPanel
            // 
            this.CalendarPanel.Controls.Add(this.panel3);
            this.CalendarPanel.Controls.Add(this.DayLayoutPanel);
            this.CalendarPanel.Controls.Add(this.MonthLayoutPanel);
            this.CalendarPanel.Location = new System.Drawing.Point(27, 270);
            this.CalendarPanel.Name = "CalendarPanel";
            this.CalendarPanel.Size = new System.Drawing.Size(488, 375);
            this.CalendarPanel.TabIndex = 29;
            // 
            // MainWindow
            // 
            this.MainWindow.Controls.Add(this.TasksPanel);
            this.MainWindow.Controls.Add(this.CalendarPanel);
            this.MainWindow.Location = new System.Drawing.Point(1044, 149);
            this.MainWindow.Name = "MainWindow";
            this.MainWindow.Size = new System.Drawing.Size(641, 612);
            this.MainWindow.TabIndex = 30;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.GhostWhite;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.75018F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.24982F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(622, 118);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(376, 159);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // projectsTableAdapter
            // 
            this.projectsTableAdapter.ClearBeforeFill = true;
            // 
            // workTimeTableAdapter1
            // 
            this.workTimeTableAdapter1.ClearBeforeFill = true;
            // 
            // usersTableAdapter1
            // 
            this.usersTableAdapter1.ClearBeforeFill = true;
            // 
            // notesTableAdapter1
            // 
            this.notesTableAdapter1.ClearBeforeFill = true;
            // 
            // projectsBindingSource
            // 
            this.projectsBindingSource.DataMember = "Projects";
            this.projectsBindingSource.DataSource = this.dataSet1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1787, 1014);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.MainWindow);
            this.Controls.Add(this.NotesPanel);
            this.Controls.Add(this.TabPanel);
            this.Controls.Add(this.tableLayoutPanel5);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.Text = "Time Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.MonthLayoutPanel.ResumeLayout(false);
            this.MonthLayoutPanel.PerformLayout();
            this.DayLayoutPanel.ResumeLayout(false);
            this.TasksPanel.ResumeLayout(false);
            this.TasksPanel.PerformLayout();
            this.NotesPanel.ResumeLayout(false);
            this.NotesPanel.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.TabPanel.ResumeLayout(false);
            this.CalendarButton.ResumeLayout(false);
            this.CalendarButton.PerformLayout();
            this.TasksButton.ResumeLayout(false);
            this.TasksButton.PerformLayout();
            this.NotesButton.ResumeLayout(false);
            this.NotesButton.PerformLayout();
            this.ManageUsersButton.ResumeLayout(false);
            this.ManageUsersButton.PerformLayout();
            this.CalendarPanel.ResumeLayout(false);
            this.MainWindow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotesbindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label DateSelecter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button TodayBtn;
        private System.Windows.Forms.FlowLayoutPanel mainPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel3;
        private GroupBox groupBox1;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.ProjectsTableAdapter projectsTableAdapter;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel4;
        private TableLayoutPanel TasksPanel;
        private TableLayoutPanel NotesPanel;
        private FlowLayoutPanel NotesWindow;
        private Label label3;
        private FlowLayoutPanel mainPanel;
        private TableLayoutPanel DayLayoutPanel;
        private TableLayoutPanel MonthLayoutPanel;
        private TableLayoutPanel tableLayoutPanel5;
        private FlowLayoutPanel TabPanel;
        private Panel CalendarPanel;
        private FlowLayoutPanel CalendarButton;
        private Label CalendarButtonText;
        private FlowLayoutPanel TasksButton;
        private Label TasksButtonText;
        private FlowLayoutPanel NotesButton;
        private Label NotesButtonText;
        private FlowLayoutPanel ManageUsersButton;
        private Label ManageUsersButtonText;
        private Panel MainWindow;
        private DataSet1TableAdapters.WorkTimeTableAdapter workTimeTableAdapter1;
        private DataSet1TableAdapters.UsersTableAdapter usersTableAdapter1;
        private TableLayoutPanel tableLayoutPanel1;
        private DataSet1TableAdapters.NotesTableAdapter notesTableAdapter1;
        private BindingSource NotesbindingSource1;
        private BindingSource projectsBindingSource;
    }
}

