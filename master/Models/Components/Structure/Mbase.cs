using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace master.Models
{
    [DataContract]
    abstract class Mbase
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

        public Mbase(string name)
        {
            this.name = name;
            this.docs = string.Empty;
        }
    }
}
