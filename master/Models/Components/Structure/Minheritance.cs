using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    abstract class Minheritance : Mbase
    {
        protected Minheritance parent;
        protected bool isAbstract;

        protected List<Object> properties;
    }
}
