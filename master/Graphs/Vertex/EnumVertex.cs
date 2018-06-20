using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;
using master.Models.Data.Component.Components;

namespace master.Graphs
{
    public class EnumVertex : BaseVertex
    {
        protected new MyEnum root;

        public EnumVertex(MyEnum root) : base(root)
        {
            this.root = root;
        }

        public IList<string> Components
        {
            get { return new List<string>(this.root.Options.Select(c => string.Format("  o {0}", c))); }
        }
    }
}
