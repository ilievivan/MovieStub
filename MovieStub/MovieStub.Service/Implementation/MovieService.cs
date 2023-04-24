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
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _MovieRepository;
        private readonly IRepository<MovieInShoppingCart> _MovieInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public MovieService(IRepository<Movie> MovieRepository, IRepository<MovieInShoppingCart> MovieInShoppingCartRepository, IUserRepository userRepository)
        {
            _MovieRepository = MovieRepository;
            _userRepository = userRepository;
            _MovieInShoppingCartRepository = MovieInShoppingCartRepository;
        }


        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedMovieId != null && userShoppingCard != null)
            {
                var Movie = this.GetDetailsForMovie(item.SelectedMovieId);

                if (Movie != null)
                {
                    MovieInShoppingCart itemToAdd = new MovieInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        CurrnetMovie = Movie,
                        MovieId = Movie.Id,
                        UserCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._MovieInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewMovie(Movie p)
        {
            this._MovieRepository.Insert(p);
        }

        public void DeleteMovie(Guid id)
        {
            var Movie = this.GetDetailsForMovie(id);
            this._MovieRepository.Delete(Movie);
        }

        public List<Movie> GetAllMovies()
        {
            return this._MovieRepository.GetAll().ToList();
        }

        public Movie GetDetailsForMovie(Guid? id)
        {
            return this._MovieRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var Movie = this.GetDetailsForMovie(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedMovie = Movie,
                SelectedMovieId = Movie.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingMovie(Movie p)
        {
            this._MovieRepository.Update(p);
        }
    }
}
