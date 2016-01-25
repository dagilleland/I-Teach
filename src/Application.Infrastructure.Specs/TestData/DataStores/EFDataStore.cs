using Application.Infrastructure.Domain;
using Application.Infrastructure.Specs.TestData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Specs.TestData.DataStores
{
    class EFDataStore : IPersist<Flight, string>
    {

        public void Save(object data)
        {
            // cast object as Flight
            // Do an update or a delete
            // "walk" the graph for deep save
            throw new NotImplementedException();
        }

        public Flight Load(string key)
        {
            // Load the data from the context using the supplied key
            throw new NotImplementedException();
        }
    }
}
