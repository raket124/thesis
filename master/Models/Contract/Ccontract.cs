using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class Ccontract
    {
        protected List<string> functions;

        protected string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
