using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Tracker
{
    public partial class Form1 : Form
    {
        //
        //preset data
        private static DateTime Today_date = DateTime.Now;
        private static string connectionString = "Driver={MICROSOFT ACCESS DRIVER (*.mdb, *.accdb)}; Dsn=Enterprise_DB;dbq=D:\\Навчання\\4 курс\\Диплом\\Project\\Main_Base\\enterprise.accdb";
        OdbcConnection connection = new OdbcConnection(connectionString);
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
            PopulateNotesForCurrentUserAsync();
        }

        private int GetUserID()
        {
            using (connection)
            {
                connection.Open();
                string query = "SELECT User_ID FROM Users WHERE User_name = ?";
                using (OdbcCommand command = new OdbcCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User_name", CurrentUser);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        //
        //create cells Form-1 cells row

        private async Task PopulateDynamicCellsAsync(DateTime startDate, DateTime? endDate, string projectName)
        {
            ClearDynamicCells();
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            var end = endDate.HasValue ? endDate.Value : new DateTime(startDate.Year, startDate.Month, daysInMonth);
            var totalDays = (end - startDate).Days + 1;

            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            try
            {
                var workTimes = FetchWorkTimes(projectName);
                var verifiedTimes = FetchVerifiedTimes(projectName);

                for (int i = 0; i < totalDays; i++)
                {
                    var date = startDate.AddDays(i);

                    FlowLayoutPanel cellPanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.TopDown,
                        BorderStyle = BorderStyle.FixedSingle,
                        Width = 110,
                        Height = 180,
                        Margin = new Padding(5),
                        Tag = new { CellOpend = false, CellDate = date }
                    };

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

                    TextBox txtNumber = new TextBox
                    {
                        Width = 50,
                        Anchor = AnchorStyles.Top,
                        Margin = new Padding(5),
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                    };

                    if (workTimes.TryGetValue(date, out string workTime))
                    {
                        txtNumber.Text = workTime;
                    }

                    if (date > DateTime.Now)
                    {
                        txtNumber.Enabled = false;
                        txtNumber.BackColor = Color.LightGray;
                        txtNumber.BorderStyle = BorderStyle.None;
                    }
                    
                    if(verifiedTimes.TryGetValue(date, out string verified))
                    {
                        if (verified == "True" )
                        {
                            txtNumber.Enabled = false;
                            txtNumber.BackColor = Color.LightGreen;
                        }
                    }
                    

                    txtNumber.Tag = txtNumber.Text;

                    txtNumber.KeyPress += TxtNumber_KeyPress;

                    txtNumber.TextChanged += TxtNumber_TextChanged;

                    cellPanel.Controls.Add(txtNumber);

                    Label btnExpand = new Label
                    {
                        Text = "Details",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                        Anchor = AnchorStyles.Bottom,
                        Margin = new Padding(5),
                        Cursor = Cursors.Hand,
                        Tag = new { CellDate = date },
                    };
                    btnExpand.Click += CellPanel_Click;
                    cellPanel.Controls.Add(btnExpand);

                    flowPanel.Controls.Add(cellPanel);
                }

                mainPanel.Controls.Add(flowPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating cells: " + ex.Message);
            }
        }



        private async Task PopulateNotesForCurrentUserAsync()
        {
            try
            {
                int userID = GetUserID();

                // Fetch data asynchronously
                await Task.Run(() => notesTableAdapter1.FillByUser_ID(dataSet1.Notes, userID));

                // Bind data to the BindingSource
                NotesbindingSource1.DataSource = dataSet1.Notes;

                foreach (DataRow row in dataSet1.Notes.Rows)
                {
                    var creationDate = row["Creation_date"].ToString();
                    var noteName = row["Note_name"].ToString();
                    var noteTheme = row["Note_theme"].ToString();
                    var content = row["Content"].ToString();
                    var relatedDate = row["Related_date"].ToString();

                    Panel noteBoxBorder = new Panel
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Width = 370,
                        Height = 150,
                        Margin = new Padding(5),
                        MinimumSize = new Size(370, 150),
                        Tag = new { CellOpend = false }
                    };

                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
                    {
                        RowCount = 3,
                        ColumnCount = 2,
                        Dock = DockStyle.Fill,
                        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                    };

                    // Set row heights
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F)); // First row
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F)); // Second row
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Third row

                    // Set column widths
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86F)); // First column
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F)); // Second column

                    noteBoxBorder.Controls.Add(tableLayoutPanel);

                    // First row
                    Label lblName = new Label
                    {
                        Text = noteName,
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    };
                    tableLayoutPanel.Controls.Add(lblName, 0, 0);

                    Label lblCreationDate = new Label
                    {
                        Text = creationDate.ToString(),
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    };
                    tableLayoutPanel.Controls.Add(lblCreationDate, 1, 0);

                    // Second row
                    Label lblTheme = new Label
                    {
                        Text = noteTheme,
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    };
                    tableLayoutPanel.Controls.Add(lblTheme, 0, 1);

                    Label lblRelatedDate = new Label
                    {
                        Text = relatedDate,
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    };
                    tableLayoutPanel.Controls.Add(lblRelatedDate, 1, 1);

                    // Third row
                    Label lblContent = new Label
                    {
                        Text = content,
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    };
                    tableLayoutPanel.Controls.Add(lblContent, 0, 2);

                    // Add the TableLayoutPanel to the Panel
                    noteBoxBorder.Controls.Add(tableLayoutPanel);

                    // Add the Panel to the FlowLayoutPanel
                    NotesWindow.Controls.Add(noteBoxBorder);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //
        //Cell work time box enter data verification
        private void TxtNumber_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (string.IsNullOrEmpty(txt.Text))
            {
                // Allow empty text
                txt.Tag = "";
                return;
            }

            if (decimal.TryParse(txt.Text, out decimal value))
            {
                // Check if number is less than 24.00
                if (value >= 24)
                {
                    MessageBox.Show("Value must be less than 24.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt.TextChanged -= TxtNumber_TextChanged; // Temporarily remove handler to avoid recursion
                    txt.Text = txt.Tag.ToString();
                    txt.TextChanged += TxtNumber_TextChanged; // Reattach handler
                }
                else
                {
                    // Valid value, update the tag to the new valid value
                    txt.Tag = txt.Text;
                }
            }
            else
            {
                MessageBox.Show("Invalid number format. Ensure the number is in a correct format.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt.TextChanged -= TxtNumber_TextChanged;
                txt.Text = txt.Tag.ToString();
                txt.TextChanged += TxtNumber_TextChanged;
            }

            // Move the cursor to the end of the text
            txt.SelectionStart = txt.Text.Length;
        }

        private void TxtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            // Allow only digits and decimal point
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }


        //
        //Saved data for minimise DB load
        private Dictionary<DateTime, string> FetchWorkTimes(string projectName)
        {
            var workTimes = new Dictionary<DateTime, string>();           

            string query = @"
        SELECT Work_day_date, Work_time
        FROM Worktime
        WHERE Project_ID = (SELECT Project_ID FROM Projects WHERE Project_name = ?)
        AND User_ID  =  (SELECT User_ID FROM Users WHERE User_name = ?)";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Project_name", projectName);
                        command.Parameters.AddWithValue("@User_name", CurrentUser);

                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var date = reader.GetDateTime(reader.GetOrdinal("Work_day_date"));
                                var workTime = reader["Work_time"].ToString();
                                workTimes[date] = workTime;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching work times: {ex.Message}\n{ex.StackTrace}");
            }

            return workTimes;
        }

        private Dictionary<DateTime, string> FetchVerifiedTimes(string projectName)
        {
            var verifiedTime = new Dictionary<DateTime, string>();

            string query = @" SELECT Work_day_date, IsVerified
            FROM Worktime
            WHERE Project_ID = (SELECT Project_ID FROM Projects WHERE Project_name = ?)
            AND User_ID = (SELECT User_ID FROM Users WHERE User_name = ?)";
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Project_name", projectName);
                        command.Parameters.AddWithValue("@User_name", CurrentUser);

                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var verified = reader["IsVerified"].ToString();
                                var date = reader.GetDateTime(reader.GetOrdinal("Work_day_date"));
                                verifiedTime[date] = verified;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching work times: {ex.Message}\n{ex.StackTrace}");
            }

            return verifiedTime;
        }

        private Dictionary<string, DateTime> FetchProjectStartDates()
        {
            var ProjectBeginDates = new Dictionary<string, DateTime>();

            string query = @" SELECT Project_name, Project_creation_date
            FROM Projects";
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var name = reader["Project_name"].ToString();
                                var Start = reader.GetDateTime(reader.GetOrdinal("Project_creation_date"));
                                ProjectBeginDates[name] = Start;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching work times: {ex.Message}\n{ex.StackTrace}");
            }
            return ProjectBeginDates;
        }

        private Dictionary<string, DateTime> FetchProjectEndDates()
        {
            var ProjectEndDates = new Dictionary<string, DateTime>();

            string query = @" SELECT Project_name, Project_end
            FROM Projects";
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var name = reader["Project_name"].ToString();
                                var End = reader.GetDateTime(reader.GetOrdinal("Project_end"));
                                ProjectEndDates[name] = End;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching work times: {ex.Message}\n{ex.StackTrace}");
            }
            return ProjectEndDates;
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
        private async void UpdateDateRangeLabel()
        {
            // Update the label text with the selected date range
            DateSelecter.Text = startDate.ToString("d/MMM/yy") + " - " + endDate.ToString("d/MMM/yy");
            await PopulateDynamicCellsAsync(startDate, endDate, SelectedProjectName);
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
            SelectedProjectName = comboBox1.Text;
            var CheckForBegining = FetchProjectStartDates();
            var CheckForEnding = FetchProjectEndDates();
            if (CheckForBegining.TryGetValue(SelectedProjectName, out DateTime ProjectBegining))
            {
                if (ProjectBegining.Date >= startDate.Date)
                {
                    startDate = new DateTime(ProjectBegining.Year, ProjectBegining.Month, 1);
                    endDate = new DateTime(ProjectBegining.Year, (ProjectBegining.Month + 1), 1).AddDays(-1);
                    buttonPrevious.Visible = false;
                    buttonPrevious.Enabled = false;
                }
            }
            if (CheckForEnding.TryGetValue(SelectedProjectName, out DateTime ProjectEnding))
            {
                if (ProjectEnding.Date <= endDate.Date)
                {
                    startDate = new DateTime(ProjectEnding.Year, ProjectEnding.Month, 1);
                    endDate = new DateTime(ProjectEnding.Year, (ProjectEnding.Month + 1), 1).AddDays(-1);
                    buttonNext.Visible = false;
                    buttonNext.Enabled = false;
                }
            }
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
                Margin = new Padding(5, 0, 5, 0),
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
            if (SelectedProjectName == "")
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
                DayLayoutPanel.Enabled = true;
                DayLayoutPanel.Visible = true;
                radioButton2.Checked = false;
            }
            else
            {
                DayLayoutPanel.Enabled = false;
                DayLayoutPanel.Visible = false;
            }
            UpdateDateRangeLabel();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                MonthLayoutPanel.Enabled = true;
                MonthLayoutPanel.Visible = true;
                radioButton1.Checked = false;
            }
            else
            {
                MonthLayoutPanel.Enabled = false;
                MonthLayoutPanel.Visible = false;
            }
            UpdateDateRangeLabel();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            var CheckForBegining = FetchProjectStartDates();
            var CheckForEnding = FetchProjectEndDates();
            // Set the start and end dates one week earlier
            startDate = startDate.AddMonths(-1);
            // Calculate the end date for the new month
            endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
            if (CheckForBegining.TryGetValue(SelectedProjectName, out DateTime ProjectBegining))
            {
                if (ProjectBegining.Date >= startDate.Date)
                {
                    buttonPrevious.Enabled = false;
                    buttonPrevious.Visible = false;
                }                
            }
            if (CheckForEnding.TryGetValue(SelectedProjectName, out DateTime ProjectEnding))
            {
                if (ProjectEnding.Date > endDate.Date)
                {
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                }
            }
            UpdateDateRangeLabel();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var CheckForBegining = FetchProjectStartDates();
            var CheckForEnding = FetchProjectEndDates();
            // Set the start and end dates one week later
            startDate = startDate.AddMonths(1);
            // Calculate the end date for the new month
            endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
            if (CheckForBegining.TryGetValue(SelectedProjectName, out DateTime ProjectBegining))
            {
                if (ProjectBegining.Date < startDate.Date)
                {
                    buttonPrevious.Enabled = true;
                    buttonPrevious.Visible = true;
                }
            }
            if (CheckForEnding.TryGetValue(SelectedProjectName, out DateTime ProjectEnding))
            {
                if (ProjectEnding.Date <= endDate.Date)
                {
                    buttonNext.Enabled = false;
                    buttonNext.Visible = false;
                }
            }
            UpdateDateRangeLabel();
        }

        private void TodayBtn_Click(object sender, EventArgs e)
        {
            startDate = new DateTime(Today_date.Year, Today_date.Month, 1); // Default start date
            endDate = new DateTime(Today_date.Year, Today_date.Month + 1, 1).AddDays(-1);
            UpdateDateRangeLabel();
        }

        private void HideAllInMainWindow()
        {
            foreach (Control control in MainWindow.Controls)
            {
                control.Enabled = false;
                control.Visible = false;
            }
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            HideAllInMainWindow();
            CalendarPanel.Visible = true;
            CalendarPanel.Enabled = true;
        }

        private void TasksButton_Click(object sender, EventArgs e)
        {
            HideAllInMainWindow();
            TasksPanel.Visible = true;
            TasksPanel.Enabled = true;
        }

        private void NotesButton_Click(object sender, EventArgs e)
        {
            HideAllInMainWindow();
            NotesPanel.Visible = true;
            NotesPanel.Enabled = true;
        }
    }
}
