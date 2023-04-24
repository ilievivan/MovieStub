using MovieStub.Domain.Relations;
using System.Collections.Generic;

namespace MovieStub.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<MovieInShoppingCart> Movies { get; set; }

        public double TotalPrice { get; set; }
    }
}
