using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class Mfile : Mbase
    {
        protected List<object> components;
        protected string fileNamespace;

        public string Namespace
        {
            get { return this.fileNamespace; }
            set { this.fileNamespace = value; }
        }

        public Mfile()
        {
            this.components = new List<object>();
        }

        public void AddComponent(object component)
        {
            this.components.Add(component);
        }
    }
}
