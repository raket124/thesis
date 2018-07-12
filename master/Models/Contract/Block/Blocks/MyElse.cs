using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyElse : Base
    {
        public MyElse() : base()
        {

        }

        public override object Clone()
        {
            return new MyElse();
        }
    }
}
