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
        protected string alias;
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
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
            this.alias = string.Empty;
            this.preventDelay = false;
        }

        public override object Clone()
        {
            return new MyRegistry()
            {
                Name = this.Name,
                Docs = this.Docs,
                Action = this.Action,
                Alias = this.Alias
            };
        }
    }
}
