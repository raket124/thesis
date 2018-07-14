using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using Prism.Commands;

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
    }
}
