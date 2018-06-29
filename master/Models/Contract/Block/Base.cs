using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block
{
    [DataContract]
    public abstract class Base : ObjectBase, ICloneable
    {
        public Base() : base() { }

        public abstract object Clone();

        public virtual IList<Variable> Aliases
        {
            get { return new List<Variable>(); }
        }
    }
}
