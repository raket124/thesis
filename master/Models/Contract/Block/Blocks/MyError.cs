using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyError : Base
    {
        [DataMember]
        protected string message;

        public MyError() : base()
        {

        }

        public override object Clone()
        {
            return new MyError()
            {
                Name = this.Name,
                Docs = this.Docs,
                //TODO other vars
            };
        }
    }
}
