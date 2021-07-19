using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string DestinationId { get; set; }
        public string SourceId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Destination { get; set; }
        public virtual Location Source { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
