using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    class ObjectGroup
    {
        protected Type type;
        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        protected List<ObjectValue> objects;
        public List<ObjectValue> Objects
        {
            get { return this.objects; }
            set { this.objects = value; }
        }

        public ObjectGroup()
        {
            this.type = null;
            this.objects = new List<ObjectValue>();
        }
    }
}
