using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class Ccontracts
    {
        protected List<Ccontract> contracts;

        public Ccontracts()
        {
            this.contracts = new List<Ccontract>();

            for (int i = 0; i < 10; i++)
                this.contracts.Add(new Ccontract() { Name = string.Format("Test {0:00}", i) });
        }

        public IList<Ccontract> Contracts
        {
            get { return this.contracts; }
        }
    }
}
