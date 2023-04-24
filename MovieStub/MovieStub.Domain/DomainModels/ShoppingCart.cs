using MovieStub.Domain.Identity;
using MovieStub.Domain.Relations;
using System;
using System.Collections.Generic;


namespace MovieStub.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual MovieStubAppUser Owner { get; set; }

        public virtual ICollection<MovieInShoppingCart> MovieInShoppingCarts { get; set; }

    }
}
