using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.service.Common.Models
{
    public class Review
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public int ExternalId { get; set; }

        public Guid MovieId { get; set; }
        public string Reviewer { get; set; }
        
        public int Stars { get; set; }

        public Review()
        { }
    }
}