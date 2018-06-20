using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    public class MyLog : Base
    {
        [DataMember]
        protected string text;
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public MyLog(Function parent) : base(parent)
        {
            this.text = "The user object was updated with the following ID: #alias:persoon1";
        }

        public override object Clone()
        {
            return new MyLog(this.parent)
            {
                Name = this.Name,
                Docs = this.Docs,
                Text = this.Text
            };
        }
    }
}
