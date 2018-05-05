using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    abstract class Mbase
    {
        protected string documentation;
        protected string name;
        //protected string content;

        public string Documentation
        {
            get { return this.documentation; }
            set { this.documentation = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        //public string Content
        //{
        //    get { return this.content; }
        //    set { this.content = value; }
        //}
    }
}
