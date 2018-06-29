using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.BaseTypes
{
    class VMtype<T> : MyBindableBase
    {
        protected T value;
        public T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.NotifyPropertyChanged();
            }
        }

        public VMtype(T value)
        {
            this.value = value;
        }
    }
}
