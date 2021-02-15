using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayNotification
{
    public static class Other
    {

        public static (int hour, int min) GetTimeReminded(string time)
        {
            var hour = 0;
            var min = 0;

            var split = time.Split(':');
            hour = int.Parse(split[0]);
            min = int.Parse(split[1]);

            return (hour, min);
        }
    }
}
