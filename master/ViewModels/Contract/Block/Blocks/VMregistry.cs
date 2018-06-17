using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Basis;
using master.Models.Contract.Block.Blocks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMregistry : VMbase
    {
        protected new MyUseRegistry root;
        public new MyUseRegistry Root
        {
            get { return this.root; }
        }

        public VMregistry(MyUseRegistry root) : base(root)
        {

        }

        public override object Clone()
        {
            return new VMregistry(this.root.Clone() as MyUseRegistry);
        }

        protected override string BlockName()
        {
            return "Registry block";
        }
    }
}
