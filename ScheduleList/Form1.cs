using Models;
using ScheduleListController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ScheduleList
{
    public partial class Form1 : Form
    {

        int selectedDates = 0;
        DateTime startDate;
        DateTime endDate;
        ContextMenuStrip menu;
        bool addButtonSelected = false;
        Controller controller = null;
        string selectedDate = "";
        Task mainSelectedTask;
        public Form1()
        {
            InitializeComponent();
            controller = new Controller();
            panel7.Visible = true;
            panel6.Visible = false;

            DateTime dt = DateTime.Now;
            label8.Text = dt.ToString("MMMM");
            label8.Text += ", ";
            label8.Text += dt.Year;

        }
        /// <summary>
        ///  Ciobanu Denis Marian
        ///  On form load: configure view (dates on buttons, config dataGridView and dateTimePicker appearances)
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayDateForButtons();
            selectedDates = 0;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "hh:mm:ss";
            dateTimePicker1.ShowUpDown = true;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            
            dataGridView1.MouseClick += new MouseEventHandler(dataGridView_MouseClick);
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Click on task to view options menu having following items: update, delete, more
        /// </summary>
        void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            menu = new System.Windows.Forms.ContextMenuStrip();
            if (e.Button == MouseButtons.Left)
            {

                int mousePosition = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if(mousePosition >= 0)
                {
                    menu.Items.Add("Update").Name = "Update";
                    menu.Items.Add("Delete").Name = "Delete";
                    menu.Items.Add("More").Name = "More";

                }
                menu.Show(dataGridView1, new Point(e.X, e.Y));
                menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
            }
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Let user choose one of the following options: update task, delete task, find out more about task
        /// </summary>
        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string title = "";
            string time = "";

            List<Task> tasks;
            time = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            title = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            
            tasks = controller.GetTasksForAGivenDate(selectedDate);
            Task selectedTask = new Task();
            foreach (Task t in tasks)
            {
                if (t.Time == time && t.Title == title)
                {
                    selectedTask = t;
                    mainSelectedTask = t;
                }
            }

            switch (e.ClickedItem.Name.ToString())
            {
                case "Update":
                    menu.Visible = false;
                    dataGridView1.CurrentCell.Style.BackColor = Color.White;
                    dataGridView1.CurrentCell.Style.ForeColor = Color.Blue;
                    
                    break;
                case "Delete":
                    menu.Visible = false;
                    if (selectedTask != null)
                    {
                        controller.DeleteTask(selectedTask);
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    break;
                case "More":
                    menu.Visible = false;
                    string description = selectedTask.Description;

                    MessageBox.Show("Description:\n\n" + description);
                    break;
            }
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  when click button show all task scheduled for that day
        /// </summary>
        private void btn_click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel6.Visible = false;

            Button buttonSender = (Button)sender;


            DateTime dt = DateTime.Now;
            string year = dt.Year.ToString();
            string month;
            if (dt.Month < 10)
            {
                 month = "0" + dt.Month.ToString();
            } else
            {
                 month = dt.Month.ToString();
            }

            string day = buttonSender.Text[4] + "" + buttonSender.Text[5];
            string date = day + "." + month + "." + year;
            selectedDate = date;
            List<Task> tasks = controller.GetTasksForAGivenDate(selectedDate);

            //List<Task> tasks = getTasks(date);

            DataTable table = new DataTable();

            table.Columns.Add("Time", typeof(String));
            table.Columns.Add("Title", typeof(String));
            table.Columns.Add("Subtitle", typeof(String));
            table.Columns.Add("Status");
            table.Columns.Add("Priority");

            foreach(Task t in tasks) {
                string priority = "";
                if (t.Priority == 0)
                {
                    priority = "Low";
                }
                else if(t.Priority == 1)
                {
                    priority = "Medium";
                }
                else
                {
                    priority = "High";
                }

                table.Rows.Add(new object[] { t.Time, t.Title, t.Subtitle, t.Status, priority });
            }

            dataGridView1.DataSource = table;

        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  clean controls when refreshing panel
        /// </summary>
        private void CleanControls()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            selectedDates = 0;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Click button to show Add Task Panel
        /// </summary>
        private void button8_Click(object sender, EventArgs e)
        {
            CleanControls();

            if (addButtonSelected == false)
            {
                addButtonSelected = true;
                panel6.Visible = true;
                panel7.Visible = false;

            } else
            {
                addButtonSelected = false;
                panel6.Visible = false;
                panel7.Visible = true;
            }

            // controller.SayHello();
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Click save for adding new task; apply validations
        /// </summary>
        private void button10_Click(object sender, EventArgs e)
        {
            if (TitleValidation())
            {
                if (SelectdDatesValidator())
                {
                    if (IntervalValidator())
                    {
                        if (RadioButtonsValidation())
                        {
                            panel7.Visible = true;
                            panel6.Visible = false;

                            Task task = new Task();

                            task.Title = textBox1.Text;
                            task.Subtitle = textBox2.Text;

                            string time = dateTimePicker1.Value.ToString("hh:mm:ss");
                            task.Time = time;
                            task.Description = richTextBox1.Text;
                            task.Status = "new";
                            if (radioButton1.Checked == true)
                                task.Priority = 3;
                            else if (radioButton2.Checked == true)
                                task.Priority = 2;
                            else
                                task.Priority = 1;

                           controller.CreateNewTask(task);

                           MessageBox.Show("Succesfully added new task!");
                           panel7.Visible = true;
                           panel6.Visible = false;

                        }
                        else
                        {
                            MessageBox.Show("Please select priority level!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select End Date After Start Date!");
                    }
                }
                else
                {
                    MessageBox.Show("Please select start date and end date!");
                }
            }
            else
            {
                MessageBox.Show("Please select title!");
            }
        }

        private bool RadioButtonsValidation()
        {
            if (this.radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                return false;
            }
            return true;
        }

        private bool TitleValidation()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                return false;
            }
            return true;
        }

        private bool SelectdDatesValidator()
        {
            return selectedDates < 2 ? false : true;
        }

        private bool IntervalValidator()
        {
            return endDate >= startDate ? true : false;
        }

        private void DisplayDateForButtons()
        {
            DateTime dt = DateTime.Now;
            this.button2.Text = dt.ToString("ddd");
            this.button2.Text += "\n";
            this.button2.Text += dt.Day.ToString("00");

            dt = dt.AddDays(1);
            this.button3.Text = dt.ToString("ddd");
            this.button3.Text += "\n";
            this.button3.Text += dt.Day.ToString("00");

            dt = dt.AddDays(1);
            this.button5.Text = dt.ToString("ddd");
            this.button5.Text += "\n";
            this.button5.Text += dt.Day.ToString("00");

            dt = dt.AddDays(1);
            this.button4.Text = dt.ToString("ddd");
            this.button4.Text += "\n";
            this.button4.Text += dt.Day.ToString("00");

            dt = dt.AddDays(1);
            this.button12.Text = dt.ToString("ddd");
            this.button12.Text += "\n";
            this.button12.Text += dt.Day.ToString("00");

            dt = dt.AddDays(1);
            this.button6.Text = dt.ToString("ddd");
            this.button6.Text += "\n";
            this.button6.Text += dt.Day.ToString("00");

            dt = dt.AddDays(1);
            this.button7.Text = dt.ToString("ddd");
            this.button7.Text += "\n";
            this.button7.Text += dt.Day.ToString("00");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = false;
            }
            monthCalendar1.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = false;
            }
            monthCalendar2.Enabled = true;
        }

        private void End_Date_Selected(object sender, DateRangeEventArgs e)
        {
            monthCalendar2.Visible = false;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = true;
            }
            endDate = monthCalendar2.SelectionRange.Start;
            var selectedDate = endDate.ToString("dd MMM yyyy");
            selectedDates++;
        }

        private void Start_Date_Selected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = true;
            }
            startDate = monthCalendar1.SelectionRange.Start;
            var selectedDate = startDate.ToString("dd MMM yyyy");
            selectedDates++;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (statsBlock.Visible)
                statsBlock.Visible = false;
            else
                statsBlock.Visible = true;
        }

        private void todayStats_Click(object sender, EventArgs e)
        {
            todayStats.FlatAppearance.BorderSize = 3;
            weekStats.FlatAppearance.BorderSize = 0;
            monthStats.FlatAppearance.BorderSize = 0;
            
            overallProgressBar.SubscriptText = "10";
            overallProgressBar.Value = 10;
            overallProgressBar.Update();

            efficiencyProgressBar.SubscriptText = "63";
            efficiencyProgressBar.Value = 63;
            efficiencyProgressBar.Update();


            remainingProgressBar.SubscriptText = "90";
            remainingProgressBar.Value = 90;
            remainingProgressBar.Update();

            finishedProgressBar.SubscriptText = "10";
            finishedProgressBar.Value = 10;
            finishedProgressBar.Update();
        }

        private void weekStats_Click(object sender, EventArgs e)
        {
            todayStats.FlatAppearance.BorderSize = 0;
            weekStats.FlatAppearance.BorderSize = 3;
            monthStats.FlatAppearance.BorderSize = 0;

            overallProgressBar.SubscriptText = "25";
            overallProgressBar.Value = 25;
            overallProgressBar.Update();

            efficiencyProgressBar.SubscriptText = "36";
            efficiencyProgressBar.Value = 36;
            efficiencyProgressBar.Update();

            remainingProgressBar.SubscriptText = "50";
            remainingProgressBar.Value = 50;
            remainingProgressBar.Update();

            finishedProgressBar.SubscriptText = "50";
            finishedProgressBar.Value = 50;
            finishedProgressBar.Update();
        }

        private void monthStats_Click(object sender, EventArgs e)
        {
            todayStats.FlatAppearance.BorderSize = 0;
            weekStats.FlatAppearance.BorderSize = 0;
            monthStats.FlatAppearance.BorderSize = 3;

            overallProgressBar.SubscriptText = "100";
            overallProgressBar.Value = 100;
            overallProgressBar.Update();

            overallProgressBar.SubscriptText = "100";
            overallProgressBar.Value = 100;
            overallProgressBar.Update();

            efficiencyProgressBar.SubscriptText = "100";
            efficiencyProgressBar.Value = 100;
            efficiencyProgressBar.Update();

            finishedProgressBar.SubscriptText = "0";
            finishedProgressBar.Value = 0;
            finishedProgressBar.Update();
        }

        public List<Task> getTasks (string date)
        {

            //date ar trebui folosit pe a aduce sub forma de lista toate taskurile din date-ul specificat

            List<Task> lista = new List<Task>();
            Task t1 = new Task();
            t1.Title = "Primul";
            t1.Subtitle = "Subtitlu1";
            t1.Priority = 0;
            t1.Status = "New";

            Task t2 = new Task();
            t2.Title = "Al doilea";
            t2.Subtitle = "Subtitlu2";
            t2.Priority = 1;
            t2.Status = "New";

            Task t3 = new Task();
            t3.Title = "Al treilea";
            t3.Subtitle = "Subtitlu3";
            t3.Priority = 2;
            t3.Status = "New";

            Task t4 = new Task();
            t4.Title = "Al patrulea";
            t4.Subtitle = "Subtitlu4";
            t4.Priority = 1;
            t4.Status = "New";

            lista.Add(t1);
            lista.Add(t2);
            lista.Add(t3);
            lista.Add(t4);

            return lista;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void cellValueChangedEvent(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell.Style.BackColor = Color.MediumPurple;
            dataGridView1.CurrentCell.Style.ForeColor = Color.Snow;

            string time = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string title = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            string subtitle = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string status = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            string strPriority = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            int priority = 0;

            if (strPriority.CompareTo("Low") == 0)
            {
                priority = 1;
            }
            else if (strPriority.CompareTo("Medium") == 0)
            {
                priority = 2;
            }
            else if (strPriority.CompareTo("High") == 0)
            {
                priority = 3;
            }

            if (mainSelectedTask != null)
            {
                controller.UpdateTaskFowView(mainSelectedTask, time, title, subtitle, status, priority);
                MessageBox.Show("Task sucessfully updated!");
            }
        }
    }
}
