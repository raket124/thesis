using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Basis
{
    [DataContract]
    abstract public class ObjectBase
    {
        [DataMember]
        protected string docs;
        public string Docs
        {
            get { return this.docs; }
            set { this.docs = value; }
        }
        [DataMember]
        protected string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public ObjectBase() : this(string.Empty) { }

        public ObjectBase(string name)
        {
            this.name = name;
            this.docs = string.Empty;
        }
    }
}
