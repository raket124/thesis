using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data.Component.Components
{
    [DataContract]
    class MyParticipant : Identity
    {
        public MyParticipant(string name) : base(name) { }

        protected override string ObjectName()
        {
            return "Participant";
        }
    }
}
