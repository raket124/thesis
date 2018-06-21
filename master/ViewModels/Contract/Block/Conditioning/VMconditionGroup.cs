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
    class VMconditionGroup : MyBindableBase, ICloneable
    {
        protected readonly Dictionary<ConditionGroup.COMPARE, string> COMPARE_DIC = new Dictionary<ConditionGroup.COMPARE, string>()
        {
            { ConditionGroup.COMPARE.and, "&&" },
            { ConditionGroup.COMPARE.or, "||" }
        };

        protected ConditionGroup root;
        public ConditionGroup Root
        {
            get { return this.root; }
        }
        protected VMcondition parent;
        public VMcondition Parent
        {
            get { return this.parent; }
        }

        protected List<VMconditionBase> conditions;
        public List<VMconditionBase> Conditions
        {
            get { return this.conditions; }
            set
            {
                this.conditions = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandAdd { get; private set; }
        public DelegateCommand CommandRemove { get; private set; }

        public VMconditionGroup(ConditionGroup root, VMcondition parent)
        {
            this.root = root;
            this.parent = parent;
            this.Wrap();

            this.CommandRemove = new DelegateCommand(() => this.Parent.Root.Groups.Remove(this.Root));
        }

        private void Wrap()
        {
            this.Conditions = new List<VMconditionBase>();
            //this.Conditions = new List<VMconditionBase>(
            //                  from condition in this.Root.Conditions
            //                  select new VMconditionBase(condition, this));
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public IList<string> Comparisons
        {
            get { return this.COMPARE_DIC.Values.ToList(); }
        }

        public IList<object> ConditionSet
        {
            get { return EnumerableUtil.Interleave(this.Conditions, this.Root.Connectors).ToList(); }
        }
    }
}
