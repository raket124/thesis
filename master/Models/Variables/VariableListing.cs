using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    class VariableListing
    {
        protected List<ObjectGroup> objectTypes;
        public List<ObjectGroup> ObjectTypes
        {
            get { return this.objectTypes; }
            set { this.objectTypes = value; }
        }
        protected List<VariableGroup> variableTypes;
        public List<VariableGroup> VariableTypes
        {
            get { return this.variableTypes; }
            set { this.variableTypes = value; }
        }

        public VariableListing()
        {
            this.objectTypes = new List<ObjectGroup>();
            this.variableTypes = new List<VariableGroup>();
        }
    }
}
