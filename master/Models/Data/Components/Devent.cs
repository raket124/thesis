using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Devent : Dinheritance
    {
        public Devent(string name) : base(name)
        {

        }
    }
}
