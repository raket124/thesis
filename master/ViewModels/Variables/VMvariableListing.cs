using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableListing : MyRootedBindableBase
    {
        public new VariableListing Root
        {
            get { return this.root as VariableListing; }
        }

        protected IList<VMobjectGroup> ObjectTypes
        {
            get { return (from o in this.Root.ObjectTypes select new VMobjectGroup(o, this)).ToList(); }
        }
        protected IList<VMvariableGroup> VariableTypes
        {
            get { return (from v in this.Root.VariableTypes select new VMvariableGroup(v, this)).ToList(); }
        }

        public VMvariableListing(VariableListing root) : base(root)
        {

        }

        public IList<object> Types
        {
            get
            {
                var output = new List<object>();
                output.AddRange(this.VariableTypes);
                output.AddRange(this.ObjectTypes);
                return output;
            }
        }
    }
}
