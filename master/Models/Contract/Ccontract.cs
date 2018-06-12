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
    class Ccontract : Basis
    {
        [DataMember]
        protected ObservableCollection<Cfunction> functions;

        public Ccontract(string name) : base(name)
        {
            this.functions = new ObservableCollection<Cfunction>();
        }

        public ObservableCollection<Cfunction> Functions
        {
            get { return this.functions; }
            set
            {
                this.functions = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
