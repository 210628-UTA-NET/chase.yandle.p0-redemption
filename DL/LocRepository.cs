using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using Entity = DL.Entities;

namespace DL
{
    public class StoreRepository
    {
        private Entities.DemoDbContext _context;
        public StoreRepository(Entities.DemoDbContext p_context)
        {  
            _context=p_context;       
        }
        public List<Stores> GetAllStores()
        {
            return _context.Locations.Select(
                loc =>
                new Stores()
                {
                    stNumber=loc.LocationId,
                    stStreet=loc.Street,
                    stCity=loc.City,
                    stState=loc.State,
                    stPhone=loc.Phone,
                    stEmail=loc.Email
                }
            ).ToList();
        }
        public Stores AddStore(Stores p_store)
        {
            _context.Locations.Add(new Entity.Location{
                LocationId=p_store.stNumber,
                Street=p_store.stStreet,
                City=p_store.stCity,
                State=p_store.stState,
                Phone=p_store.stPhone,
                Email=p_store.stEmail
            });
            _context.SaveChanges();
            return p_store;
        }
        public List<Stores> FilterStore(Stores p_store)
        {
            throw new NotImplementedException();
        }
        public Stores GetStoreByNumber(string p_storeID)
        {
            List<Stores> sSet = new List<Stores>();
            sSet = GetAllStores();
            return sSet.Find(store => store.stNumber==p_storeID);
        }
        public int CountCustomers(Stores p_store)
        {
            
            return _context.Customers.Select(
                cust=>
                cust.CustomerId.Substring(0,4)==p_store.stNumber
            ).Count();
        }
        public List<LineItems> SystemStoreInventory(Stores p_store)
        {
            return _context.Inventories
                .Where(lItems => lItems.Location==p_store.stNumber && lItems.Product.Substring(0,1) =="S")
                .Select(lItems =>
                new LineItems()
                {
                    liSystem=lItems.Product.Substring(1),
                    liQuantity=(int)lItems.Quantity
                }
            ).ToList();
        }
        public List<LineItems> GameStoreInventory(Stores p_store)
        {
            return _context.Inventories
                .Where(lItems => lItems.Location==p_store.stNumber && lItems.Product.Substring(0,1) =="G")
                .Select(lItems =>
                new LineItems()
                {
                    liGame=lItems.Product.Substring(1),
                    liQuantity=(int)lItems.Quantity
                }
            ).ToList();
        }
        public void AddSystemStoreInventory(string p_store, LineItems p_line)
        {
            LineItems temp = new LineItems();
            temp =_context.Inventories
            .Where(game => "S"+p_line.liSystem== game.Product)
            .Select( game =>
                new LineItems()
                {
                    liQuantity=(int)game.Quantity
                }
            ).ToList()[0];
            int old = temp.liQuantity;
            Entities.Inventory update= new Entity.Inventory();
            update.Location=p_store;
            update.Product="S"+p_line.liSystem;
            update.Quantity=old+p_line.liQuantity;
            _context.Inventories.Update(update);
            _context.SaveChanges();
        }
        public void AddGamesStoreInventory(string p_store, LineItems p_line)
        {
            LineItems temp = _context.Inventories
            .Where(game => "G"+p_line.liGame+" "+p_line.liSystem== game.Product)
            .Select( game =>
                new LineItems()
                {
                    liQuantity=(int)game.Quantity
                }
            ).ToList()[0];
            int old = temp.liQuantity;
            Entities.Inventory update= new Entity.Inventory();
            update.Location=p_store;
            update.Product="G"+p_line.liGame+" "+p_line.liSystem;
            update.Quantity=old+p_line.liQuantity;
            _context.Inventories.Update(update);
            _context.SaveChanges();
        }
        public void SellSystemStoreInventory(string p_store, LineItems p_line)
        {
            LineItems temp = _context.Inventories
            .Where(game => "S"+p_line.liSystem== game.Product)
            .Select( game =>
                new LineItems()
                {
                    liQuantity=(int)game.Quantity
                }
            ).ToList()[0];
            int old = temp.liQuantity;
            Entities.Inventory update= new Entity.Inventory();
            update.Location=p_store;
            update.Product="S"+p_line.liSystem;
            update.Quantity=old-p_line.liQuantity;
            _context.Inventories.Update(update);
            _context.SaveChanges();
        }
        public void SellGamesStoreInventory(string p_store, LineItems p_line)
        {
            LineItems temp = _context.Inventories
            .Where(game => "G"+p_line.liGame+" "+p_line.liSystem== game.Product)
            .Select( game =>
                new LineItems()
                {
                    liQuantity=(int)game.Quantity
                }
            ).ToList()[0];
            int old = temp.liQuantity;
            Entities.Inventory update= new Entity.Inventory();
            update.Location=p_store;
            update.Product="G"+p_line.liGame+" "+p_line.liSystem;
            update.Quantity=old-p_line.liQuantity;
            _context.Inventories.Update(update);
            _context.SaveChanges();
        }
    }
}