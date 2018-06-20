using master.Basis;
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
    public class ContractModel : ObjectBase
    {
        [DataMember]
        protected ObservableCollection<Function> functions;
        public ObservableCollection<Function> Functions
        {
            get { return this.functions; }
            set { this.functions = value; }
        }

        public ContractModel(string name) : base(name)
        {
            this.functions = new ObservableCollection<Function>();
        }
    }
}
