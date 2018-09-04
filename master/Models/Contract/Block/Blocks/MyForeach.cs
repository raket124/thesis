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
        protected VariableLink list;
        public VariableLink List
        {
            get { return this.list; }
            set { this.list = value; }
        }
        [DataMember]
        protected VariableLink objectAlias;
        public VariableLink ObjectAlias
        {
            get { return this.objectAlias; }
            set { this.objectAlias = value; }
        }
        [DataMember]
        protected VariableLink iteratorAlias;
        public VariableLink IteratorAlias
        {
            get { return this.iteratorAlias; }
            set { this.iteratorAlias = value; }
        }

        public MyForeach()
        {
            this.list = new VariableLink(new Block.MyVariable(typeof(Nullable)) { List = true });
            this.objectAlias = new VariableLink(new Block.MyVariable(typeof(Nullable)));
            this.iteratorAlias = new VariableLink(new Block.MyVariable(typeof(int)));
        }

        public override object Clone()
        {
            return new MyForeach()
            {
                List = this.List.Clone() as VariableLink,
                ObjectAlias = this.ObjectAlias.Clone() as VariableLink,
                IteratorAlias = this.IteratorAlias.Clone() as VariableLink
            };
        }
    }
}
