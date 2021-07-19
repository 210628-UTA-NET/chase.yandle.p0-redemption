using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;
using Entity = DL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Globalization;

namespace DL
{
    public class CustRepository
    {
        private Entities.DemoDbContext _context;
        public CustRepository(Entities.DemoDbContext p_context)
        {
            _context=p_context;
        }
        public Customers AddCustomer(Customers p_cust)
        {
            _context.Customers.Add(new Entity.Customer{
                CustomerId=p_cust.cID,
                Name=p_cust.cName,
                Street=p_cust.cStreet,
                City=p_cust.cCity,
                State=p_cust.cState,
                Phone=p_cust.cPhone,
                Email=p_cust.cEmail,
                Birthday=p_cust.cBDay,
            });

            _context.SaveChanges();
            return p_cust;
        }
        public List<Customers> GetAllCustomers()
        {   
            return _context.Customers.Select(
                cust =>
                new Customers()
                {
                    cID=cust.CustomerId,
                    cName=cust.Name,
                    cStreet=cust.Street,
                    cCity=cust.City,
                    cState=cust.State,
                    cPhone=cust.Phone,
                    cEmail=cust.Email,
                    cBDay=(DateTime)cust.Birthday
                }
            ).ToList();
        }

        public Customers GetCustomerByID(string p_string)
        {
            List<Customers> cSet = new List<Customers>();
            cSet = GetAllCustomers();
            return cSet.Find(customer => customer.cID==p_string);
        }

        public Customers EditCustomer(Customers p_cust)
        {
            Entities.Customer test = new Entity.Customer();
            test.CustomerId=p_cust.cID;
            test.Name=p_cust.cName;
            test.Street=p_cust.cStreet;
            test.City=p_cust.cCity;
            test.State=p_cust.cState;
            test.Phone=p_cust.cPhone;
            test.Email=p_cust.cEmail;
            test.Birthday=p_cust.cBDay;
            _context.Customers.Update(test);
            _context.SaveChanges();
            return p_cust;
        }

        public List<Customers> FilterCustomer(Customers p_cust)
        {
            List<Customers> cSet = new List<Customers>();
            cSet = GetAllCustomers();
            IEnumerable<Customers> filteredList =
            from cust in cSet
            where cust.cID==p_cust.cID ||
            cust.cName==p_cust.cName ||
            cust.cStreet==p_cust.cStreet ||
            cust.cCity==p_cust.cCity ||
            cust.cState==p_cust.cState ||
            cust.cPhone==p_cust.cPhone ||
            cust.cEmail==p_cust.cEmail ||
            cust.cBDay.ToString() == p_cust.cBDay.ToString()
            select cust;
            return filteredList.ToList();
        }
        public void DeleteCustomer(Customers p_cust)
        {
            p_cust.cName="Removed On "+DateTime.Today.ToString("yyyy-MM-dd");
            p_cust.cStreet="Removed";
            p_cust.cCity="Removed";
            p_cust.cState="XX";
            p_cust.cPhone="XXX-XXX-XXXX";
            p_cust.cEmail="Removed";
            p_cust.cBDay=DateTime.Today;
            EditCustomer(p_cust);
        }
    }
}