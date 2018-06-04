using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Ccontracts
    {
        [DataMember]
        protected List<Ccontract> contracts;

        public Ccontracts()
        {
            this.contracts = new List<Ccontract>();
        }

        public void Add(Ccontract contract)
        {
            this.contracts.Add(contract);
        }

        public IList<Ccontract> Contracts
        {
            get { return this.contracts; }
        }
    }
}
