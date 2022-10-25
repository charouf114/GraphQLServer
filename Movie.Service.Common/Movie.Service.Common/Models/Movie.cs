using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.service.Common.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Movie()
        { }
        
        public IList<Review> Reviews { get; set; }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}