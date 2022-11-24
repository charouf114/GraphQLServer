using Movies.service.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        public UserRoles Roles { get; set; }

        [NotMapped]
        public string Username => FirstName + " " + LastName;

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [NotMapped]
        public string[] RolesList => getRolesFromEnum(Roles);

        public ICollection<Drink> Drinks { get; set; }

        public string[] getRolesFromEnum(UserRoles value)
        {
            var b = new List<string>();
            var values = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().ToList();

            foreach (var v in values)
            {
                if (value.HasFlag(v))
                {
                    b.Add(v.ToString());
                }
            }
            return b.ToArray();
        }

    }

    [Flags]
    public enum UserRoles
    {
        Admin = 0x00,
        User = 0x01,
        CanPay = 0x02,
        Moderator = 0x04,
    }
}
