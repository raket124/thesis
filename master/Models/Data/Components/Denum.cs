using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Denum : Dbase
    {
        [DataMember]
        protected List<string> options;

        public List<string> Options
        {
            get { return this.options; }
        }

        public Denum(string name) : base(name)
        {
            this.options = new List<string>();
        }

        public void AddItem(string item)
        {
            this.options.Add(item);
        }
    }
}
