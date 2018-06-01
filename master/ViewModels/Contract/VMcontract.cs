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
    class VMcontract
    {
        VMcontracts parent;
        Ccontract root;
        ReadOnlyCollection<VMfunction> functions;

        public VMcontract(Ccontract root, VMcontracts parent)
        {
            this.root = root;
            this.parent = parent;
            this.functions = new ReadOnlyCollection<VMfunction>((
                             from functions
                             in this.root.Functions
                             select new VMfunction(functions, this)).ToList());
        }

        public ReadOnlyCollection<VMfunction> Functions
        {
            get { return this.functions; }
        }

        public int CountPublic
        {
            get { return this.functions.Where(f => f.Access == Cfunction.ACCESSIBILITY.Public).Count(); }
        }
        public int CountControlled
        {
            get { return this.functions.Where(f => f.Access == Cfunction.ACCESSIBILITY.Controlled).Count(); }
        }
        public int CountPrivate
        {
            get { return this.functions.Where(f => f.Access == Cfunction.ACCESSIBILITY.Private).Count(); }
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
