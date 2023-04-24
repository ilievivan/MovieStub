using MovieStub.Domain.DomainModels;
using System;

namespace MovieStub.Domain.Relations
{
    public class MovieInShoppingCart : BaseEntity
    {
        public Guid MovieId { get; set; }
        public virtual Movie CurrnetMovie { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}
