using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyForeach : Base
    {
        [DataMember]
        protected string variable;
        public string Variable
        {
            get { return this.variable; }
            set { this.variable = value; }
        }
        [DataMember]
        protected string alias;
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }

        public MyForeach()
        {
            this.variable = string.Empty;
            this.alias = string.Empty;
        }

        public override object Clone()
        {
            return new MyForeach()
            {
                Variable = this.Variable,
                Alias = this.Alias
            };
        }
    }
}
