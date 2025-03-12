using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork22
{
    public class TicketInfo
    {
        public string TicketNumber {  get; set; }
        public string FilmTitle { get; set; }
        public DateTime SessionTime { get; set; }
        public string CinemaName { get; set; }
        public int HallNumber { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
    }
}
