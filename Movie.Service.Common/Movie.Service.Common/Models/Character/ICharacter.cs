using System;
using System.Collections.Generic;

namespace Movies.service.Common.Models
{
    public interface ICharacter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ICharacter> Friends { get; set; }
    }
}
