using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block.Conditioning;
using static master.Models.Contract.Block.Conditioning.ConditionGroup;

namespace master.ViewModels.BaseTypes
{
    class VMconditionGroupCompare : VMenum<COMPARE>
    {
        public VMconditionGroupCompare(COMPARE value) : base(value)
        {
            this.dictionary = new Dictionary<COMPARE, string>()
            {
                { ConditionGroup.COMPARE.and, "&&" },
                { ConditionGroup.COMPARE.or, "||" }
            };
        }
    }
}
