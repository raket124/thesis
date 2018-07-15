using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data.Component.Components
{
    [DataContract]
    public class MyEnum : Base
    {
        [DataMember]
        protected List<string> options;
        public List<string> Options
        {
            get { return this.options; }
            set { this.options = value; }
        }

        public MyEnum(string name) : base(name)
        {
            this.options = new List<string>();
        }

        protected override string ObjectName()
        {
            return "Enum";
        }
    }
}
