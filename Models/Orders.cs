using System;
using System.Collections.Generic;

namespace Models
{
    public class Orders
    {
        public List<string> oLineItemNumbers { get; set; }
        public string oCustomerNumber { get; set; }
        public string oStoreNumber { get; set; }
        public float oTotalPrice { get; set; }
        public string oNumber { get; set; }
        public DateTime oDateAndTime { get; set; }
        public List<LineItems> oLineItems = new List<LineItems>();
    }
    public class StockOrders
    {
        public string soSource { get; set; }
        public string soDestination { get; set; }
        public List<string> soLineItemNumbers { get; set; }
        public DateTime soRequestTime { get; set; }
        public string soNumber { get; set; }
        public List<LineItems> soLineItems = new List<LineItems>();
    }
}
