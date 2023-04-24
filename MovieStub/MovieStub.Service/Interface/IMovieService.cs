using MovieStub.Domain.DomainModels;
using MovieStub.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Service.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetDetailsForMovie(Guid? id);
        void CreateNewMovie(Movie p);
        void UpdeteExistingMovie(Movie p);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteMovie(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
