using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    class ConditionBase
    {
        public enum COMPARE { equal, not_equal, greater, greater_or_equal, lesser, lesser_or_equal }

        protected object lhs;
        protected COMPARE comparison;
        protected object rhs;
    }
}
