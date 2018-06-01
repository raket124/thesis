using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class Ccontract : Basis
    {
        protected List<Cfunction> functions;

        public Ccontract(string name) : base(name)
        {
            this.functions = new List<Cfunction>();
        }

        public void AddFunction(Cfunction function)
        {
            this.functions.Add(function);
        }

        public IList<Cfunction> Functions
        {
            get { return this.functions; }
        }
    }
}
