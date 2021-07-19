using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
            LineItems = new HashSet<LineItem>();
        }

        public string ProductId { get; set; }
        public string GameId { get; set; }
        public string SystemId { get; set; }

        public virtual Game Game { get; set; }
        public virtual System System { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
