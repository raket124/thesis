using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    abstract class Didentity : Dinheritance
    {
        [DataMember]
        protected string identifier;

        public string Identifier
        {
            get { return this.identifier; }
            set { this.identifier = value; }
        }

        public Didentity(string name) : base(name)
        {
            this.identifier = string.Empty;
        }
    }
}
