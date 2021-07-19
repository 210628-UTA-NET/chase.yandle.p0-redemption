using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class LineItem
    {
        public string LineItemId { get; set; }
        public string OrderId { get; set; }
        public string Product { get; set; }
        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product ProductNavigation { get; set; }
    }
}
