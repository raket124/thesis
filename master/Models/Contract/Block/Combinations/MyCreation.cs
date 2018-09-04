using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Combinations
{
    [DataContract]
    class MyCreation : Base
    {
        [DataMember]
        protected MyAssign myObject;
        public MyAssign Object
        {
            get { return this.myObject; }
            set { this.myObject = value; }
        }
        [DataMember]
        protected MyModification modifications;
        public MyModification Modifications
        {
            get { return this.modifications; }
            set { this.modifications = value; }
        }

        public MyCreation() : base()
        {
            this.myObject = new MyAssign();
            this.modifications = new MyModification();
        }

        public override object Clone()
        {
            return new MyCreation()
            {
                Name = this.Name,
                Docs = this.Docs,
                Object = this.Object.Clone() as MyAssign,
                Modifications = this.Modifications.Clone() as MyModification
            };
        }
    }
}
