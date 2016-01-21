using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Specs.TestData.Events
{
    public class FlightScheduled
    {
        public string FlightNumber { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FromAirport { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
