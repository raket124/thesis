using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableGroup : MyRootedBindableBase
    {
        public new VariableGroup Root
        {
            get { return this.root as VariableGroup; }
        }

        protected VMvariableListing parent;
        public VMvariableListing Parent
        {
            get { return this.parent; }
        }
        public string Type
        {
            get { return this.Root.Type.Name; }
        }
        public IList<VMvariableGroupAlias> Aliases
        {
            get
            {
                return (from a in this.Root.Aliases
                        select new VMvariableGroupAlias(a, this)).ToList();
            }
        }

        public VMvariableGroup(VariableGroup root, VMvariableListing parent) : base(root)
        {
            this.parent = parent;
        }
    }
}
