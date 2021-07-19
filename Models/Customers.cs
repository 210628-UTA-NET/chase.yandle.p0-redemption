using System;
using System.Collections.Generic;
using System.Globalization;

namespace Models
{
    public class Customers
    {
        public DateTime _cBDay=DateTime.Today;
        public string ageNullIfZero;
        private int _cAge;
        private List<string> _cSystems=new List<string>();
        public string cName { get; set; }
        public string cStreet { get; set; }
        public string cCity { get; set; }
        public string cState { get; set; }
        public string cPhone { get; set; }
        public string cEmail { get; set; }
        public DateTime cBDay
        { get
        {
            return _cBDay;
        }
        set
        {
            _cBDay=value;
            this.cAge=(int)((DateTime.Today-_cBDay).TotalDays/365.25);
        }
        }
        public int cAge
        { get
        {
            return _cAge;
        }
        private set
        {
            _cAge=value;
            ageNullIfZero=_cAge.ToString();
        }
        }
        public List<Orders> cOrders { get; set; }
        public string cStoreAddedAt { get; set; }
        public string cID { get; set; }
    }
}