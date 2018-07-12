using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    [DataContract]
    class ConditionBase : ICloneable
    {
        public enum COMPARE { equal, not_equal, greater, greater_or_equal, lesser, lesser_or_equal }

        [DataMember]
        protected string lhs;
        public string LHS
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
        protected string rhs;
        public string RHS
        {
            get { return this.rhs; }
            set { this.rhs = value; }
        }
        [DataMember]
        protected string alias;
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }
        [DataMember]
        protected bool invert;
        public bool Invert
        {
            get { return this.invert; }
            set { this.invert = value; }
        }

        public ConditionBase()
        {
            this.lhs = string.Empty;
            this.comparison = COMPARE.equal;
            this.rhs = string.Empty;
            this.alias = string.Empty;
            this.invert = false;
        }

        public object Clone()
        {
            return new ConditionBase()
            {
                LHS = this.LHS,
                Comparison = this.Comparison,
                RHS = this.RHS,
                Alias = this.Alias,
                Invert = this.Invert
            };
        }
    }
}
