using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    class VariableGroup
    {
        protected Type type;
        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        protected List<string> aliases;
        public List<string> Aliases
        {
            get { return this.aliases; }
            set { this.aliases = value; }
        }

        public VariableGroup()
        {
            this.type = null;
            this.aliases = new List<string>();
        }
    }
}
