using System;
using System.Collections.Generic;

namespace Models
{
    public class Games
    {
        public string gName { get; set; }
        public float gMSRP { get; set; }
        public string gSystem { get; set; }
        public int gAgeRating { get; set; }
        public DateTime gReleaseDate { get; set; }
    }
    public class Systems
    {
        public string sName { get; set; }
        public DateTime sReleaseDate { get; set; }
        public float sMSRP { get; set; }
        public List<string> _availSystems = new List<string>(){};
    }
}
