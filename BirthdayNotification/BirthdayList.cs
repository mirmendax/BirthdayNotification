using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayNotification
{
    /// <summary>
    /// Not used
    /// </summary>
    public class BirthdayList
    {
        public List<BirthdayItem> birthdayItems = new List<BirthdayItem>();

       public void Add(BirthdayItem item)
        {
            if (!IsDouble(item))
            {
                birthdayItems.Add(item);
            }
        }

        public bool IsDouble(BirthdayItem item)
        {
            return birthdayItems.Any(i => i.Name == item.Name);
        }
    }
}
