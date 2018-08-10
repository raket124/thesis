using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;
using master.Models.Data.Component;
using master.ViewModels.Data;

namespace master.Graphs
{
    class IdentityVertex : InheritanceVertex
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

        public new IList<VMvariable> Components
        {
            get { return new List<VMvariable>(this.root.Components.Select(c => new VMvariable(c) { Identifier = this.Id == c.Name })); }
        }
    }
}
