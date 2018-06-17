using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    class Ccondition
    {
        protected CconditionGroup condition;

        public CconditionGroup Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        public override string ToString()
        {
            return this.condition.ToString();
        }
    }
}
