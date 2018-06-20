using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data.Component
{
    [DataContract]
    abstract public class Identity : Inheritance
    {
        [DataMember]
        protected string identifier;
        public string Identifier
        {
            get { return this.identifier; }
            set { this.identifier = value; }
        }

        public Identity(string name) : base(name)
        {
            this.identifier = string.Empty;
        }
    }
}
