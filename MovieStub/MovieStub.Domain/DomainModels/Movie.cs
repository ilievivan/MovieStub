using MovieStub.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieStub.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }
        [Required]
        [Display(Name = "Movie Poster URL")]
        public string MovieImage { get; set; }
        [Required]
        [Display(Name = "Movie Description")]
        public string MovieDescription { get; set; }
        [Required]
        [Display(Name = "Price of 1 Ticket for Movie")]
        public double MoviePrice { get; set; }
        [Required]
        [Display(Name = "Movie Rating")]
        public double MovieRating { get; set; }


        public virtual ICollection<MovieInShoppingCart> MovieInShoppingCarts { get; set; }
        public virtual ICollection<MovieInOrder> MovieInOrders { get; set; }

    }
}
