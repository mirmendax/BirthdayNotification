using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirthdayNotification
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            var splitTimeBeginEvent = Other.GetTimeReminded(mtbBeginTime.Text);
            if (splitTimeBeginEvent.hour > 23 || splitTimeBeginEvent.min > 59)
            {
                MessageBox.Show("Время вне диапозона (00:00-23:59)");
                button3.Enabled = true;
                return;
            }
            var duration = (int)nudDuration.Value;
            var timeReminded = (int)nudReminder.Value;
            var task = new Task(() =>
            {
                
                var list = new List<BirthdayItem>();
                for (int i = 0; i < 12; i++)
                {
                    var temp = ParserExcelData.ParseExcel(i);
                    prBar.Invoke(new Action(() =>
                    {
                        prBar.Maximum = temp.Count;
                        prBar.Minimum = 0;
                        prBar.Value = 0;
                    }));
                    foreach (var item in temp)
                    {
                        var birthdayItem = new BirthdayItem(item.name, DateTime.Parse(item.day));
                        list.Add(birthdayItem);
                        prBar.Invoke(new Action(() => { prBar.Value++; }));
                    }
                }

                
                if (Other.IsExecutOutlook())
                {
                    try
                    {
                        prBar.Invoke(new Action(() =>
                        {
                            prBar.Maximum = list.Count;
                            prBar.Value = 0;
                        }));
                        Calendar.AddProcProgressBar += delegate
                        {
                            prBar.Invoke(new Action(() => { prBar.Value++; }));
                        };
                        Calendar.AddItemCalendar(list, splitTimeBeginEvent.hour, splitTimeBeginEvent.min, duration, timeReminded);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show($"Error: {exp.Message} \n StackTrace: {exp.StackTrace}");
                    }
                }
                else MessageBox.Show("Outlook не запущен.");
                MessageBox.Show("Синхронизация успешно выполнена. \n " +
                    "Добавлено элементов: "+list.Count.ToString());
            });
            task.Start();
        }
    }
}
