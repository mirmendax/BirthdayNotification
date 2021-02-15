using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirthdayNotification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = ParserExcelData.ParseExcel(0);
            if (list != null)
            {
                var str = new StringBuilder();
                foreach (var item in list)
                {
                    str.Append($"{item.name} \t\t[{DateTime.Parse(item.day).ToString("d")}]\n");
                }
                MessageBox.Show(str.ToString());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var temp = new List<BirthdayItem>();
            var list = ParserExcelData.ParseExcel(2);
            foreach (var item in list)
            {
                var t = new BirthdayItem(item.name, DateTime.Parse(item.day));
                temp.Add(t);
            }
            var splitTimeBeginEvent = Other.GetTimeReminded(mtbBeginTime.Text);
            var duration = (int)nudDuration.Value;
            var timeReminded = (int)nudReminder.Value;
            //MessageBox.Show($"{splitTimeBeginEvent.hour}-{splitTimeBeginEvent.min}");
            Calendar.AddItemCalendar(temp, splitTimeBeginEvent.hour, splitTimeBeginEvent.min, duration, timeReminded);
        }
    }
}
