using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    class Mvariable<T>
    {
        protected string name;
        protected bool isRelation;
        protected bool isList;
        protected T value;
    }
}
