using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    class DataObject
    {
        protected enum CLASS_TYPES { Asset, Participant, Transaction, Event, Concept};

        protected bool isAbstract;
        protected CLASS_TYPES type;
        protected string name;
        protected DataObject super;

        protected object[] variables;
    }
}
