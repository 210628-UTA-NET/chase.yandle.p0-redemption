using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Location
    {
        public Location()
        {
            Inventories = new HashSet<Inventory>();
            OrderDestinations = new HashSet<Order>();
            OrderSources = new HashSet<Order>();
        }

        public string LocationId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Order> OrderDestinations { get; set; }
        public virtual ICollection<Order> OrderSources { get; set; }
    }
}
