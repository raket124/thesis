using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using System.Collections.ObjectModel;
using master.Models.Contract.Block.Blocks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMend : VMbase
    {
        public new MyEnd Root
        {
            get { return this.root as MyEnd; }
        }

        public VMend(MyEnd root, VMfunction parent) : base(root, parent)
        {

        }

        public override object Clone()
        {
            return new VMend(this.Root.Clone() as MyEnd, this.Parent);
        }

        protected override string BlockName() { return "End - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 \"If block\""); }
        protected override string Optional() { return string.Empty; }
    }
}
