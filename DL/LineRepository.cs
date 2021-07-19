using System;
using Models;
using System.Collections.Generic;
using Entity = DL.Entities;
using System.Linq;

namespace DL
{
    public class LineRepository
    {
        private Entities.DemoDbContext _context;
        public LineRepository(Entities.DemoDbContext p_context)
        {  
            _context=p_context;       
        }
        public List<LineItems> GetGameLineItemsFromOrder(Orders p_order)
        {

            return _context.LineItems.Where(litem => litem.OrderId == p_order.oNumber && litem.Product.Substring(0,1)=="G")
            .Select(litem=>
                new LineItems{
                    liOrderNumber = litem.OrderId,
                    liLineNumber = litem.LineItemId,
                    liPrice = (float)_context.Games
                        .Where(game => game.GameId.Contains(litem.Product))
                        .Select(game => game.Msrp).ToList()[0],
                    liGame=litem.Product,
                    liQuantity = (int)litem.Quantity
                }
            ).ToList();
        }
        public List<LineItems> GetSystemsLineItemsFromOrder(Orders p_order)
        {

            return _context.LineItems.Where(litem => litem.OrderId == p_order.oNumber && litem.Product.Substring(0,1)=="S")
            .Select(litem=>
                new LineItems{
                    liOrderNumber = litem.OrderId,
                    liLineNumber = litem.LineItemId,
                    liPrice = (float)_context.Systems
                        .Where(syst =>  syst.Name.Contains(litem.Product))
                        .Select(syst => syst.Msrp).ToList()[0],
                    liSystem=litem.Product,
                    liQuantity = (int)litem.Quantity
                }
            ).ToList();
        }
        public List<LineItems> GetGameLineItemsFromOrder(StockOrders p_order)
        {

            return _context.LineItems.Where(litem => litem.OrderId == p_order.soNumber && litem.Product.Substring(0,1)=="G")
            .Select(litem=>
                new LineItems{
                    liOrderNumber = litem.OrderId,
                    liLineNumber = litem.LineItemId,
                    liPrice = (float)_context.Games
                        .Where(game => game.GameId.Contains(litem.Product))
                        .Select(game => game.Msrp).ToList()[0],
                    liGame=litem.Product,
                    liQuantity = (int)litem.Quantity
                }
            ).ToList();
        }
        public List<LineItems> GetSystemsLineItemsFromOrder(StockOrders p_order)
        {

            return _context.LineItems.Where(litem => litem.OrderId == p_order.soNumber && litem.Product.Substring(0,1)=="S")
            .Select(litem=>
                new LineItems{
                    liOrderNumber = litem.OrderId,
                    liLineNumber = litem.LineItemId,
                    liPrice = (float)_context.Systems
                        .Where(syst =>  syst.Name.Contains(litem.Product))
                        .Select(syst => syst.Msrp).ToList()[0],
                    liSystem=litem.Product,
                    liQuantity = (int)litem.Quantity
                }
            ).ToList();
        }
        public LineItems AddGameLineItems(LineItems p_lineitem)
        {
            _context.LineItems.Add(new Entity.LineItem{
                LineItemId = p_lineitem.liLineNumber,
                OrderId = p_lineitem.liOrderNumber,
                Product = "G"+p_lineitem.liGame+" "+p_lineitem.liSystem,
                Quantity = p_lineitem.liQuantity
            });
            _context.SaveChanges();
            return p_lineitem;
        }
        public LineItems AddSystemLineItems(LineItems p_lineitem)
        {
            _context.LineItems.Add(new Entity.LineItem{
                LineItemId = p_lineitem.liLineNumber,
                OrderId = p_lineitem.liOrderNumber,
                Product = "S"+p_lineitem.liSystem,
                Quantity = p_lineitem.liQuantity
            });
            _context.SaveChanges();
            return p_lineitem;
        }
    }
}