using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Blocks
{
    class BlockEmpty : BlockBase
    {
        public override string ToCode(int indent = 0)
        {
            this.Add("// Content expected");
            return this.ProduceCode(indent);
        }
    }
}
