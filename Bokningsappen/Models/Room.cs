using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public int RoomSize { get; set; }
        public string? Name { get; set; }
        public bool Booked { get; set; }
        public DateTime? BookedDate { get; set; }
        public int popularity { get; set; }



    }
}
