using master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMcontracts
    {
        protected Ccontracts root;
        protected ReadOnlyCollection<VMcontract> contracts;

        public VMcontracts(Ccontracts root)
        {
            this.root = root;
            this.contracts = new ReadOnlyCollection<VMcontract>((
                             from contracts 
                             in this.root.Contracts
                             select new VMcontract(contracts, this)).ToList());
        }

        public ReadOnlyCollection<VMcontract> Contracts
        {
            get { return this.contracts; }
        }
    }
}
