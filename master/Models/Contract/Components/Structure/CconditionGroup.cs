using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class CconditionGroup
    {
        public enum COMPARE { and, or }
        protected readonly Dictionary<COMPARE, string> COMPARE_DIC = new Dictionary<COMPARE, string>()
        {
            {COMPARE.and, "&&" },
            {COMPARE.or, "||" }
        };

        protected Dictionary<int, CconditionSingle> singles;
        protected Dictionary<int, CconditionGroup> groups;
        protected List<COMPARE> relations;

        public CconditionGroup()
        {
            this.singles = new Dictionary<int, CconditionSingle>();
            this.groups = new Dictionary<int, CconditionGroup>();
            this.relations = new List<COMPARE>();
        }

        public void Add(CconditionSingle input)
        {
            this.singles.Add(this.Count(), input);
        }

        public void Add(CconditionGroup input)
        {
            this.groups.Add(this.Count(), input);
        }

        public void Add(COMPARE input)
        {
            this.relations.Add(input);
        }

        public int Count()
        {
            return this.singles.Count + this.groups.Count;
        }

        public override string ToString()
        {
            var total = this.singles.Count + this.groups.Count;
            if (total - 1 != relations.Count)
                throw new Exception("ConditionGroup isn't valid");

            var output = string.Empty;
            for (int i = 0; i < total; i++)
            {
                bool _single = singles.TryGetValue(i, out CconditionSingle single);
                bool _group = groups.TryGetValue(i, out CconditionGroup group);
                if (!_single && !_group)
                    throw new Exception("ConditionGroup isn't valid");

                if (i == 0)
                {
                    if (_single)
                        output = string.Format("{0}", single);
                    if (_group)
                        output = string.Format("({0})", group);
                }
                else
                {
                    if (_single)
                        output = string.Format("{0} {1} {2}", output, this.COMPARE_DIC[this.relations[i - 1]], single);
                    if (_group)
                        output = string.Format("{0} {1} ({2})", output, this.COMPARE_DIC[this.relations[i - 1]], group);
                }
            }
            return output;
        }
    }
}
