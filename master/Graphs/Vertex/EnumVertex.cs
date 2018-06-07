using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;

namespace master.Graphs
{
    class EnumVertex : BaseVertex
    {
        protected new Denum root;

        public EnumVertex(Denum root) : base(root)
        {
            this.root = root;
        }

        public IList<string> Components
        {
            get { return new List<string>(this.root.Options.Select(c => string.Format("  o {0}", c))); }
        }

    }
}
