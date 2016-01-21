using Application.Infrastructure.Domain;
using Application.Infrastructure.Specs.TestData.Aggregates;
using Application.Infrastructure.Specs.TestData.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Specs.TestData
{
    class DomainAggregate : Airline,
        IHandleCommand<ScheduleFlight>,
        IHandleCommand<BookSeat>,
        IHandleCommand<CancelSeat>
    {
    }
    /*
     * Flight
     *  ScheduleFlight(FlightNumber, Destination, DepartureTime, ArrivalTime, AvailableSeats)
     *  BookSeat(FlightNumber, SeatNumber)
     *  CancelSeat(FlightNumber, SeatNumber)
     */

}
