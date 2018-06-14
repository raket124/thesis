using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Bassign : Bbase
    {
        [DataMember]
        protected string variable;
        [DataMember]
        protected string value;
    }
}
