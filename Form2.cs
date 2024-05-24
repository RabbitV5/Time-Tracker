using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Tracker
{
    public partial class Form2 : Form
    {
        OdbcConnection connection = new OdbcConnection("Driver = {MICROSOFT ACCESS DRIVER (*.mdb, *.accdb)}; Dsn=Enterprise_DB;");

        //
        //Load data
        static string ProjectName = Form1.SelectedProjectName;
        static string CurrentUser = Environment.UserName;

        //
        //Crucial strings for queries
        static string LoadSetConditionsForRequest = Form1.SetConditionsForRequest;
        static string LoadInsertODBCData = Form1.InsertODBCData;
        static string LoadUpdateODBCData = Form1.UpdateODBCData;
        static string LoadGetODBCData = Form1.GetODBCData;
        

        public Form2()
        {
            InitializeComponent();
            DateLabel_TTW.Text = Form1.SelectedCellDate.ToString("dd.MM.yyyy");
            label2.Text = Form1.CurrentUser.ToString();
            label3.Text = Form1.SelectedProjectName.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadCometsPanel();
            LoadDateRelatedComments();
        }

        private void LoadDateRelatedComments()
        {
            string SelectedColumns = "*";
            string SelectedTable = "Managers_comments";
            string Conditions = " WHERE Related_user = '" + CurrentUser + "' AND Related_date = '" + DateLabel_TTW.Text + "' " ;
            using (connection)
            {
                try
                {
                    connection.Open();
                    //string GetODBCData = $"SELECT @SelectedColmnName FROM @SelectedTableName @SetConditionsForRequest";
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
                }
                catch (Exception ex)
                {
                    // Handle exception
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void LoadCometsPanel()
        {
            DateTime date = Form1.SelectedCellDate;
            var id = "";
            FlowLayoutPanel ComentBoxPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                AutoScroll = false,
                BackColor = Color.SandyBrown,
                BorderStyle = BorderStyle.FixedSingle,
                FlowDirection = FlowDirection.TopDown,
                Margin = new Padding(10),
                Padding = new Padding(10),
                Tag = new { CellDate = date, CommentId = id },
                Width = 300,
                WrapContents = true,
            };
            Label lblM_Name = new Label
            {
                Text = "Manager name",//date.ToString("dd"),
                Anchor = AnchorStyles.None,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                
                AutoSize = true,
                Margin = new Padding(5, 0, 5, 0),
                Tag = new { CellDate = date },
            };
            ComentBoxPanel.Controls.Add(lblM_Name);
            Label lblCommentTheme = new Label
            {
                Text = "Theme",//date.ToString("dd"),
                Anchor = AnchorStyles.None,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12),
                AutoSize = true,
                Margin = new Padding(5, 0, 5, 0),
                Tag = new { CellDate = date }
            };
            ComentBoxPanel.Controls.Add(lblCommentTheme);
            Label lblCommentContext = new Label
            {
                Text = "Drag a Button control from the Toolbox into the FlowLayoutPanel control and point to the space between two Button controls. Note that an insertion bar is drawn, indicating where the Button will be placed when it is dropped into the FlowLayoutPanel control. Before you drop the new Button control into the FlowLayoutPanel control, move the mouse pointer around to observe how the insertion bar moves.",
                Font = new Font("Microsoft Sans Serif", 12),
                AutoSize = true,
                Margin = new Padding(5, 2, 5, 2),
            };
            ComentBoxPanel.Controls.Add(lblCommentContext);
            TextBox AnswerBox = new TextBox
            {
                Margin = new Padding(25 , 5, 0, 5),
                Font = new Font("Microsoft Sans Serif", 12),
                Width = ComentBoxPanel.Width - 30,
                Height = 28,
            };
            ComentBoxPanel.Controls.Add(AnswerBox);
            Button SendAnswer = new Button
            {
                Visible = false,
                Margin = new Padding(15, 5, 0, 5),
                Text = "Send",
                Height = 30,
                Font = new Font("Microsoft Sans Serif", 12)
            };
            ComentBoxPanel.Controls.Add(SendAnswer);

            AnswerBox.TextChanged += (sender, e) =>
            {
                if (AnswerBox.Text == "")
                    SendAnswer.Visible = false;

                else
                    SendAnswer.Visible = true;
            };

                 

            TimeTrackingComentsWindow.Controls.Add(ComentBoxPanel);
        }

        private void TimeBoxEnter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {                
                e.Handled = true;
            }
        }
    }
}
