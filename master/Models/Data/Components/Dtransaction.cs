using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Dtransaction : Dinheritance
    {
        public Dtransaction(string name) : base(name)
        {

        }

        protected override string ObjectName()
        {
            return "Transaction";
        }
    }
}
