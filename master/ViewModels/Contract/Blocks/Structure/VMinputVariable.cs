using master.Models;
using master.Utils;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMinputVariable : VMBbase
    {
        protected VMinput parent;
        protected new Bvariable root;
        public new Bvariable Root
        {
            get { return this.root; }
            set
            {
                this.root = value;
                base.root = value;
            }
        }

        public DelegateCommand<object> CommandRemove { get; private set; }

        public VMinputVariable(Bvariable root, VMinput parent) : base(root)
        {
            this.root = root;
            this.parent = parent;

            this.CommandRemove = new DelegateCommand<object>(this.Remove);
        }

        public void Remove(object input)
        {
            this.parent.Root.Vars.Remove((input as VMinputVariable).Root);
        }

        public override object Clone()
        {
            return new VMinputVariable(this.root, this.parent);
        }

        public IList<Bvariable.TYPES> Types
        {
            get { return EnumUtil.EnumToList<Bvariable.TYPES>(); }
        }

        public IList<string> ObjectNames
        {
            get
            {
                return new List<string>()
                {
                    "LegalOwnerAdmin",
                    "CompoundAdmin"
                };
            }
        }

        public string Alias
        {
            get { return "Not implemented yet"; }
            set { }
        }

        protected override string BlockName()
        {
            return "Function input";
        }
    }
}
