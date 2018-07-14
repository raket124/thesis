using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableList : MyRootedBindableBase
    {
        public new VariableList Root
        {
            get { return this.root as VariableList; }
        }

        public VMvariableList(VariableList root) : base(root)
        {

        }

        protected IList<VMvariableGroup> VariableGroups
        {
            get
            {
                return (from vg in this.Root.VariableGroups
                        select new VMvariableGroup(vg, this)).ToList();
            }
        }

        protected IList<VMobjectGroup> ObjectGroups
        {
            get
            {
                return (from og in this.Root.ObjectGroups
                        select new VMobjectGroup(og, this)).ToList();
            }
        }

        public IList<object> Groups
        {
            get
            {
                var output = new List<object>();
                output.AddRange(this.VariableGroups);
                output.AddRange(this.ObjectGroups);
                return output;
            }
        }
    }
}
