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
        Controller controller = null;
        public Form1()
        {
            InitializeComponent();
            controller = new Controller();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.sayHello();
        }
    }
}
