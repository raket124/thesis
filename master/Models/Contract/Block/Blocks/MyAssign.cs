using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    public class MyAssign : Base
    {
        [DataMember]
        protected string variable;
        [DataMember]
        protected string value;

        public MyAssign(Function parent) : base(parent)
        {

        }

        public override object Clone()
        {
            return new MyAssign(this.parent)
            {
                Name = this.Name,
                Docs = this.Docs,
                //TODO other vars
            };
        }
    }
}
