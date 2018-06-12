using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Ccontracts : MyBindableBase
    {
        [DataMember]
        protected ObservableCollection<Ccontract> contracts;
        public ObservableCollection<Ccontract> Contracts
        {
            get { return this.contracts; }
            set
            {

                this.contracts = value;
                this.NotifyPropertyChanged();
            }
        }

        public Ccontracts()
        {
            this.contracts = new ObservableCollection<Ccontract>();
        }
    }
}
