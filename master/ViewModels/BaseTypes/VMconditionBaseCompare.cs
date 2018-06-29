using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static master.Models.Contract.Block.Conditioning.ConditionBase;

namespace master.ViewModels.BaseTypes
{
    class VMconditionBaseCompare : VMenum<COMPARE>
    {
        public VMconditionBaseCompare(COMPARE value) : base(value)
        {
            this.dictionary = new Dictionary<COMPARE, string>()
            {
                { COMPARE.equal, "==" },
                { COMPARE.not_equal, "!=" },
                { COMPARE.greater, ">" },
                { COMPARE.greater_or_equal, ">=" },
                { COMPARE.lesser, "<" },
                { COMPARE.lesser_or_equal, "<=" }
            };
        }
    }
}
