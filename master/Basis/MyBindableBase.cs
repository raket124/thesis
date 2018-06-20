using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Basis
{
    //DefaultDropHandler and MyBindableBase could/should be reversed
    abstract public class MyBindableBase : DefaultDropHandler, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void FullRefresh()
        {
            //Override with all PropertyChanged events
        }
    }
}
