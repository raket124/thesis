using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyGetRegistry : Base
    {
        public enum CATEGORY { Participant, Asset };

        [DataMember]
        protected CATEGORY objectCategory;
        [DataMember]
        protected string objectName;
        [DataMember]
        protected string objectNameSpace;

        public MyGetRegistry(Function parent) : base(parent)
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
