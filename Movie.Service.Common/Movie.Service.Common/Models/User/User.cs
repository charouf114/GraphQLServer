using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Movies.Service.Common.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string PhoneNumber { get; set; }

        public string Function { get; set; }

        public string Roles { get; set; }

        [NotMapped]
        public string Username => FirstName + " " + LastName;

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
