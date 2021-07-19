using System;
using System.Collections.Generic;
using DL;
using Models;

namespace BL
{
    public class OrdersBL
    {
        private OrdRepository _repo;
        public OrdersBL(OrdRepository p_repo)
        {
            _repo=p_repo;
        }

        public Orders AddOrders(Orders _oAdd)
        {
            return _repo.AddOrders(_oAdd);
        }

        public List<Orders> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }
        public List<Orders> FilterOrderByCust(string p_cust)
        {
            return _repo.FilterOrderByCust(p_cust);
        }
        public List<Orders> FilterOrdeByStore(string p_stores)
        {
            return _repo.FilterOrderByStore(p_stores);
        }
        public List<StockOrders> FilterOrderBoth(string p_cust, string p_store)
        {
            return _repo.FilterOrderBoth(p_cust,p_store);
        }
    }
    public class StockOrdersBL
    {
    private StockOrdRepository _repo;
        public StockOrdersBL(StockOrdRepository p_repo)
        {
            _repo=p_repo;
        }

        public StockOrders AddStockOrders(StockOrders _soAdd)
        {
            return _repo.AddStockOrders(_soAdd);
        }

        public List<StockOrders> GetAllStockOrders()
        {
            return _repo.GetAllStockOrders();
        }

        public List<StockOrders> FilterStockOrderDestination(string p_store)
        {
            return _repo.FilterStockOrderDestination(p_store);
        }
        public List<StockOrders> FilterStockOrderSource(string p_store)
        {
            return _repo.FilterStockOrderSource(p_store);
        }
        public List<StockOrders> FilterStockOrderBoth(string p_destination, string p_source)
        {
            return _repo.FilterStockOrderBoth(p_destination,p_source);
        }
    }
}