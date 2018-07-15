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
        protected string variable;
        public string Variable
        {
            get { return this.variable; }
            set { this.variable = value; }
        }
        [DataMember]
        protected string value;
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public MyAssign() : base()
        {
            this.variable = string.Empty;
            this.value = string.Empty;
        }

        public override object Clone()
        {
            return new MyAssign()
            {
                Name = this.Name,
                Docs = this.Docs,
                Variable = this.Variable,
                Value = this.Value
            };
        }
    }
}
