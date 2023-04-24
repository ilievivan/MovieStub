using MovieStub.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteMovieFromSoppingCart(string userId, Guid MovieId);
        bool order(string userId);
    }
}
