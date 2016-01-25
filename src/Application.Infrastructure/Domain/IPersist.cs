using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Domain
{
    public interface IPersist<TAggregateRoot, TKey>  // or IPersist<>
    {
        void Save(object data);
        TAggregateRoot Load(TKey key);
    }
}
