using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Microsoft.Office.Interop.Outlook.Application;
using Exception = System.Exception;
using Microsoft.Office.Interop.Outlook;

namespace BirthdayNotification
{
    public static class Calendar
    {
        public static void AddItemCalendar(List<BirthdayItem> listItem, int hour_begin, int min_begin, int duration, int remind_min)
        {
            Application Application = null;
            Application = new Application();
            MAPIFolder primaryCalendar = Application.ActiveExplorer().Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

            foreach (var item in listItem)
            {

                 

                var personalCalendar = primaryCalendar.Folders["Birthday"];
                if (personalCalendar == null) return;

                //AppointmentItem newEvent = primaryCalendar.Items.Add(OlItemType.olAppointmentItem) as AppointmentItem;
                AppointmentItem newEvent = personalCalendar.Items.Add(OlItemType.olAppointmentItem) as AppointmentItem;
                if (newEvent != null)
                {
                    var date = new DateTime(DateTime.Now.Year, item.Birthday.Month, item.Birthday.Day, hour_begin, min_begin, 0);
                    newEvent.Start = date;
                    //newEvent.End = item.EndDateTime;
                    
                    var recur = newEvent.GetRecurrencePattern();
                    recur.RecurrenceType = OlRecurrenceType.olRecursYearly;
                    
                    recur.PatternStartDate = date;
                    recur.StartTime = date;
                    recur.Duration = duration;
                    recur.NoEndDate = true;
                    //recur.EndTime = DateTime.Now.AddMinutes(10);
                    newEvent.ReminderMinutesBeforeStart = remind_min;
                    newEvent.Subject =  $"[B] День рождения {item.Name}";
                    newEvent.Body = $"День рождения {item.Name}. Дата рождения {item.Birthday.ToString("d")}";
                    newEvent.Save();
                }
            }
            Application.ActiveExplorer().CurrentFolder.Display();
            
        }
    }
}
