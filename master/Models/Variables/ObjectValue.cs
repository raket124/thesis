using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    class ObjectValue
    {
        protected string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        protected List<string> aliases;
        public List<string> Aliases
        {
            get { return this.aliases; }
            set { this.aliases = value; }
        }

        public ObjectValue()
        {
            this.name = string.Empty;
            this.aliases = new List<string>();
        }
    }
}
