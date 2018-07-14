using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using System.Collections.ObjectModel;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMelseIf : VMbase
    {
        public new Base Root
        {
            get { return this.root as Base; }
        }

        public VMelseIf(Base root, VMfunction parent) : base(root, parent)
        {

        }

        public override object Clone()
        {
            return new VMelseIf(this.Root.Clone() as Base, this.Parent);
        }

        protected override string BlockName() { return "Else If - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 \"If block\""); }
        protected override string Optional() { return string.Empty; }
    }
}
