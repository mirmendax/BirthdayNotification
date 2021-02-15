using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayNotification
{
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
            foreach (var i in birthdayItems)
            {
                if (i.Name == item.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
