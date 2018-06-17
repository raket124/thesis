using master.Models;
using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMassign : VMbase
    {
        protected new MyAssign root;

        public VMassign(MyAssign root) : base(root)
        {
            this.root = root;
        }

        public override object Clone()
        {
            return new VMassign(this.root.Clone() as MyAssign);
        }

        protected override string BlockName()
        {
            return "Block chain block";
        }
    }
}
