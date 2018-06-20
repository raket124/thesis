using master.Basis;
using master.Models.Contract.Block.Conditioning;
using master.Utils;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Conditioning
{
    public class VMconditionBase : MyBindableBase, ICloneable
    {
        protected readonly Dictionary<ConditionBase.COMPARE, string> COMPARE_DIC = new Dictionary<ConditionBase.COMPARE, string>()
        {
            { ConditionBase.COMPARE.equal, "==" },
            { ConditionBase.COMPARE.not_equal, "!=" },
            { ConditionBase.COMPARE.greater, ">" },
            { ConditionBase.COMPARE.greater_or_equal, ">=" },
            { ConditionBase.COMPARE.lesser, "<" },
            { ConditionBase.COMPARE.lesser_or_equal, "<=" }
        };

        protected ConditionBase root;
        public ConditionBase Root
        {
            get { return this.root; }
        }
        protected VMcondition parent;
        public VMcondition Parent
        {
            get { return this.parent; }
        }

        public DelegateCommand CommandRemove { get; private set; }

        public VMconditionBase(ConditionBase root, VMcondition parent)
        {
            this.root = root;
            this.parent = parent;

            this.CommandRemove = new DelegateCommand(() => this.Parent.Root.Conditions.Remove(this.Root));
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public IList<string> Comparisons
        {
            get { return this.COMPARE_DIC.Values.ToList(); }
        }
    }
}
