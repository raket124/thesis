using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class Cfunction : Basis
    {
        public enum ACCESSIBILITY { Public, Private, Controlled }

        protected ACCESSIBILITY access;
        protected List<object> users;

        public Cfunction(string name, ACCESSIBILITY access) : base(name)
        {
            this.access = access;
        }

        public ACCESSIBILITY Access
        {
            get { return this.access; }
            set { this.access = value; }
        }
    }
}
