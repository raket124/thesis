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

        public VariableLink(Variable value) : this(value, null) { }

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

        public void AddLast(Variable input)
        {
            var current = this;
            while (current != null)
                current = current.Child;
            current = new VariableLink(input);
        }

        public void RemoveLast()
        {
            var previous = this;
            var current = this.Child;
            while (current != null)
            {
                previous = current;
                current = current.Child;
            }
            previous.Child = null;
        }

        public bool CanRemoveLast
        {
            get { return this.child != null; }
        }

        public void Clear()
        {
            this.value = null;
            this.child = null;
        }

        public int Count
        {
            get
            {
                return 1 + (this.child == null ? 0 : this.child.Count);
            }
        }

        public IList<Variable> Listing
        {
            get
            {
                var output = new List<Variable>();
                var current = this.Child;
                while (current != null)
                {
                    output.Add(current.Value);
                    current = current.Child;
                }

                return output;
            }
        }
    }
}
