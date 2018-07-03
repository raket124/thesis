using master.Models.Contract.Block.Conditioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MySimpleIf : Base
    {
        [DataMember]
        protected ConditionBase condition;
        public ConditionBase Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        public MySimpleIf() : base()
        {
            this.condition = new ConditionBase();
        }

        public override object Clone()
        {
            return new MySimpleIf()
            {
                Name = this.Name,
                Docs = this.Docs,
                Condition = this.Condition.Clone() as ConditionBase,
            };
        }
    }
}
