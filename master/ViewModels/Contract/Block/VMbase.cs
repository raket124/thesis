using master.Basis;
using master.Models;
using master.Models.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block
{
    abstract class VMbase: MyBindableBase, ICloneable
    {
        protected readonly string reqFormat = "Requirements: {0}";
        protected readonly string optFormat = "Optional: {0}";

        protected VMfunction parent;
        public VMfunction Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        protected Base root;
        public Base Root
        {
            get { return this.root; }
            //set { this.root = value; }
        }

        public VMbase(Base root)
        {
            this.root = root;
            this.parent = null;
        }

        public string Documentation
        {
            get { return this.root.Docs; }
        }

        public string Name
        {
            get { return this.BlockName(); }
        }

        public string RequiredParams
        {
            get { return this.Required(); }
        }

        public string OptionalParams
        {
            get { return this.Optional(); }
        }

        public abstract object Clone();

        protected abstract string BlockName();

        protected abstract string Required();

        protected abstract string Optional();

        public virtual Dictionary<Type, Dictionary<string, List<string>>> Aliases
        {
            get { return new Dictionary<Type, Dictionary<string, List<string>>>(); }
        }
    }
}
