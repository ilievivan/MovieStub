using MovieStub.Domain.DomainModels;
using MovieStub.Domain.DTO;
using MovieStub.Domain.Relations;
using MovieStub.Repository.Interface;
using MovieStub.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieStub.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<MovieInOrder> _MovieInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<MovieInOrder> MovieInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _MovieInOrderRepository = MovieInOrderRepository;
        }


        public bool deleteMovieFromSoppingCart(string userId, Guid MovieId)
        {
            if(!string.IsNullOrEmpty(userId) && MovieId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.MovieInShoppingCarts.Where(z => z.MovieId.Equals(MovieId)).FirstOrDefault();

                userShoppingCart.MovieInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                var allMovies = userCard.MovieInShoppingCarts.ToList();

                var allMoviePrices = allMovies.Select(z => new
                {
                    MoviePrice = z.CurrnetMovie.MoviePrice,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allMoviePrices)
                {
                    totalPrice += item.Quantity * item.MoviePrice;
                }

                var reuslt = new ShoppingCartDto
                {
                    Movies = allMovies,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDto();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<MovieInOrder> MovieInOrders = new List<MovieInOrder>();

                var result = userCard.MovieInShoppingCarts.Select(z => new MovieInOrder
                {
                    Id = Guid.NewGuid(),
                    MovieId = z.CurrnetMovie.Id,
                    Movie = z.CurrnetMovie,
                    OrderId = order.Id,
                    Order = order
                }).ToList();

                MovieInOrders.AddRange(result);

                foreach (var item in MovieInOrders)
                {
                    this._MovieInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.MovieInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }

            return false;
        }
    }
}
