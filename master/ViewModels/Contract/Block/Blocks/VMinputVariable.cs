using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using Prism.Commands;
using master.Models.Data.Component.Components;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMinputVariable : VMvariable
    {
        protected VMinput parent;
        public VMinput Parent
        {
            get { return this.parent; }
        }

        public DelegateCommand<object> CommandRemove { get; private set; }

        public VMinputVariable(Variable root, VMinput parent) : base(root)
        {
            this.parent = parent;

            this.CommandRemove = new DelegateCommand<object>(this.Remove);
        }

        public void Remove(object input)
        {
            this.Parent.Root.Vars.Remove((input as VMvariable).Root);
        }

        public IList<Type> Types
        {
            get
            {
                return new List<Type>()
                {
                    typeof(int),
                    typeof(long),
                    typeof(double),
                    typeof(bool),
                    typeof(string),
                    typeof(DateTime),
                    typeof(MyAsset),
                    typeof(MyConcept),
                    typeof(MyEnum),
                    typeof(MyEvent),
                    typeof(MyParticipant),
                    typeof(MyTransaction)
                };
            }
        }
    }
}
