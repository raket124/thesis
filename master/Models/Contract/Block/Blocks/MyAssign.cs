using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyAssign : Base
    {
        [DataMember]
        protected VariableLink variable;
        public VariableLink Variable
        {
            get { return this.variable; }
            set { this.variable = value; }
        }
        [DataMember]
        protected VariableLink value;
        public VariableLink Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public MyAssign() : base()
        {
            this.variable = new VariableLink(new Block.MyVariable(typeof(Nullable)));
            this.value = new VariableLink(new Block.MyVariable(typeof(Nullable)));
        }

        public override object Clone()
        {
            return new MyAssign()
            {
                Name = this.Name,
                Docs = this.Docs,
                Variable = this.Variable.Clone() as VariableLink,
                Value = this.Value.Clone() as VariableLink
            };
        }
    }
}
