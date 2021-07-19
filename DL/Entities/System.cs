using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class System
    {
        public System()
        {
            Games = new HashSet<Game>();
            Products = new HashSet<Product>();
        }

        public string Name { get; set; }
        public decimal? Msrp { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
