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
    class Cfunction : Basis
    {
        public enum ACCESSIBILITY { Public, Private, Controlled }

        protected ACCESSIBILITY access;
        protected ObservableCollection<Bbase> blocks;

        public Cfunction(string name, ACCESSIBILITY access) : base(name)
        {
            this.access = access;
            this.blocks = new ObservableCollection<Bbase>();
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

        public ObservableCollection<Bbase> Blocks
        {
            get { return this.blocks; }
            set { this.blocks = value; }
        }
    }
}
