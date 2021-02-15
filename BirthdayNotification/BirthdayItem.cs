using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayNotification
{
    public class BirthdayItem
    {
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public BirthdayItem(string name, DateTime birthday)
        {
            if (!string.IsNullOrWhiteSpace(name) || birthday != DateTime.MinValue)
            {
                Name = name;
                Birthday = new DateTime(birthday.Year, birthday.Month, birthday.Day);
            }
            else
                throw new ArgumentException("Error: Не правильные аргументы функции.");

        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}" +
                   $"{nameof(Birthday)}: {Birthday.ToString("d")}";
        }
    }
}
