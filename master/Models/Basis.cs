using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Basis
    {
        [DataMember]
        protected string docs;
        [DataMember]
        protected string name;

        public string Docs
        {
            get { return this.docs; }
            set { this.docs = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Basis()
        {
            this.name = string.Empty;
            this.docs = string.Empty;
        }
    }
}
