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
        Controller _controller;
        public Form1()
        {
            InitializeComponent();
            _controller = new Controller();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _controller.sayHello();
            Console.WriteLine("Hello world from interface ");
        }
    }
}
