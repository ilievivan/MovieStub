using MovieStub.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStub.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Movie SelectedMovie { get; set; }
        public Guid SelectedMovieId { get; set; }
        public int Quantity { get; set; }
    }
}
