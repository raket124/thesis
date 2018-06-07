﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;

namespace master.Graphs
{
    class IdentityVertex : InheritanceVertex
    {
        protected new Didentity root;

        public IdentityVertex(Didentity root) : base(root)
        {
            this.root = root;
        }

        public string Id
        {
            get { return this.root.Identifier; }
        }
    }
}