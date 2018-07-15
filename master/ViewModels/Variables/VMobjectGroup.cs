using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMobjectGroup : MyRootedParentalBindableBase
    {
        public new ObjectGroup Root
        {
            get { return this.root as ObjectGroup; }
        }
        public new VMvariableList Parent
        {
            get { return this.parent as VMvariableList; }
        }

        public VMobjectGroup(ObjectGroup root, VMvariableList parent) : base(root, parent)
        {

        }

        public Type Type
        {
            get { return this.Root.Type; }
        }

        public IList<VMobjects> Objects
        {
            get
            {
                return (from o in this.Root.Objects
                        select new VMobjects(o, this)).ToList();
            }
        }

        public int CountTotal
        {
            get { return this.Root.Objects.Sum(x => x.Variables.Count); }
        }
    }
}
