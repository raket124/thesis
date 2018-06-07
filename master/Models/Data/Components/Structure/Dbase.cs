using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace master.Models
{
    [DataContract]
    abstract class Dbase
    {
        [DataMember]
        protected string docs;
        [DataMember]
        protected string name;

        public Dbase(string name)
        {
            this.name = name;
            this.docs = string.Empty;
        }

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
        public string ClassName
        {
            get { return this.ObjectName(); }
        }

        protected abstract string ObjectName();
    }
}
