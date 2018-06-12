using master.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMcontract : MyBindableBase
    {
        private VMcontracts parent;
        private Ccontract root;
        public Ccontract Root
        {
            get { return this.root; }
        }
        private ObservableCollection<VMfunction> functions;
        public ObservableCollection<VMfunction> Functions
        {
            get { return this.functions; }
            set
            {
                this.functions = value;
                this.NotifyPropertyChanged();
            }
        }

        public VMcontract(Ccontract root, VMcontracts parent)
        {
            this.root = root;
            this.parent = parent;
            this.WrapFunctions();
            this.Root.Functions.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapFunctions();
        }

        private void WrapFunctions()
        {
            this.Functions = new ObservableCollection<VMfunction>((
                             from functions in this.root.Functions
                             select new VMfunction(functions, this)));

            this.NotifyPropertyChanged("CountPublic");
            this.NotifyPropertyChanged("CountControlled");
            this.NotifyPropertyChanged("CountPrivate");
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
    }
}
