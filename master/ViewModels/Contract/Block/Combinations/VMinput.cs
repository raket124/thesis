using master.Models;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Combinations;
using master.ViewModels.Contract.Block.Blocks;
using master.Windows.Blocks;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Combinations
{
    class VMinput : VMbase
    {
        public new MyInput Root
        {
            get { return this.root as MyInput; }
        }
        private List<VMinputVariable> variables;
        public List<VMinputVariable> Variables
        {
            get { return this.variables; }
            set
            {
                this.variables = value;
                this.NotifyPropertyChanged();
            }
        }
        //public DelegateCommand CommandAdd { get; private set; }

        public VMinput(MyInput root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new InputWindow() { DataContext = this }.ShowDialog());


            this.WrapVars();

            //this.CommandAdd = new DelegateCommand(this.Add);
            this.Root.Variables.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapVars();
        }

        private void WrapVars()
        {
            this.Variables = new List<VMinputVariable>(
                             from var in this.Root.Variables
                             select new VMinputVariable(var, this));
        }

        public override object Clone()
        {
            return new VMinput(this.Root.Clone() as MyInput, this.Parent);
        }

        protected override string BlockName() { return "Input - block"; }
        protected override string Required() { return string.Empty; }
        protected override string Optional() { return string.Empty; }

        //public IList<VariableLink> ViewVariables
        //{
        //    get { return (from v in this.Root.Variables select v.Value.Listing).ToList(); }
        //}


        //public void Add()
        //{
        //    this.Root.Vars.Add(new MyVariable(typeof(string)));
        //    this.Parent.FullRefresh();
        //}

        //protected override List<VMvariable> GetVariables()
        //{
        //    return new List<VMvariable>(this.Vars);
        //}
    }
}
