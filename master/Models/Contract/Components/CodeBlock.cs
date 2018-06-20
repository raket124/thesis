using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Blocks
{
    public class CodeBlock
    {
        //protected List<string> _documentation;
        //protected string _param;
        //protected string _return;
        //protected string _transaction;

        List<BlockBase> code;

        public CodeBlock()
        {
            this.code = new List<BlockBase>();
        }

        public void Add(BlockBase bb)
        {
            this.code.Add(bb);
        }

        public string ToCode()
        {
            return string.Join(Environment.NewLine, this.code.Select(s => s.ToCode()));
        }
    }
}
