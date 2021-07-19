using System;
using System.Collections.Generic;
using DL;
using Models;

namespace BL
{
    public class StoresBL
    {
        private StoreRepository _repo;
        public StoresBL(StoreRepository p_repo)
        {
            _repo=p_repo;
        }

        public Stores AddStore(Stores _stAdd)
        {
            return _repo.AddStore(_stAdd);
        }

        public List<Stores> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public List<Stores> FilterStore(Stores p_store)
        {
            return _repo.FilterStore(p_store);
        }

        public Stores GetStoreByNumber(string p_storeID)
        {
            return _repo.GetStoreByNumber(p_storeID);
        }
        public int CountCustomers(Stores p_store)
        {
            return _repo.CountCustomers(p_store);
        }
        public List<LineItems> SystemStoreInventory(Stores p_store)
        {
            return _repo.SystemStoreInventory(p_store);
        }
        public List<LineItems> GamesStoreInventory(Stores p_store)
        {
            return _repo.GameStoreInventory(p_store);
        }
        public void AddSystemStoreInventory(string p_store, LineItems p_line)
        {
            _repo.AddSystemStoreInventory(p_store, p_line);
        }
        public void AddGamesStoreInventory(string p_store, LineItems p_line)
        {
            _repo.AddGamesStoreInventory(p_store, p_line);
        }
        public void SellSystemStoreInventory(string p_store, LineItems p_line)
        {
            _repo.SellSystemStoreInventory(p_store, p_line);
        }
        public void SellGamesStoreInventory(string p_store, LineItems p_line)
        {
            _repo.SellGamesStoreInventory(p_store, p_line);
        }
        public void SellStoreInventory(string p_store, List<LineItems> p_line)
        {
            foreach (LineItems item in p_line)
            {
                if (item.liGame==null)
                {
                    SellSystemStoreInventory(p_store, item);
                } else
                {
                    SellGamesStoreInventory(p_store, item);
                }

            }
        }
        public void AddStoreInventory(string p_store, List<LineItems> p_line)
        {
            foreach (LineItems item in p_line)
            {
                if (item.liGame==null)
                {
                    AddSystemStoreInventory(p_store, item);
                } else
                {
                    AddGamesStoreInventory(p_store, item);
                }

            }
        }
    }
}