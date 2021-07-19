using System;
using System.Collections.Generic;
using DL;
using Models;

namespace BL
{
    public class LineItemBL
    {
        private LineRepository _repo;
        public LineItemBL(LineRepository p_repo)
        {
            _repo=p_repo;
        }
        public void AddGameLineItems(LineItems _cAdd)
        {
                _repo.AddGameLineItems(_cAdd);
        }
        public void AddSystemLineItems(LineItems _cAdd)
        {
                _repo.AddSystemLineItems(_cAdd);
        }
        public List<LineItems> GetGameLineItemsFromOrder(Orders p_order)
        {
            return _repo.GetGameLineItemsFromOrder(p_order);
        }
        public List<LineItems> GetSystemsLineItemsFromOrder(Orders p_order)
        {
            return _repo.GetSystemsLineItemsFromOrder(p_order);
        }
        public List<LineItems> GetGameLineItemsFromOrder(StockOrders p_order)
        {
            return _repo.GetGameLineItemsFromOrder(p_order);
        }
        public List<LineItems> GetSystemsLineItemsFromOrder(StockOrders p_order)
        {
            return _repo.GetSystemsLineItemsFromOrder(p_order);
        }
        public void AddLineItems(List<LineItems> _cAdd)
        {
            foreach (LineItems item in _cAdd)
            {
                if (item.liGame==null)
                {
                    AddSystemLineItems(item);
                } else
                {
                    AddGameLineItems(item);
                }

            }
        }
    }
}