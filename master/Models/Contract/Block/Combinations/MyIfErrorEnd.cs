using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Combinations
{
    [DataContract]
    class MyIfErrorEnd : Base
    {
        protected MyIf ifBlock;
        protected MyError errorBlock;

        public MyIfErrorEnd() : base()
        {

        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
