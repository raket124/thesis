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
        protected Variable variable;
        public Variable Variable
        {
            get { return this.variable; }
            set { this.variable = value; }
        }
        [DataMember]
        protected Variable value;
        public Variable Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public MyAssign() : base()
        {
            this.variable = new Variable(typeof(Nullable));
            this.value = new Variable(typeof(Nullable));
        }

        public override object Clone()
        {
            return new MyAssign()
            {
                Name = this.Name,
                Docs = this.Docs,
                Variable = this.Variable.Clone() as Variable,
                Value = this.Value.Clone() as Variable
            };
        }
    }
}
