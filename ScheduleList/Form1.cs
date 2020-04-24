using ScheduleListController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleList
{
    public partial class Form1 : Form
    {

        int selectedDates = 0;
        DateTime startDate;
        DateTime endDate;

        Controller controller = null;
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
        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayDateForButtons();
            selectedDates = 0;
        }

        private void btn_click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel6.Visible = false;
        }

        private void CleanControls()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox1.Clear();
            richTextBox1.Clear();
            selectedDates = 0;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            CleanControls();
            panel6.Visible = true;
            panel7.Visible = false;

            controller.SayHello();
        }

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

                            // trimimte info catre controller
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
            if (string.IsNullOrWhiteSpace(textBox1.Text))
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
            this.button2.Text += dt.Day;

            dt = dt.AddDays(1);
            this.button3.Text = dt.ToString("ddd");
            this.button3.Text += "\n";
            this.button3.Text += dt.Day;

            dt = dt.AddDays(1);
            this.button5.Text = dt.ToString("ddd");
            this.button5.Text += "\n";
            this.button5.Text += dt.Day;

            dt = dt.AddDays(1);
            this.button4.Text = dt.ToString("ddd");
            this.button4.Text += "\n";
            this.button4.Text += dt.Day;

            dt = dt.AddDays(1);
            this.button12.Text = dt.ToString("ddd");
            this.button12.Text += "\n";
            this.button12.Text += dt.Day;

            dt = dt.AddDays(1);
            this.button6.Text = dt.ToString("ddd");
            this.button6.Text += "\n";
            this.button6.Text += dt.Day;

            dt = dt.AddDays(1);
            this.button7.Text = dt.ToString("ddd");
            this.button7.Text += "\n";
            this.button7.Text += dt.Day;
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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
