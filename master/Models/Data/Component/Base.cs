using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data.Component
{
    [DataContract]
    public abstract class Base : ObjectBase
    {
        public Base(string name) : base(name) { }

        public string ClassName
        {
            get { return this.ObjectName(); }
        }

        protected abstract string ObjectName();
    }
}
