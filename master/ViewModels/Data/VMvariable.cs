using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMvariable
    {
        private Mvariable root;

        public VMvariable(Mvariable root)
        {
            this.root = root;
        }

        public string Name
        {
            get { return this.root.Name; }
        }

        public string Type
        {
            get { return this.root.Type; }
        }

        public string Relation
        {
            get { return this.root.Relation == Mvariable.RELATION.variable ? "  o" : "-->"; }
        }

        public string List
        {
            get { return this.root.List ? "[]" : string.Empty; }
        }

        public string Optional
        {
            get { return this.root.Optional ? "optional" : string.Empty; }
        }
    }
}
