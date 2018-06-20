using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;
using master.Models.Data.Component;

namespace master.Graphs
{
    public class IdentityVertex : InheritanceVertex
    {
        protected new Identity root;

        public IdentityVertex(Identity root) : base(root)
        {
            this.root = root;
        }

        public string Id
        {
            get { return this.root.Identifier; }
        }
    }
}
