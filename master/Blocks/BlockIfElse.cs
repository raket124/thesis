using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Blocks
{
    class BlockIfElse : BlockContent
    {
        public BlockIfElse() : base()
        {
            this.Add("if({0} {1} {2}) {{", "X", "<", "Y");
            this.Add("{0}", '\t' + "// test");
            this.Add("}}");
        }

        public override string ToCode(int indent = 0)
        {
            throw new NotImplementedException();
        }
    }
}
