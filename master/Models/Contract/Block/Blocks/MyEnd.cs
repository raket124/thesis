using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyEnd : Base
    {
        public MyEnd() : base() { }

        public override object Clone()
        {
            return new MyEnd()
            {
                Name = this.Name,
                Docs = this.Docs
            };
        }
    }
}
