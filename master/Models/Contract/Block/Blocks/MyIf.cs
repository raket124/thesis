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
    class MyIf : Base
    {
        protected Condition condition;
        public Condition Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        public MyIf(Function parent) : base(parent)
        {
            this.condition = new Condition();
            this.condition.Conditions.Add(new ConditionBase());
            this.condition.Groups.Add(new ConditionGroup());
        }

        public override object Clone()
        {
            return new MyIf(this.parent)
            {
                Name = this.Name,
                Docs = this.Docs,
                Condition = this.Condition,
                //TODO other vars
            };
        }
    }
}
