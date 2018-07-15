using master.Models;
using master.Models.Contract.Block.Blocks;
using master.Models.Data.Component.Components;
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

        public VMassign(MyAssign root, VMfunction parent) : base(root, parent)
        {
            this.CommandSetVariable = new DelegateCommand(() => this.Variable = this.Parent.SelectVar());
            this.CommandSetValue = new DelegateCommand(() => this.Value = this.Parent.SelectVar());
        }

        public override object Clone()
        {
            return new VMassign(this.Root.Clone() as MyAssign, this.Parent);
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
    }
}
