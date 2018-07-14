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
    class VMelse : VMbase
    {
        public new MyElse Root
        {
            get { return this.root as MyElse; }
        }

        public VMelse(MyElse root, VMfunction parent) : base(root, parent)
        {

        }

        public override object Clone()
        {
            return new VMelse(this.Root.Clone() as MyElse, this.Parent);
        }

        protected override string BlockName() { return "Else - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 \"If block\""); }
        protected override string Optional() { return string.Empty; }
    }
}
