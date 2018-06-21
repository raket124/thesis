using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMerror : VMbase
    {
        public VMerror(Base root) : base(root)
        {
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        protected override string BlockName() { return "Error block"; }

        protected override string Required() { return string.Format(this.reqFormat, "1 text"); }

        protected override string Optional() { return string.Format(this.optFormat, "x aliases"); }
    }
}
