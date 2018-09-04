using master.Models.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    public class VariableGroup
    {
        protected Type type;
        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        protected List<MyVariable> variables;
        public List<MyVariable> Variables
        {
            get { return this.variables; }
            set { this.variables = value; }
        }

        public VariableGroup(Type type)
        {
            this.type = type;
            this.variables = new List<MyVariable>();
        }
    }
}
