using master.Models;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMinput : VMbase
    {
        public new MyInput Root
        {
            get { return this.root as MyInput; }
        }
        private List<VMinputVariable> vars;
        public List<VMinputVariable> Vars
        {
            get { return this.vars; }
            set
            {
                this.vars = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandAdd { get; private set; }

        public VMinput(MyInput root, VMfunction parent) : base(root, parent)
        {
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
            this.Vars = new List<VMinputVariable>(
                        from var in this.Root.Vars
                        select new VMinputVariable(var, this));
        }

        public override object Clone()
        {
            return new VMinput(this.Root.Clone() as MyInput, this.Parent);
        }

        protected override string BlockName() { return "Input block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1+ variable(s)"); }
        protected override string Optional() { return string.Empty; }


        public void Add()
        {
            this.Root.Vars.Add(new Variable(typeof(string)));
            //this.Parent.FullRefresh();
        }

        protected override List<VMvariable> GetVariables()
        {
            return new List<VMvariable>(this.Vars);
        }
    }
}
