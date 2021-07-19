using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class SystemsOwned
    {
        public string Customer { get; set; }
        public string SystemOwned { get; set; }

        public virtual Customer CustomerNavigation { get; set; }
        public virtual System SystemOwnedNavigation { get; set; }
    }
}
