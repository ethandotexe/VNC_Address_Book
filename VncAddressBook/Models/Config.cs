using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VncAddressBook.Models
{
    public class Config
    {
        public string UsingVnc { get; set; } // "tightVnc" OR "realVnc"
        public string OtherProperty { get; set; }
    }
}
