using MovieStub.Domain.Identity;
using MovieStub.Domain.Relations;
using System;
using System.Collections.Generic;

namespace MovieStub.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public MovieStubAppUser User { get; set; }

        public virtual ICollection<MovieInOrder> MovieInOrders { get; set; }
    }
}
