using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    public class MyUseRegistry : Base
    {
        public enum ACTION { Insert, Update, Remove };

        [DataMember]
        protected ACTION action;
        public ACTION Action
        {
            get { return this.action; }
            set { this.action = value; }
        }
        [DataMember]
        protected Variable variable;
        public Variable Variable
        {
            get { return this.variable; }
            set
            {
                if(value == null || (value.Type == Variable.TYPES.Asset || value.Type == Variable.TYPES.Participant))
                    this.variable = value;
            }
        }
        [DataMember]
        protected bool preventDelay;
        public bool Delay
        {
            get { return this.preventDelay; }
            set { this.preventDelay = value; }
        }

        public MyUseRegistry(Function parent) : base(parent)
        {
            this.action = ACTION.Insert;
            this.variable = null;
            this.preventDelay = false;
        }

        public override object Clone()
        {
            return new MyUseRegistry(this.parent)
            {
                Name = this.Name,
                Docs = this.Docs,
                Action = this.Action,
                Variable = this.Variable
            };
        }
    }
}
