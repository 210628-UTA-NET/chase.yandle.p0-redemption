using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class GamesOnSystem
    {
        public GamesOnSystem()
        {
            Products = new HashSet<Product>();
        }

        public string GameId { get; set; }
        public string GameName { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string OnSystem { get; set; }
        public decimal? Msrp { get; set; }

        public virtual Game Game { get; set; }
        public virtual System OnSystemNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
