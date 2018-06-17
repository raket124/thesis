using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;
using master.ViewModels.Data;
using master.Models.Data.Component;

namespace master.Graphs
{
    class InheritanceVertex : BaseVertex
    {
        protected new Inheritance root;

        public InheritanceVertex(Inheritance root) : base(root)
        {
            this.root = root;
        }

        public string Parent
        {
            get { return this.root.Parent; }
        }

        public string Abstract
        {
            get { return this.root.Abstract ? "Yes" : "No"; }
        }

        public IList<VMvariable> Components
        {
            get { return new List<VMvariable>(this.root.Components.Select(c => new VMvariable(c))); }
        }
    }
}
