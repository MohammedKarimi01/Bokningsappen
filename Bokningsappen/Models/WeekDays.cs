using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class WeekDays
    {
        public int ID { get; set; }
        public string DayName { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
