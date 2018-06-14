using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Binput : Bbase
    {
        protected ObservableCollection<Bvariable> vars;
        public ObservableCollection<Bvariable> Vars
        {
            get { return this.vars; }
            set { this.vars = value; }
        }

        public Binput() : base()
        {
            this.vars = new ObservableCollection<Bvariable>();
        }
    }
}
