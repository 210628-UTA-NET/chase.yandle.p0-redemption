using System;
using System.Collections.Generic;
using DL;
using Models;

namespace BL
{
    public class CustomerBL
    {
        private CustRepository _repo;
        public CustomerBL(CustRepository p_repo)
        {
            _repo=p_repo;
        }

        public Customers AddCustomer(Customers _cAdd)
        {
            return _repo.AddCustomer(_cAdd);
        }

        public List<Customers> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }
        public Customers GetCustomerByID(string p_cid)
        {
            return _repo.GetCustomerByID(p_cid);
        }
        public Customers EditCustomer(Customers p_cust)
        {
            _repo.EditCustomer(p_cust);
            return p_cust;
        }

        public List<Customers> FilterCustomer(Customers p_cust)
        {
            return _repo.FilterCustomer(p_cust);
        }

        public Customers DeleteCustomer(Customers p_cust)
        {
            _repo.DeleteCustomer(p_cust);
            return p_cust;
        }
    }
}