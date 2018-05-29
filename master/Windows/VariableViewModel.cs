using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Windows
{
    class VariableViewModel : INotifyPropertyChanged
    {
        Variable _variable;
        VariableViewModel _parent;
        ReadOnlyCollection<VariableViewModel> _children;

        public VariableViewModel(Variable variable) : this(variable, null) { }

        private VariableViewModel(Variable variable, VariableViewModel parent)
        {
            _variable = variable;
            _parent = parent;

            _children = new ReadOnlyCollection<VariableViewModel>(
                    (from child in _variable.Children
                     select new VariableViewModel(child, this))
                     .ToList<VariableViewModel>());
        }

        public ReadOnlyCollection<VariableViewModel> Children
        {
            get { return _children; }
        }

        public string Name
        {
            get { return _variable.Name; }
        }

        public VariableViewModel Parent
        {
            get { return _parent; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
