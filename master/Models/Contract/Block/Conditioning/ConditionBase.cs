using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    [DataContract]
    public class ConditionBase
    {
        public enum COMPARE { equal, not_equal, greater, greater_or_equal, lesser, lesser_or_equal }

        [DataMember]
        protected object lhs;
        public object LHS
        {
            get { return this.lhs; }
            set { this.lhs = value; }
        }
        [DataMember]
        protected COMPARE comparison;
        public COMPARE Comparison
        {
            get { return this.comparison; }
            set { this.comparison = value; }
        }
        [DataMember]
        protected object rhs;
        public object RHS
        {
            get { return this.rhs; }
            set { this.rhs = value; }
        }

        public ConditionBase()
        {
            this.lhs = null;
            this.comparison = COMPARE.equal;
            this.rhs = null;
        }
    }
}
