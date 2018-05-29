using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    class Cfunction : Basis
    {
        public enum ACCESSIBILITY { Public, Private }

        protected ACCESSIBILITY access;
        protected List<object> users;
    }
}
