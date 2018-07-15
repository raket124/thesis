using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.ViewModels.Contract.Block.Conditioning;
using Prism.Commands;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMsimpleIf : VMbase
    {
        public new MySimpleIf Root
        {
            get { return this.root as MySimpleIf; }
        }
        protected VMconditionBase condition;
        public VMconditionBase Condition
        {
            get { return this.condition; }
            set
            {
                this.condition = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandSetLHS { get; private set; }
        public DelegateCommand CommandSetRHS { get; private set; }

        public VMsimpleIf(MySimpleIf root, VMfunction parent) : base(root, parent)
        {
            this.condition = new VMconditionBase(root.Condition, null);
            this.CommandSetLHS = new DelegateCommand(() => this.Condition.LHS = this.Parent.SelectVar());
            this.CommandSetRHS = new DelegateCommand(() => this.Condition.RHS = this.Parent.SelectVar());
        }

        public override object Clone()
        {
            return new VMsimpleIf(this.Root.Clone() as MySimpleIf, this.Parent);
        }

        protected override string BlockName() { return "Simple if block"; }
        protected override string Required() { return string.Format(this.reqFormat, "2 variables, 1 comparison"); }
        protected override string Optional() { return string.Empty; }
    }
}
