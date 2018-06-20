using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using System.Collections.ObjectModel;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Conditioning;
using master.Utils;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMif : VMbase
    {
        //protected readonly Dictionary<COMPARE, string> COMPARE_DIC = new Dictionary<COMPARE, string>()
        //{
        //    {COMPARE.equal, "==" },
        //    {COMPARE.not_equal, "!=" },
        //    {COMPARE.greater, ">" },
        //    {COMPARE.greater_or_equal, ">=" },
        //    {COMPARE.lesser, "<" },
        //    {COMPARE.lesser_or_equal, "<=" }
        //};

        public new MyIf Root
        {
            get { return this.root as MyIf; }
        }

        public VMif(MyIf root) : base(root)
        {

        }

        public override object Clone()
        {
            return new VMif(this.root.Clone() as MyIf);
        }

        protected override string BlockName() { return "If - block"; }

        protected override string Required() { return string.Format(this.reqFormat, "1+ condition(s)"); }

        protected override string Optional() { return string.Empty; }

        public ObservableCollection<Condition> Conditions
        {
            get { return this.Root.Condition.Conditions; }
        }

        public ObservableCollection<ConditionGroup> Groups
        {
            get { return this.Root.Condition.Groups; }
        }

        public ConditionGroup Condition
        {
            get { return this.Root.Condition.Value; }
        }




        //public IList<ConditionBase.COMPARE> Comparisons
        //{
        //    get { return EnumUtil.EnumToList<ConditionBase.COMPARE>(); }
        //}

        public ObservableCollection<VMvariable> Vars
        {
            get { return new ObservableCollection<VMvariable>() { new VMvariable(null, null) }; }
            set
            {
                this.NotifyPropertyChanged();
            }
        }
    }
}
