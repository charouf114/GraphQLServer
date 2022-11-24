using Movies.Service.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.service.Common.Models
{
    public class Drink
    {
        [Required]
        [Key]
        public string BadgeId { get; set; }

        public DrinkType DrinkType { get; set; }

        public int SucreCount { get; set; }

        public bool HasMug { get; set; }

        public DateTime? CreationDate { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        [NotMapped]
        public ICharacter character { get; set; }

        public Drink(string BadgeId, DrinkType drinkType, int sucreCount, bool hasMug)
        {
            this.BadgeId = BadgeId;
            this.DrinkType = drinkType;
            this.SucreCount = sucreCount;
            this.HasMug = hasMug;
        }
        public Drink() { }
    }
}



