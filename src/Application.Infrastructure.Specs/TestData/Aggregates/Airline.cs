using Application.Infrastructure.Specs.TestData.Commands;
using Application.Infrastructure.Specs.TestData.Events;
using Application.Infrastructure.Specs.TestData.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Specs.TestData.Aggregates
{
    /// <summary>
    /// As a Domain Aggregate, this object is responsible to process commands to the domain and emit the events
    /// </summary>
    public class Airline
    {
        public IEnumerable ScheduleFlight(ScheduleFlight info)
        {
            FlightNumber flightNumber = new FlightNumber(info.FlightNumber);
            Location destination = new Location(new Airport(info.DestinationAirport),
                                                                info.ArrivalTime);
            Location origin = new Location(new Airport(info.FromAirport),
                                                                info.DepartureTime);
            Flight flight = new Flight(flightNumber,
                                       destination,
                                       origin);
            yield return new FlightScheduled
                {
                };
        }
        internal void Apply(FlightScheduled info)
        {
            FlightNumber flightNumber = new FlightNumber(info.FlightNumber);
            Location destination = new Location(new Airport(info.DestinationAirport), info.ArrivalTime);
            Location origin = new Location(new Airport(info.FromAirport), info.DepartureTime);
            Flight flight = new Flight(flightNumber, destination, origin);
        }
    }
}
