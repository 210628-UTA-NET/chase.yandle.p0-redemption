using System;
using Models;
using System.Collections.Generic;
using Entity = DL.Entities;
using System.Linq;

namespace DL
{
    public class OrdRepository
    {
        private Entities.DemoDbContext _context;
        public OrdRepository(Entities.DemoDbContext p_context)
        {  
            _context=p_context;       
        }
        public List<Orders> GetAllOrders()
        {
            return _context.Orders
            .Where(ord => ord.OrderId.Substring(0,1)=="C")
            .Select(
                ord =>
                new Orders()
                {
                    oCustomerNumber = ord.CustomerId,
                    oLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    oNumber = ord.OrderId,
                    oDateAndTime = (DateTime)ord.OrderDate,
                    oTotalPrice = (float)ord.TotalPrice,
                    oStoreNumber = ord.SourceId
                }
            ).ToList();
        }
        public Orders AddOrders(Orders p_orders)
        {
            _context.Orders.Add(new Entity.Order{
                OrderId = p_orders.oNumber,
                CustomerId = p_orders.oCustomerNumber,
                SourceId = p_orders.oStoreNumber,
                OrderDate = p_orders.oDateAndTime,
                TotalPrice = (decimal?)p_orders.oTotalPrice
            });
            _context.SaveChanges();
            return p_orders;
        }
        public List<Orders> FilterOrderByStore(string p_store)
        {
            return _context.Orders
                .Where(ord => ord.SourceId == p_store)
                .Select(
                ord =>
                new Orders()
                {
                    oCustomerNumber = ord.CustomerId,
                    oLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    oNumber = ord.OrderId,
                    oDateAndTime = (DateTime)ord.OrderDate,
                    oTotalPrice = (float)ord.TotalPrice,
                    oStoreNumber = ord.SourceId
                }
            ).ToList();
        }
        public List<Orders> FilterOrderByCust(string p_cust)
        {
            return _context.Orders
                .Where(ord => ord.CustomerId == p_cust)
                .Select(
                ord =>
                new Orders()
                {
                    oCustomerNumber = ord.CustomerId,
                    oLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    oNumber = ord.OrderId,
                    oDateAndTime = (DateTime)ord.OrderDate,
                    oTotalPrice = (float)ord.TotalPrice,
                    oStoreNumber = ord.SourceId
                }
            ).ToList();
        }
        public List<StockOrders> FilterOrderBoth(string p_cust, string p_store)
        {
            return _context.Orders
                .Where(ord => ord.SourceId == p_store && ord.CustomerId == p_cust)
                .Select(
                ord =>
                new StockOrders()
                {
                    soNumber = ord.OrderId,
                    soLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    soDestination = ord.DestinationId,
                    soRequestTime = (DateTime)ord.OrderDate,
                    soSource = ord.SourceId
                }
            ).ToList();
        }
    }
        public class StockOrdRepository
    {
        private Entities.DemoDbContext _context;
        public StockOrdRepository(Entities.DemoDbContext p_context)
        {  
            _context=p_context;       
        }
        public List<StockOrders> GetAllStockOrders()
        {
            return _context.Orders
            .Where(ord => ord.OrderId.Substring(0,1)=="S")
            .Select(
                ord =>
                new StockOrders()
                {
                    soNumber = ord.OrderId,
                    soLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    soDestination = ord.DestinationId,
                    soRequestTime = (DateTime)ord.OrderDate,
                    soSource = ord.SourceId
                }
            ).ToList();
        }
        public StockOrders AddStockOrders(StockOrders p_stockorders)
        {
            _context.Orders.Add(new Entity.Order{
                OrderId = p_stockorders.soNumber,
                DestinationId = p_stockorders.soDestination,
                SourceId = p_stockorders.soSource,
                OrderDate = p_stockorders.soRequestTime
            });
            _context.SaveChanges();
            return p_stockorders;
        }
        public List<StockOrders> FilterStockOrderDestination(string p_store)
        {
            return _context.Orders
                .Where(ord => ord.DestinationId == p_store)
                .Select(
                ord =>
                new StockOrders()
                {
                    soNumber = ord.OrderId,
                    soLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    soDestination = ord.DestinationId,
                    soRequestTime = (DateTime)ord.OrderDate,
                    soSource = ord.SourceId
                }
            ).ToList();
        }
        public List<StockOrders> FilterStockOrderSource(string p_store)
        {
            return _context.Orders
                .Where(ord => ord.SourceId == p_store)
                .Select(
                ord =>
                new StockOrders()
                {
                    soNumber = ord.OrderId,
                    soLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    soDestination = ord.DestinationId,
                    soRequestTime = (DateTime)ord.OrderDate,
                    soSource = ord.SourceId
                }
            ).ToList();
        }
        public List<StockOrders> FilterStockOrderBoth(string p_destination, string p_source)
        {
            return _context.Orders
                .Where(ord => ord.SourceId == p_source && ord.DestinationId == p_destination)
                .Select(
                ord =>
                new StockOrders()
                {
                    soNumber = ord.OrderId,
                    soLineItemNumbers = _context.LineItems.Where(litem => litem.OrderId==ord.OrderId).Select(litem => litem.LineItemId).ToList(),
                    soDestination = ord.DestinationId,
                    soRequestTime = (DateTime)ord.OrderDate,
                    soSource = ord.SourceId
                }
            ).ToList();
        }
    }
}