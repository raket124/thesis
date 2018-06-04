using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Cfunction : Basis
    {
        public enum ACCESSIBILITY { Public, Private, Controlled }

        protected ACCESSIBILITY access;
        protected List<Bbase> blocks;

        public Cfunction(string name, ACCESSIBILITY access) : base(name)
        {
            this.access = access;
            this.blocks = new List<Bbase>();
        }

        public void Add(Bbase block)
        {
            this.blocks.Add(block);
        }

        public ACCESSIBILITY Access
        {
            get { return this.access; }
            set { this.access = value; }
        }
    }
}
