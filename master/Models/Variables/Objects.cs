using master.Models.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    public class Objects
    {
        protected string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        protected List<MyVariable> variables;
        public List<MyVariable> Variables
        {
            get { return this.variables; }
            set { this.variables = value; }
        }

        public Objects(string name)
        {
            this.name = name;
            this.Variables = new List<MyVariable>();
        }
    }
}
