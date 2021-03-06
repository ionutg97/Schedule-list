﻿/**************************************************************************
 *                                                                        *
 *  File:        Form1.cs                                                 *
 *  Copyright:   (c) 2019-2020                                            *
 *                Ciobanu Denis Marian                                    *
 *                Galan Ionut Andrei                                      *
 *  Description: Task Shedule - Windows Form Program                      *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Models;
using ScheduleListController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace ScheduleList
{
    public partial class Form1 : Form
    {

        int _selectedDates = 0;
        DateTime _startDate;
        DateTime _endDate;
        ContextMenuStrip _menu;
        bool _addButtonSelected = false;
        Controller _controller = null;
        string _selectedDate = "";
        Task _mainSelectedTask;
        public Form1()
        {
            InitializeComponent();
            _controller = new Controller();
            panel7.Visible = true;
            panel6.Visible = false;

            DateTime dt = DateTime.Now;
            label8.Text = dt.ToString("MMMM");
            label8.Text += ", ";
            label8.Text += dt.Year;

        }
        /// <summary>
        ///  Ciobanu Denis Marian
        ///  On form load: configure view (dates on buttons, config dataGridView and dateTimePicker appearances).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayDateForButtons();
            _selectedDates = 0;

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
        ///  Click on task to view options menu having following items: update, delete, more.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            _menu = new System.Windows.Forms.ContextMenuStrip();
            if (e.Button == MouseButtons.Left)
            {

                int mousePosition = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (mousePosition >= 0)
                {
                    _menu.Items.Add("Update").Name = "Update";
                    _menu.Items.Add("Delete").Name = "Delete";
                    _menu.Items.Add("More").Name = "More";

                }
                _menu.Show(dataGridView1, new Point(e.X, e.Y));
                _menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
            }
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Let user choose one of the following options: update task, delete task, find out more about task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string title = "";
            string time = "";

            List<Task> tasks;
            time = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            title = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            tasks = _controller.GetTasksForAGivenDate(_selectedDate);
            Task selectedTask = new Task();
            foreach (Task t in tasks)
            {
                if (t.Time == time && t.Title == title)
                {
                    selectedTask = t;
                    _mainSelectedTask = t;
                }
            }

            switch (e.ClickedItem.Name.ToString())
            {
                case "Update":
                    _menu.Visible = false;
                    dataGridView1.CurrentCell.Style.BackColor = Color.White;
                    dataGridView1.CurrentCell.Style.ForeColor = Color.Blue;

                    break;
                case "Delete":
                    _menu.Visible = false;
                    if (selectedTask != null)
                    {
                        _controller.DeleteTask(selectedTask);
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    break;
                case "More":
                    _menu.Visible = false;
                    string description = selectedTask.Description;

                    MessageBox.Show("Description:\n\n" + description);
                    break;
            }
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  when click button show all task scheduled for that day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel6.Visible = false;

            Button buttonSender = (Button)sender;

            DateTime dt = DateTime.Now;
            string year = dt.Year.ToString();
            string month;
            int monthN;
            string day = buttonSender.Text[4] + "" + buttonSender.Text[5];

            if (day.StartsWith("0") && !(button2.Text[4].ToString().Equals("0")))
            {
                monthN = dt.AddMonths(1).Month;
            }
            else
            {
                monthN = dt.Month;
            }

            if (dt.Month < 10)
            {
                month = "0" + monthN.ToString();
            }
            else
            {
                month = monthN.ToString();
            }

            string date = day + "." + month + "." + year;
            _selectedDate = date;
            List<Task> tasks = _controller.GetTasksForAGivenDate(_selectedDate);

            DataTable table = new DataTable();

            table.Columns.Add("Time", typeof(String));
            table.Columns.Add("Title", typeof(String));
            table.Columns.Add("Subtitle", typeof(String));
            table.Columns.Add("Status");
            table.Columns.Add("Priority");

            foreach (Task t in tasks)
            {
                string priority = "";
                if (t.Priority == 0)
                {
                    priority = "Low";
                }
                else if (t.Priority == 1)
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

        private void label8_Click(object sender, EventArgs e){}

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
            _selectedDates = 0;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Click button to show Add Task Panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            CleanControls();

            if (_addButtonSelected == false)
            {
                _addButtonSelected = true;
                panel6.Visible = true;
                panel7.Visible = false;

            }
            else
            {
                _addButtonSelected = false;
                panel6.Visible = false;
                panel7.Visible = true;
            }

            // controller.SayHello();
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Click save for adding new task; apply validations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                            _controller.CreateNewTask(task, _selectedDate);

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


        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Validate priority inputs.
        /// </summary>
        private bool RadioButtonsValidation()
        {
            if (this.radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Validate Title and subtitle inputs
        /// </summary>
        private bool TitleValidation()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Validate if dates have been both selected inputs.
        /// </summary>
        private bool SelectdDatesValidator()
        {
            return _selectedDates < 2 ? false : true;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Check if end date is selected after start date.
        /// </summary>
        private bool IntervalValidator()
        {
            return _endDate >= _startDate ? true : false;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Show dates on buttons.
        /// </summary>
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

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Display start_date calendar and disable the other controlls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = false;
            }
            monthCalendar1.Enabled = true;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Display end_date calendar and disable the other controlls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = false;
            }
            monthCalendar2.Enabled = true;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Event for when end_date is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void End_Date_Selected(object sender, DateRangeEventArgs e)
        {
            monthCalendar2.Visible = false;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = true;
            }
            _endDate = monthCalendar2.SelectionRange.Start;
            var selectedDate = _endDate.ToString("dd MMM yyyy");
            _selectedDates++;
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Event for when start_date is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Date_Selected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
            foreach (Control c in panel3.Controls)
            {
                c.Enabled = true;
            }
            _startDate = monthCalendar1.SelectionRange.Start;
            _selectedDate = _startDate.ToString("dd.MM.yyyy");
            _selectedDates++;

        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Hide or display statsBlock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            if (statsBlock.Visible)
                statsBlock.Visible = false;
            else
                statsBlock.Visible = true;
        }

        /// <summary>
        /// Galan Ionut Andrei
        /// Daily Statistics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todayStats_Click(object sender, EventArgs e)
        {
            DateTime baseDate = DateTime.Today;
            var today = baseDate;
            string date = Utils.ConvertDateTimeToString(today);

            todayStats.FlatAppearance.BorderSize = 3;
            weekStats.FlatAppearance.BorderSize = 0;
            customStats.FlatAppearance.BorderSize = 0;

            groupBoxInputDateStatistics.Visible = false;
            buttonViewStatistics.Visible = false;

            int resultEfficiency = (int)_controller.GetEffiencyOfTasksPercent(date, date);
            efficiencyProgressBar.SubscriptText = resultEfficiency.ToString();
            efficiencyProgressBar.Value = resultEfficiency;
            efficiencyProgressBar.Update();

            int resultFinished = (int)_controller.GetFinishedTasksPercent(date, date);
            int resultRemaining = (int)(100 - resultFinished);
            remainingProgressBar.SubscriptText = resultRemaining.ToString();
            remainingProgressBar.Value = resultRemaining;
            remainingProgressBar.Update();

            finishedProgressBar.SubscriptText = resultFinished.ToString();
            finishedProgressBar.Value = resultFinished;
            finishedProgressBar.Update();

            int resultOverall = (resultEfficiency  + resultFinished) / 2;
            overallProgressBar.SubscriptText = resultOverall.ToString();
            overallProgressBar.Value = resultOverall;
            overallProgressBar.Update();

        }
        /// <summary>
        /// Galan Ionut Andrei
        /// Weekly Statistics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weekStats_Click(object sender, EventArgs e)
        {
            DateTime baseDate = DateTime.Today;
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            string thisWeekStartString = Utils.ConvertDateTimeToString(thisWeekStart);
            string thisWeekEndString = Utils.ConvertDateTimeToString(thisWeekEnd);

            todayStats.FlatAppearance.BorderSize = 0;
            weekStats.FlatAppearance.BorderSize = 3;
            customStats.FlatAppearance.BorderSize = 0;

            groupBoxInputDateStatistics.Visible = false;
            buttonViewStatistics.Visible = false;

            int resultEfficiency = (int)_controller.GetEffiencyOfTasksPercent(thisWeekStartString, thisWeekEndString);
            efficiencyProgressBar.SubscriptText = resultEfficiency.ToString();
            efficiencyProgressBar.Value = resultEfficiency;
            efficiencyProgressBar.Update();

            int resultFinished = (int)_controller.GetFinishedTasksPercent(thisWeekStartString, thisWeekEndString);
            int resultRemaining = (int)(100 - resultFinished);
            remainingProgressBar.SubscriptText = resultRemaining.ToString();
            remainingProgressBar.Value = resultRemaining;
            remainingProgressBar.Update();

            finishedProgressBar.SubscriptText = resultFinished.ToString();
            finishedProgressBar.Value = resultFinished;
            finishedProgressBar.Update();

            int resultOverall = (resultEfficiency  + resultFinished) / 2;
            overallProgressBar.SubscriptText = resultOverall.ToString();
            overallProgressBar.Value = resultOverall;
            overallProgressBar.Update();

        }

        /// <summary>
        /// Galan Ionut Andrei
        /// Monthly Statistics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customStats_Click(object sender, EventArgs e)
        {
           
            todayStats.FlatAppearance.BorderSize = 0;
            weekStats.FlatAppearance.BorderSize = 0;
            customStats.FlatAppearance.BorderSize = 3;

            groupBoxInputDateStatistics.Visible = true;
            buttonViewStatistics.Visible = true;
            buttonViewStatistics.FlatAppearance.BorderColor = Color.Red;
            buttonViewStatistics.Enabled = true;

            efficiencyProgressBar.SubscriptText = "0";
            efficiencyProgressBar.Value = 0;

            finishedProgressBar.SubscriptText = "0";
            finishedProgressBar.Value = 0;

            remainingProgressBar.SubscriptText = "0";
            remainingProgressBar.Value = 0;

            overallProgressBar.SubscriptText = "0";
            overallProgressBar.Value = 0;

        }
        /// <summary>
        /// Galan Ionut Andrei
        /// Calculate statistics between two Date and verify if the start date is smaller then end date.
        /// If input date was validated the border button will be green in other case the border button is red.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonViewStatistics_Click(object sender, EventArgs e)
        {
            var thisCustomStart = dateTimePicker2.Value.Date;
            var thisCustomEnd = dateTimePicker3.Value.Date;


            if (Utils.validateDate(thisCustomStart, thisCustomEnd))
            {
                string thisCustomStartString = Utils.ConvertDateTimeToString(thisCustomStart);
                string thisCustomEndString = Utils.ConvertDateTimeToString(thisCustomEnd);

                buttonViewStatistics.FlatAppearance.BorderColor = Color.Green;

                int resultEfficiency = (int)_controller.GetEffiencyOfTasksPercent(thisCustomStartString, thisCustomEndString);
                efficiencyProgressBar.SubscriptText = resultEfficiency.ToString();
                efficiencyProgressBar.Value = resultEfficiency;
                efficiencyProgressBar.Update();

                int resultFinished = (int)_controller.GetFinishedTasksPercent(thisCustomStartString, thisCustomEndString);
                int resultRemaining = (int)(100 - resultFinished);
                remainingProgressBar.SubscriptText = resultRemaining.ToString();
                remainingProgressBar.Value = resultRemaining;
                remainingProgressBar.Update();

                finishedProgressBar.SubscriptText = resultFinished.ToString();
                finishedProgressBar.Value = resultFinished;
                finishedProgressBar.Update();

                int resultOverall = (resultEfficiency  + resultFinished) / 2;
                overallProgressBar.SubscriptText = resultOverall.ToString();
                overallProgressBar.Value = resultOverall;
                overallProgressBar.Update();
            }
        }


        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Clear selection for dataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Event triggered after user press Enter to update task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (_mainSelectedTask != null)
            {
                _controller.UpdateTaskFowView(_mainSelectedTask, time, title, subtitle, status, priority);
                MessageBox.Show("Task sucessfully updated!");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:/EMANUEL_DOCS/_IP/Schedule-list/Schedule List Help.chm");
        }
    }
}
