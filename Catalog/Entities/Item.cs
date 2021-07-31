using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Entities
{
    public record Item
    {
        //our goal is that we need this immutable, so either "init;" or...
        //"private set;" while this would make it immutable, it is trickier to implement,
        //because now we will need to implement a constructor, and navigating our objects would be clunkier for our purpose
        public Guid Id { get; init; } // so Item item = new() { id = Guid.NewGuid() } .... works because "NEW()" makes immutable, WHILE "item.Id = Guid.NewGuid();" DOES NOT work
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
