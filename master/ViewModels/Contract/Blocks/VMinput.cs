using master.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMinput : VMBbase
    {
        protected new Binput root;
        public new Binput Root
        {
            get { return this.root; }
            set
            {
                this.root = value;
                base.Root = value;
                this.NotifyPropertyChanged();
            }
        }
        private ObservableCollection<VMinputVariable> vars;
        public ObservableCollection<VMinputVariable> Vars
        {
            get { return this.vars; }
            set
            {
                this.vars = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandAdd { get; private set; }

        public VMinput(Binput root) : base(root)
        {
            this.root = root;
            this.WrapVars();

            this.CommandAdd = new DelegateCommand(this.Add);
            this.Root.Vars.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapVars();
        }

        private void WrapVars()
        {
            this.Vars = new ObservableCollection<VMinputVariable>((
                        from var in this.root.Vars
                        select new VMinputVariable(var, this)));
        }


        public void Add()
        {

        }

        public override object Clone()
        {
            return new VMinput(this.root);
        }

        protected override string BlockName()
        {
            return "Input";
        }
    }
}
