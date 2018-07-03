using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Conditioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Combinations
{
    [DataContract]
    class MySimpleIfError : Base
    {
        protected ConditionBase condition;
        public ConditionBase Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        public MySimpleIfError() : base()
        {

        }

        public override object Clone()
        {
            return new MySimpleIfError()
            {
                Name = this.Name,
                Docs = this.Docs,
                Condition = this.Condition.Clone() as ConditionBase,
            };
        }
    }
}
