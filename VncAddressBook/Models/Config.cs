using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VncAddressBook.Models
{
    public class Config
    {
        public bool UsingTightVnc { get; set; } = true;
        public bool UsingRealVnc { get; set; } = false;
    }
}
