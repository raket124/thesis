using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    [DataContract]
    class Condition
    {
        [DataMember]
        protected ConditionGroup condition;
        public ConditionGroup Value
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        public Condition()
        {
            this.condition = new ConditionGroup();
        }
    }
}
