using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block
{
    class VariableLink : ICloneable
    {
        protected MyVariable value;
        public MyVariable Value
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

        public VariableLink(MyVariable value) : this(value, null) { }

        public VariableLink(MyVariable value, VariableLink child)
        {
            this.value = value;
            this.child = child;
        }

        public object Clone()
        {
            return new VariableLink(this.Value.Clone() as MyVariable)
            {
                Child = this.Child == null ? null : this.Child.Clone() as VariableLink,
            };
        }

        public void AddLast(MyVariable input)
        {
            var current = this;
            while (current.Child != null)
                current = current.Child;
            current.Child = new VariableLink(input);
        }

        public void RemoveLast()
        {
            var previous = this;
            var current = this.Child;
            while (current.Child != null)
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

        public int Count
        {
            get { return 1 + (this.child == null ? 0 : this.child.Count); }
        }

        public void Clear()
        {
            this.value = null;
            this.child = null;
        }

        public IList<MyVariable> Listing
        {
            get
            {
                var output = new List<MyVariable>() { this.Value };
                var current = this.Child;
                while (current != null)
                {
                    output.Add(current.Value);
                    current = current.Child;
                }
                return output;
            }
        }

        public string Output
        {
            get
            {
                var listing = this.Listing;

                var aliases = (from l in listing
                               select l.Alias).ToList();
                if (listing.First().Input)
                    aliases.Insert(0, "input");


                return string.Join("->", aliases);
            }
        }
    }
}
