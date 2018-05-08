using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    abstract class Minheritance : Mbase
    {
        [DataMember]
        protected string parent;
        [DataMember]
        protected bool isAbstract;
        [DataMember]
        protected List<Mvariable> components;

        public string Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public bool Abstract
        {
            get { return this.isAbstract; }
            set { this.isAbstract = value; }
        }

        public Minheritance(string name) : base(name)
        {
            this.parent = string.Empty;
            this.isAbstract = false;
            this.components = new List<Mvariable>();
        }

        public void AddComponent(Mvariable component)
        {
            this.components.Add(component);
        }
    }
}
