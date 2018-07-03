using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.ViewModels.Contract.Block.Conditioning;

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

        public VMsimpleIf(MySimpleIf root) : base(root)
        {
            this.condition = new VMconditionBase(root.Condition, null);
        }

        public override object Clone()
        {
            return new VMsimpleIf(this.root.Clone() as MySimpleIf)
            {
                Parent = this.Parent
            };
        }

        protected override string BlockName() { return "Simple if block"; }

        protected override string Required() { return string.Format(this.reqFormat, "2 variables, 1 comparison"); }

        protected override string Optional() { return string.Empty; }
    }
}
