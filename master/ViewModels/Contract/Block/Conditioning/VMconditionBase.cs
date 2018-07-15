using master.Basis;
using master.Models.Contract.Block.Conditioning;
using master.Utils;
using master.ViewModels.BaseTypes;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMconditionBase : MyRootedParentalBindableBase, ICloneable
    {
        public readonly Dictionary<ConditionBase.COMPARE, string> COMPARE_DIC = new Dictionary<ConditionBase.COMPARE, string>()
        {
            { ConditionBase.COMPARE.equal, "==" },
            { ConditionBase.COMPARE.not_equal, "!=" },
            { ConditionBase.COMPARE.greater, ">" },
            { ConditionBase.COMPARE.greater_or_equal, ">=" },
            { ConditionBase.COMPARE.lesser, "<" },
            { ConditionBase.COMPARE.lesser_or_equal, "<=" }
        };

        public new ConditionBase Root
        {
            get { return this.root as ConditionBase; }
        }
        public new VMcondition Parent
        {
            get { return this.parent as VMcondition; }
        }

        public DelegateCommand CommandRemove { get; private set; }
        public DelegateCommand CommandSetLHS { get; private set; }
        public DelegateCommand CommandSetRHS { get; private set; }

        public VMconditionBase(ConditionBase root, VMcondition parent) : base(root, parent)
        {
            this.root = root;
            this.parent = parent;

            this.CommandRemove = new DelegateCommand(() => this.Parent.Root.Conditions.Remove(this.Root));
            //this.CommandSetLHS = new DelegateCommand(() => this.LHS = this.Parent.Parent.SelectVar());
            //this.CommandSetRHS = new DelegateCommand(() => this.RHS = this.Parent.Parent.SelectVar());
        }

        public object Clone()
        {
            return new VMconditionBase(this.Root.Clone() as ConditionBase, this.Parent);
        }

        public string LHS
        {
            get { return this.Root.LHS; }
            set
            {
                this.Root.LHS = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Comparison
        {
            get { return this.COMPARE_DIC[this.Root.Comparison]; }
            set
            {
                this.Root.Comparison = (from entry in this.COMPARE_DIC
                                        where entry.Value == value
                                        select entry.Key).First();
                this.NotifyPropertyChanged();
            }
        }

        public string RHS
        {
            get { return this.Root.RHS; }
            set
            {
                this.Root.RHS = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool Invert
        {
            get { return this.Root.Invert; }
            set
            {
                this.Root.Invert = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Alias
        {
            get { return this.Root.Alias; }
            set
            {
                this.Root.Alias = value;
                this.NotifyPropertyChanged();
            }
        }

        public IList<string> Comparisons
        {
            get { return this.COMPARE_DIC.Values.ToList(); }
        }
    }
}
