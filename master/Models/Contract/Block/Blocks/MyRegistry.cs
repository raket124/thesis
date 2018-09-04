using master.Models.Data.Component.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyRegistry : Base
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
        protected VariableLink variable;
        public VariableLink Variable
        {
            get { return this.variable; }
            set { this.variable = value; }
        }
        [DataMember]
        protected bool preventDelay;
        public bool Delay
        {
            get { return this.preventDelay; }
            set { this.preventDelay = value; }
        }

        public MyRegistry() : base()
        {
            this.action = ACTION.Insert;
            this.variable = new VariableLink(new MyVariable(typeof(MyAsset)));
            this.preventDelay = false;
        }

        public override object Clone()
        {
            return new MyRegistry()
            {
                Name = this.Name,
                Docs = this.Docs,
                Action = this.Action,
                Variable = this.Variable,
                Delay = this.Delay
            };
        }
    }
}
