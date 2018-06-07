using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Dparticipant : Didentity
    {
        public Dparticipant(string name) : base(name)
        {

        }

        protected override string ObjectName()
        {
            return "Participant";
        }
    }
}
