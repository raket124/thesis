using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    public class ObjectGroup
    {
        protected Type type;
        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        protected List<Objects> objects;
        public List<Objects> Objects
        {
            get { return this.objects; }
            set { this.objects = value; }
        }

        public ObjectGroup(Type type)
        {
            this.type = type;
            this.objects = new List<Objects>();
        }
    }
}
