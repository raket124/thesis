using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    [DataContract]
    class ContractCollection
    {
        [DataMember]
        protected ObservableCollection<ContractModel> contracts;
        public ObservableCollection<ContractModel> Contracts
        {
            get { return this.contracts; }
            set { this.contracts = value; }
        }

        public ContractCollection()
        {
            this.contracts = new ObservableCollection<ContractModel>();
        }
    }
}
