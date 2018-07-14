using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableGroup : MyRootedParentalBindableBase
    {
        public new VariableGroup Root
        {
            get { return this.root as VariableGroup; }
        }
        public new VMvariableList Parent
        {
            get { return this.parent as VMvariableList; }
        }

        public VMvariableGroup(VariableGroup root, VMvariableList parent) : base(root, parent)
        {

        }

        public string Type
        {
            get { return this.Root.Type.Name; }
        }

        //public IList<VMvariableGroupAlias> Aliases
        //{
        //    get
        //    {
        //        return (from a in this.Root.Aliases
        //                select new VMvariableGroupAlias(a, this)).ToList();
        //    }
        //}

    }
}
