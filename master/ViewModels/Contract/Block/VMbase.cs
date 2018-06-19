﻿using master.Basis;
using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block
{
    abstract class VMbase: MyBindableBase, ICloneable
    {
        protected VMfunction parent;
        public VMfunction Parent
        {
            get { return this.parent; }
        }
        protected ObjectBase root;
        public ObjectBase Root
        {
            get { return this.root; }
        }

        public VMbase(ObjectBase root)
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

        public abstract object Clone();

        protected abstract string BlockName();
    }
}
