using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data.Component
{
    [DataContract]
    public abstract class Inheritance : Base
    {
        [DataMember]
        protected string parent;
        public string Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        [DataMember]
        protected bool isAbstract;
        public bool Abstract
        {
            get { return this.isAbstract; }
            set { this.isAbstract = value; }
        }
        [DataMember]
        protected List<Variable> components;
        public List<Variable> Components
        {
            get { return this.components; }
            set { this.components = value; }
        }

        public Inheritance(string name) : base(name)
        {
            this.parent = string.Empty;
            this.isAbstract = false;
            this.components = new List<Variable>();
        }
    }
}
