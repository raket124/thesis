using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block
{
    class VariableLink : ICloneable
    {
        protected Variable value;
        public Variable Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        protected VariableLink child;
        public VariableLink Child
        {
            get { return this.child; }
            set { this.child = value; }
        }

        public VariableLink(Variable value) : this(value, null)
        {

        }

        public VariableLink(Variable value, VariableLink child)
        {
            this.value = value;
            this.child = child;
        }

        public object Clone()
        {
            return new VariableLink(this.Value.Clone() as Variable)
            {
                Child = this.Child == null ? null : this.Child.Clone() as VariableLink,
            };
        }

        public void AddLast(Variable value)
        {

        }

        public void RemoveLast()
        {
            var current = this;


        }

        public bool CanRemoveLast
        {
            get { return true; }
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }
    }
}
