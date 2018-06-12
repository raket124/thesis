using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Basis : MyBindableBase
    {
        [DataMember]
        protected string docs;
        [DataMember]
        protected string name;

        public string Docs
        {
            get { return this.docs; }
            set
            {
                this.docs = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged();
            }
        }

        public Basis(string name)
        {
            this.name = name;
            this.docs = string.Empty;
        }
    }
}
