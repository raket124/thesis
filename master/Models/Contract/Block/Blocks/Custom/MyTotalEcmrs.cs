using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks.Custom
{
    [DataContract]
    class MyTotalEcmrs : Base
    {
        [DataMember]
        protected string input;
        public string Input
        {
            get { return this.input; }
            set { this.input = value; }
        }

        public MyTotalEcmrs() : base()
        {
            this.input = string.Empty;
        }

        public override object Clone()
        {
            return new MyTotalEcmrs()
            {
                Name = this.Name,
                Docs = this.Docs,
                Input = this.Input
            };
        }
    }
}
