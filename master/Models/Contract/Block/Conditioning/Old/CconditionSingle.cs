using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    class CconditionSingle
    {
        public enum COMPARE { equal, not_equal, greater, greater_or_equal, lesser, lesser_or_equal }
        protected readonly Dictionary<COMPARE, string> COMPARE_DIC = new Dictionary<COMPARE, string>()
        {
            {COMPARE.equal, "==" },
            {COMPARE.not_equal, "!=" },
            {COMPARE.greater, ">" },
            {COMPARE.greater_or_equal, ">=" },
            {COMPARE.lesser, "<" },
            {COMPARE.lesser_or_equal, "<=" }
        };

        protected Cvariable objectA;
        protected COMPARE comparison;
        protected Cvariable objectB;

        public Cvariable A
        {
            get { return this.objectA; }
            set { this.objectA = value; }
        }

        public COMPARE Compare
        {
            get { return this.comparison; }
            set { this.comparison = value; }
        }

        public Cvariable B
        {
            get { return this.objectB; }
            set { this.objectB = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.objectA, this.COMPARE_DIC[this.comparison], this.objectB);
        }
    }
}
