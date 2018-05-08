using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Menum : Mbase
    {
        [DataMember]
        protected List<string> options;

        public Menum(string name) : base(name)
        {
            this.options = new List<string>();
        }

        public void AddItem(string item)
        {
            this.options.Add(item);
        }
    }
}
