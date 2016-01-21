using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Specs.TestData.Commands
{
    public class ScheduleFlight
    {
        public string FlightNumber { get; set; }
        public string FromAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
    }
    public class BookSeat
    {
        public string FlightNumber { get; set; }
        public string SeatNumber { get; set; }
    }
    public class CancelSeat
    {
        public string FlightNumber { get; set; }
        public string SeatNumber { get; set; }
    }
}
