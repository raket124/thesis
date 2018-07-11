using master.Models;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMassign : VMbase
    {
        public new MyAssign Root
        {
            get { return this.root as MyAssign; }
        }

        public DelegateCommand CommandSetVariable { get; private set; }
        public DelegateCommand CommandSetValue { get; private set; }

        public VMassign(MyAssign root) : base(root)
        {
            this.CommandSetVariable = new DelegateCommand(this.SetVariable);
            this.CommandSetValue = new DelegateCommand(this.SetValue);
        }

        public override object Clone()
        {
            return new VMassign(this.root.Clone() as MyAssign)
            {
                Parent = this.Parent
            };
        }

        protected override string BlockName() { return "Block chain block"; }

        protected override string Required() { return string.Format(this.reqFormat, "2 variables"); }

        protected override string Optional() { return string.Empty; }

        public string Variable
        {
            get { return this.Root.Variable; }
            set
            {
                this.Root.Variable = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Value
        {
            get { return this.Root.Value; }
            set
            {
                this.Root.Value = value;
                this.NotifyPropertyChanged();
            }
        }

        public void SetVariable()
        {
            this.Variable = this.SelectVar();
        }

        public void SetValue()
        {
            this.Value = this.SelectVar();
        }

        //public IList<string> AliasOptions
        //{
        //    get { return this.Parent.Aliases; }
        //}

        //public override void FullRefresh()
        //{
        //    this.NotifyPropertyChanged("Aliases");
        //}
    }
}
