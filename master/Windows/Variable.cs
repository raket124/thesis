using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Windows
{
    class Variable
    {
        protected Variable _parent;
        protected List<Variable> _children;
        protected string _name;

        public Variable Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }

        public void AddChild(Variable child)
        {
            this._children.Add(child);
        }

        public IList<Variable> Children
        {
            get { return this._children; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public Variable()
        {
            this._parent = null;
            this._children = new List<Variable>();
            this._name = string.Empty;
        }
    }
}
