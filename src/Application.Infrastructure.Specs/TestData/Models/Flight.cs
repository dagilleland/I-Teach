using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
namespace Application.Infrastructure.Specs.TestData.Models
{
    class Flight
    {
        public FlightNumber FlightNumber { get; internal set; }
        public Location Destination { get; internal set; }
        public Location Origin { get; internal set; }

        public Flight(FlightNumber flightNumber, Location destination, Location origin)
        {
            Contract.Requires(flightNumber != null, "flightNumber is null.");
            Contract.Requires(destination != null, "destination is null.");
            Contract.Requires(origin != null, "origin is null.");
            FlightNumber = flightNumber;
            Destination = destination;
            Origin = origin;
        }
        private Flight() { } // Private for EF 5+
    }

    public class FlightNumber
    {
        public string Value { get; internal set; }

        public FlightNumber(string value)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is null or empty.");
            Value = value;
        }
        private FlightNumber() { } // Private for EF 5+
    }

    public class Airport
    {
        public string Value { get; internal set; }

        public Airport(string value)
        {
            Contract.Requires(!string.IsNullOrEmpty(value), "value is null or empty.");
            Value = value;
        }
        private Airport() { } // Private for EF 5+
    }

    public class Location
    {
        public Airport Airport { get; internal set; }
        public DateTime Time { get; internal set; }
        public Location(Airport airport, DateTime time)
        {
            Contract.Requires(airport != null, "airport is null.");
            Contract.Requires(time != DateTime.MinValue, "time has not been set.");
            Airport = airport;
            Time = time;
        }
        private Location() { } // Private for EF 5+
    }
}
