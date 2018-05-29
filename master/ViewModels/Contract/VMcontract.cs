using master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMcontract : INotifyPropertyChanged
    {
        VMcontracts parent;
        Ccontract root;
        ReadOnlyCollection<VMfunction> functions;

        public VMcontract(Ccontract root, VMcontracts parent)
        {
            this.root = root;
            this.parent = parent;

            this.functions = new ReadOnlyCollection<VMfunction>(new VMfunction[] {
                    new VMfunction(null),
                    new VMfunction(null),
                    new VMfunction(null),
                    new VMfunction(null),
                    new VMfunction(null),
                    new VMfunction(null),
                    new VMfunction(null)
                });
        }

        public ReadOnlyCollection<VMfunction> Functions
        {
            get { return this.functions; }
        }

        public string Name
        {
            get { return this.root.Name; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
