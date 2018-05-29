using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Blocks
{
    abstract class BlockContent : BlockBase
    {
        protected BlockBase content;
        public BlockBase Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public BlockContent() : base()
        {
            this.content = new BlockEmpty();
        }
    }
}
