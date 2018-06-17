using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyUseRegistry : Base
    {
        public enum ACTION { Add, Update, Remove };

        [DataMember]
        protected ACTION action;
        [DataMember]
        protected string alias;

        public MyUseRegistry() : base()
        {

        }

        public override object Clone()
        {
            return new MyUseRegistry()
            {
                Name = this.Name,
                Docs = this.Docs,
                //TODO other vars
            };
        }
    }
}
