using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Time_Tracker
{
    public partial class Form1 : Form
    {
        //
        //preset data
        private static DateTime Today_date = DateTime.Now;
        OdbcConnection connection = new OdbcConnection("Driver = {MICROSOFT ACCESS DRIVER (*.mdb, *.accdb)}; Dsn=Enterprise_DB;");
        private DateTime startDate = new DateTime(Today_date.Year, Today_date.Month, 1); // Default start date
        private DateTime endDate = new DateTime(Today_date.Year, (Today_date.Month + 1), 1).AddDays(-1); // Default end date
        public static DateTime SelectedCellDate;
        public static string CurrentUser = Environment.UserName;
        public static string SelectedProjectName = "";

        //
        //creating strings of queries
        public static string SetConditionsForRequest = " WHERE @ColumnConditions = ' @SelectConditions ' @SetConditionsForRequest";
        public static string InsertODBCData = "INSERT INTO @SelectedTableName ( @ListOfColumnsInTable ) VALUES @InsertDataValues";
        public static string UpdateODBCData = "UPDATE @SelectedTableName SET @SelectedColmnName = ' @NewODBCData ' @SetConditionsForRequest";
        public static string GetODBCData = $"SELECT @SelectedColmnName FROM @SelectedTableName @SetConditionsForRequest";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Projects' table. You can move, or remove it, as needed.
            this.projectsTableAdapter.Fill(this.dataSet1.Projects);

            radioButton1.Checked = true;
            label1.Text = CurrentUser;
            comboBox1.Text = "";

            UpdateDateRangeLabel();
            
        }

        //
        //create cells Form-1 cells row
        private void CreateDynamicCells(DateTime startDate, DateTime? endDate)
        {
            //clear panel before populating
            ClearDynamicCells();
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            var end = endDate ?? new DateTime(startDate.Year, startDate.Month, daysInMonth);
            var totalDays = (end - startDate).Days + 1;

            // Create a FlowLayoutPanel to hold all cells horizontally
            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            for (int i = 0; i < totalDays; i++)
            {
                var date = startDate.AddDays(i);

                // Create a FlowLayoutPanel for each cell
                FlowLayoutPanel cellPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 110,
                    Height = 180,
                    Margin = new Padding(5),
                    Tag = new { CellOpend = false, CellDate = date } // To keep track of expanded state
                };                

                // Date label
                Label lblDate = new Label
                {
                    Text = date.ToString("dd"),
                    Anchor = AnchorStyles.Top,
                    Padding = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 18, FontStyle.Bold),
                    AutoSize = true,
                    Tag = new { CellDate = date },
                };
                cellPanel.Controls.Add(lblDate);

                // Day of the week label
                Label lblDayOfWeek = new Label
                {
                    Text = date.ToString("ddd", CultureInfo.InvariantCulture),
                    Anchor = AnchorStyles.Top,
                    Padding = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 18, FontStyle.Bold),
                    AutoSize = true,
                    Tag = new { CellDate = date },
                };
                cellPanel.Controls.Add(lblDayOfWeek);

                // TextBox for number entry
                TextBox txtNumber = new TextBox
                {
                    Width = 50,
                    Anchor = AnchorStyles.Top,
                    Margin = new Padding(5),
                    TextAlign = HorizontalAlignment.Center,
                    Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                    Tag = new { CellDate = date },
                };
                if (date > DateTime.Now)
                {
                    txtNumber.Enabled = false;
                    txtNumber.BackColor = Color.LightGray;
                    txtNumber.BorderStyle = BorderStyle.None;
                }
                txtNumber.KeyPress += (sender, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
                    {
                        e.Handled = true;
                    }
                };
                cellPanel.Controls.Add(txtNumber);

                // Button to expand cell
                Label btnExpand = new Label
                {
                    Text = "Details",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                    Anchor = AnchorStyles.Bottom,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand,
                    Tag = new {CellDate = date},
                };
                btnExpand.Click += CellPanel_Click;
                cellPanel.Controls.Add(btnExpand);

                // Add the cell FlowLayoutPanel to the main flow panel
                flowPanel.Controls.Add(cellPanel);
                
            }
            
            // Add the main flow panel to the main panel
            mainPanel.Controls.Add(flowPanel);
        }

        private void ClearDynamicCells()
        {
            foreach (Control control in mainPanel.Controls)
            {
                mainPanel.Controls.Remove(control);
                control.Dispose();
            }
        }

        //
        //Data update in window
        private void UpdateDateRangeLabel()
        {
            // Update the label text with the selected date range
            DateSelecter.Text = startDate.ToString("d/MMM/yy") + " - " + endDate.ToString("d/MMM/yy");
            ClearDynamicCells();
            CreateDynamicCells(startDate, endDate);
            ClearCalendar();
            PopulateCalendar(startDate, endDate);
            
            ScrollToPosition(mainPanel);
            ScrollToPosition(mainPanel2);
        }

        private void ScrollToPosition(FlowLayoutPanel flowPanel)
        {
            if (flowPanel == mainPanel)  
                foreach (Control control in flowPanel.Controls)
                    foreach (Control control2 in control.Controls)
                        if (((dynamic)control2.Tag).CellDate == DateTime.Now.Date.AddDays(2))
                        {
                            flowPanel.ScrollControlIntoView(control2);
                        }

            /*
            // button.Tag= new [name] {Count = btnCount, FileName=soundFfilepath);
            // int count=((dynamic)button.Tag).Count;
            // string filename = ((dynamic)button.Tag).FileName;
            // flowPanel.HorizontalScroll.Equals(CellPositionX);
            // control2.FindForm().PointToClient(control2.Parent.PointToScreen(control2.Location));
            */


            if (flowPanel == mainPanel2)            
                foreach (Control control in flowPanel.Controls)
                    foreach (Control control2 in control.Controls)
                        foreach (Control control3 in control2.Controls)                            
                            if (((dynamic)control3.Tag).CellDate == DateTime.Now.Date)
                            {
                                flowPanel.ScrollControlIntoView(control3);
                            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateRangeLabel();
        }

        //
        //create cells Form-2 in window
        private void PopulateCalendar(DateTime startDate, DateTime endDate)
        {
            // Clear the main panel before adding new cells
            mainPanel2.Controls.Clear();

            // Save the original start and end dates
            DateTime originalStartDate = startDate;
            DateTime originalEndDate = endDate;

            // Adjust the start date to the previous Monday if it's not already a Monday
            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            // Adjust the end date to the next Sunday if it's not already a Sunday
            while (endDate.DayOfWeek != DayOfWeek.Sunday)
            {
                endDate = endDate.AddDays(1);
            }

            DateTime currentDate = startDate;
            FlowLayoutPanel weekPanel = CreateNewWeekPanel();

            while (currentDate <= endDate)
            {
                // Create a blank cell for dates outside the original range
                Panel cellPanel = (currentDate < originalStartDate || currentDate > originalEndDate) ? CreateBlankCellPanel() : CreateCellPanel(currentDate);

                // Add the cell to the current week panel
                weekPanel.Controls.Add(cellPanel);

                // Start a new week if it's Sunday
                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    // Add the current week panel to the main panel
                    mainPanel2.Controls.Add(weekPanel);
                    weekPanel = CreateNewWeekPanel();
                }

                // Move to the next day
                currentDate = currentDate.AddDays(1);
            }

            // Add the last week panel if it hasn't been added yet
            if (weekPanel.Controls.Count > 0)
            {
                mainPanel2.Controls.Add(weekPanel);
            }
        }

        private FlowLayoutPanel CreateNewWeekPanel()
        {
            return new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Height = 100, // Set a height for week panels
                Width = mainPanel2.ClientSize.Width // Ensure it spans the width of the main panel
            };
        }

        private Panel CreateCellPanel(DateTime date)
        {
            Panel cellPanel1 = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = 80,
                Height = 80,
                Margin = new Padding(4),
                Cursor = Cursors.Hand,
                Tag = new { CellDate = date } // To keep track of expanded state
            };

            
            // Create an inner FlowLayoutPanel to manage the layout of child controls
            FlowLayoutPanel innerPanel1 = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false,
                AutoScroll = false,
                Anchor = AnchorStyles.None,
                Padding = new Padding(12),
                Cursor = Cursors.Hand,
                Tag = new { CellDate = date }
            };
            innerPanel1.Click += CellPanel_Click;
            

            // Date label
            Label lblDate1 = new Label
            {
                Text = date.ToString("dd"),
                Anchor = AnchorStyles.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 18, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(5,0,5,0),
                Cursor = Cursors.Hand,
                Tag = new { CellDate = date }
            };
            lblDate1.Click += CellPanel_Click;
            innerPanel1.Controls.Add(lblDate1);

            // Day of the week label
            Label lblDayOfWeek = new Label
            {
                Text = date.ToString("ddd", CultureInfo.InvariantCulture),
                Anchor = AnchorStyles.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                AutoSize = true,
                Cursor = Cursors.Hand,
                Tag = new { CellDate = date }
            };
            lblDayOfWeek.Click += CellPanel_Click;

            innerPanel1.Controls.Add(lblDayOfWeek);

            // Attach the click event handler
            cellPanel1.Click += CellPanel_Click;
            
            // Add the inner panel to the cell panel
            cellPanel1.Controls.Add(innerPanel1);

            return cellPanel1;
            
        }

        private Panel CreateBlankCellPanel()
        {
            return new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = 80,
                Height = 80,
                Margin = new Padding(4),
                Tag = false // To keep track of expanded state
            };
        }
        
        private void ClearCalendar()
        {
            foreach (Control control in mainPanel2.Controls)
            {
                mainPanel2.Controls.Remove(control);
                control.Dispose();
            }
        }
        
        //
        // This is the event handler method for cell clicks
        private void CellPanel_Click(object sender, EventArgs e)
        {
            Control clickedControl = (Control)sender;
            Panel cellPanel = clickedControl is Panel ? (Panel)clickedControl : (Panel)clickedControl.Parent;
            DateTime date = ((dynamic)cellPanel.Tag).CellDate;
            SelectedProjectName = comboBox1.Text;
            OpenDatailedView(date);
        }

        //
        //Form2 for datailed view of selected date
        private void OpenDatailedView(DateTime date)
        {
            SelectedCellDate = date;
            Form2 form = new Form2();
            if(SelectedProjectName == "")
            {
                MessageBox.Show("Selected project before using datailed view", "No project selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    form.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        //
        //Window controls
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                tableLayoutPanel3.Enabled = true;
                tableLayoutPanel3.Visible = true;
                radioButton2.Checked = false;
            }
            else
            {
                tableLayoutPanel3.Enabled = false;
                tableLayoutPanel3.Visible = false;
            }
            UpdateDateRangeLabel();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                tableLayoutPanel4.Enabled = true;
                tableLayoutPanel4.Visible = true;
                radioButton1.Checked = false;
            }
            else
            {
                tableLayoutPanel4.Enabled = false;
                tableLayoutPanel4.Visible = false;
            }
            UpdateDateRangeLabel();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            // Set the start and end dates one week earlier
            startDate = startDate.AddMonths(-1);
            // Calculate the end date for the new month
            endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
            UpdateDateRangeLabel();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            // Set the start and end dates one week later
            startDate = startDate.AddMonths(1);
            // Calculate the end date for the new month
            endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
            UpdateDateRangeLabel();
        }

        private void TodayBtn_Click(object sender, EventArgs e)
        {
            startDate = new DateTime(Today_date.Year, Today_date.Month, 1); // Default start date
            endDate = new DateTime(Today_date.Year, Today_date.Month + 1, 1).AddDays(-1);
            UpdateDateRangeLabel();
        }


        //
        //Loading db data to programm
        private void LoadingData_toSystem()
        {
            string SelectedTable = "Users";
            string SelectedColumns = "*";
            string Conditions = "WHERE User_name = " + CurrentUser;
            connection.Open();
            using (OdbcCommand GetUserData = new OdbcCommand(GetODBCData, connection))
            {
                // Add parameters to the command
                GetUserData.Parameters.AddWithValue("@SelectedTableName", SelectedTable);
                GetUserData.Parameters.AddWithValue("@SelectedColmnName", SelectedColumns);
                GetUserData.Parameters.AddWithValue("@SetConditionsForRequest", Conditions);

                // Execute the command
                int rowsAffected = GetUserData.ExecuteNonQuery();
                MessageBox.Show($"{rowsAffected} rows updated.");
            }
            //
            /*
                 using (OdbcCommand command = new OdbcCommand(LoadGetODBCData, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@SelectedColmnName", SelectedColumns);
                            command.Parameters.AddWithValue("@SelectedTableName", SelectedTable);
                            command.Parameters.AddWithValue("@SetConditionsForRequest", Conditions);

                            // Execute the command
                            int rowsAffected = command.ExecuteNonQuery();
                            MessageBox.Show($"{rowsAffected} rows updated.");
                        }

            ///

                using (OdbcCommand GetUserData = new OdbcCommand(GetODBCData, connection))
                    {
                        while (LastUserTime.Read())
                        {
                            mem = LastUserTime.GetString(0);
                        }
                    }
                    label3.Text = mem.ToString();
            */
            //Loading data from DB
            try
            {
                string SelectedTable = "WorkTime";
                string SelectedColumns = "*";
                string Conditions = "WHERE User_ID = ";
                connection.Open();
                using (OdbcCommand GetUserData = new OdbcCommand(GetODBCData, connection))
                {
                    // Add parameters to the command
                    GetUserData.Parameters.AddWithValue("@SelectedTableName", SelectedTable);
                    GetUserData.Parameters.AddWithValue("@SelectedColmnName", SelectedColumns);
                    GetUserData.Parameters.AddWithValue("@SetConditionsForRequest", Conditions);

                    // Execute the command
                    int rowsAffected = GetUserData.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} rows updated.");
                }
            }
            catch (Exception ex)
            {
                label3.Text = "00:00:00";
            }
            finally
            {
                connection.Close();
            }
        }


        /*
        private void RefreshTaskTable()
        {
            string SelectedProjectName = null;
            if (comboBox1.SelectedItem != null)
            {
                SelectedProjectName = comboBox1.Text;
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = "Assigned_project like '%" + SelectedProjectName + "%' AND Assigned_user like '%" + label1.Text + "%'";
            dataGridView1.DataSource = bs.DataSource;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTaskTable();
            Refresh_counter();
        }
        private void Refresh_counter()
        {
            var SelectedProject = comboBox1.Text;
            var CurentUser = label1.Text;
            var mem = "00:00:00";
            string UserTime = "SELECT Time_spent FROM [" + SelectedProject + "] WHERE Work_day = Date() AND User_name = '" + CurentUser + "'";
            //
            //Loading user time spent on this project today
            OdbcCommand GetUserTime = new OdbcCommand(UserTime, connection);
            try
            {
                connection.Open();
                using (OdbcDataReader LastUserTime = GetUserTime.ExecuteReader())
                {
                    while (LastUserTime.Read())
                    {
                        mem = LastUserTime.GetString(0);
                    }
                }
                label3.Text = mem.ToString();
            }
            catch (Exception ex)
            {
                label3.Text = "00:00:00";
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //set up var to operate in btn click
            var SelectedProject = comboBox1.Text;
            var CurentUser = label1.Text;
            var WorkTime = label3.Text;
            var WorkStart = System.DateTime.Now.ToString("HH:mm:ss");
            string RegistredUser = null;
            string RecentData = null;
            bool TableExist = false;
            //
            //preparing SQL queries for data compare
            string GetUserData = "SELECT User_name FROM Users WHERE User_name = '" + CurentUser + "'";
            string GetRecentRecords_FromThisUser = "SELECT COUNT(*) FROM Work_time WHERE Work_day_date = Date() AND User_name = '" + CurentUser + "'";
            string GetProjectRecords_FromThisUser = "SELECT COUNT(*) FROM ["+ SelectedProject + "] WHERE Work_day = Date() AND User_name = '" + CurentUser + "'";
            string TableCheck = "select * FROM [" + SelectedProject + "]";
            //
            // Open the connection to DB
            connection.Open();
            //
            //selected project check
            try
            {
                using (OdbcCommand SelectedTable = new OdbcCommand(TableCheck, connection))
                {
                    //Look in DB for a table
                    SelectedTable.ExecuteScalar();
                    TableExist = true;
                    //                  
                    //set up queries using Odbc
                    OdbcCommand UserDBCheck = new OdbcCommand(GetUserData, connection);
                    OdbcCommand RecentRecords = new OdbcCommand(GetRecentRecords_FromThisUser, connection);
                    OdbcCommand ProjectRecords = new OdbcCommand(GetProjectRecords_FromThisUser, connection);
                    //
                    //Bring to display stop button
                    button2.Visible = true;
                    button2.Enabled = true;
                    //
                    //Hide start button
                    button1.Enabled = false;
                    button1.Visible = false;
                    //
                    //executing prepared queries
                    //
                    //checking User table in DB for current user
                    OdbcDataReader ConectingUser = UserDBCheck.ExecuteReader();
                    //
                    //
                    //checking Work_time table in DB for user today records
                    int RecentUserRecords = (int)RecentRecords.ExecuteScalar();
                    //
                    //
                    //checking Work_time table in DB for user today records
                    int RecentProjectRecords = (int)ProjectRecords.ExecuteScalar();
                    //



                    while (ConectingUser.Read())
                    {
                        RegistredUser = ConectingUser.GetString(0);
                    }
                    ConectingUser.Close();
                    //
                    //
                    //work time update sys
                    //
                    timer2.Tick += (s, ev) =>
                        {
                            // Parse the label's text as a TimeSpan
                            TimeSpan currentTime = TimeSpan.ParseExact(label3.Text, "hh\\:mm\\:ss", null);

                            // Add one second to the current time
                            TimeSpan newTime = currentTime.Add(TimeSpan.FromSeconds(1));

                            // Format the new time as a string in the format 00:00:00
                            string formattedTime = newTime.ToString("hh\\:mm\\:ss");

                            // Update the label's text with the new time
                            label3.Text = formattedTime;
                        };

                    //checking for aprop data

                    //user in db check

                    if (RegistredUser != "none")
                    {

                        comboBox1.Enabled = false;
                        //activating counters for livetime system functionality
                        timer1.Start();
                        timer2.Start();

                        //checking for records for current user in current day

                        if (RecentUserRecords == 0 && RecentProjectRecords == 0)
                        {

                            // Construct the SQL query to insert data to selected 'Project' table
                            string query_set_ProjectWorkTime = "INSERT INTO [" + SelectedProject + "] ([Work_day], [User_name], [Time_spent])" +
                                                                                    " VALUES ( date() , '" + CurentUser + "', '00:00:00')";
                            // Construct the SQL query to insert data to Work_time table
                            string query_set_UserWorkTime = "INSERT INTO [Work_time] ([Work_day_date], [User_name], [Project_name], [Work_time_start], [Work_time_end])" +
                                                                    " VALUES ( date(), '" + CurentUser + "', '" + SelectedProject + "', '" + WorkStart + "', '00:00:00')";

                            try
                            {
                                // Create a commands object for sending data
                                OdbcCommand createData_Project = new OdbcCommand(query_set_ProjectWorkTime, connection);
                                OdbcCommand createData_WorkTime = new OdbcCommand(query_set_UserWorkTime, connection);

                                // Execute the command
                                createData_Project.ExecuteNonQuery();
                                createData_WorkTime.ExecuteNonQuery();

                                //MessageBox.Show("Data inserted successfully!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "conection error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You are continuing the work, good luck!", "Resuming work", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }

                    }
                    else
                    {
                        MessageBox.Show("You are not registred in DB ", "Unregistered user", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }

                }

            }
            catch
            {
                TableExist = false;
                MessageBox.Show("No such project are created", "Not valid project selected", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            connection.Close();     
        }

        private void UpdateData(object sender, EventArgs e)
        {
            
            //geting values for functions
            var CurentUser = label1.Text;
            var SelectedProject = comboBox1.Text;

            //seting up sql queries
            string WorkTimeUpdate = "UPDATE [Work_time] SET [Work_time_end] = Date() WHERE [Work_day_date] = date() AND [User_name] = '"+ CurentUser + "'";
            string ProjectTimeUpdate = "UPDATE [" + SelectedProject + "] SET [Time_spent] = DateAdd('s',5,[Time_spent]) WHERE Work_day = date() AND User_name = '" + CurentUser + "'";
            //exequte queries
            OdbcCommand UpdateWorkTime = new OdbcCommand(WorkTimeUpdate, connection);
            OdbcCommand UpdateProjectTime = new OdbcCommand(ProjectTimeUpdate, connection);

            
            try
            {
                //conecting to DB
                connection.Open();
                UpdateWorkTime.ExecuteNonQuery();
                UpdateProjectTime.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //closing connection to DB
                connection.Close();
            }
            
            //MessageBox.Show("connection succes", "some text", MessageBoxButtons.OK, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Bring to display start button
            button1.Visible = true;
            button1.Enabled = true;
            //
            //Hide stop button
            button2.Enabled = false;
            button2.Visible = false;
            //
            //Enabeling project selection
            comboBox1.Enabled = true;
            //
            //Stop programm timers
            timer1.Stop();
            timer2.Stop();
            
            //sending final data
            Stop_work();
        }

        private void Stop_work()
        {
            //Seting up var for queries
            var WorkEnd = System.DateTime.Now.ToString("HH:mm:ss");
            var TotalWorkTime = label3.Text;
            var CurentUser = label1.Text;
            var SelectedProject = comboBox1.Text;
            //
            //Seting up queries
            string Q_WorkEnd = "UPDATE [Work_time] SET [Work_time_end] = '" + WorkEnd + "' WHERE [Work_day_date] = date() AND [User_name] = '"+ CurentUser + "'";
            string Q_TotalWorkTime = "UPDATE [" + SelectedProject + "] SET [Time_spent] = '" + TotalWorkTime + "' WHERE Work_day = date() AND User_name = '" + CurentUser + "'";
            //
            //
            OdbcCommand SendWorkEnd = new OdbcCommand(Q_WorkEnd, connection);
            OdbcCommand SendTotalWorkTime = new OdbcCommand(Q_TotalWorkTime, connection);
            //
            //Exquite queries
            try
            {
                connection.Open();
                SendWorkEnd.ExecuteNonQuery();
                SendTotalWorkTime.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //sending final data
            Stop_work();
        }
        */

    }
}
