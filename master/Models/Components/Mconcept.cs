using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Mconcept : Minheritance
    {
        public Mconcept(string name) : base(name)
        {

        }
    }
}
