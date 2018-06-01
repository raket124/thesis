using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Windows
{
    class VariableTreeViewModel
    {
        VariableViewModel _rootVariable;
        ReadOnlyCollection<VariableViewModel> _firstIteration;

        public ReadOnlyCollection<VariableViewModel> FirstIteration
        {
            get { return _firstIteration; }
        }

        public VariableTreeViewModel(Variable root)
        {
            _rootVariable = new VariableViewModel(root);
            _firstIteration = new ReadOnlyCollection<VariableViewModel>(new VariableViewModel[] { _rootVariable });
        }
    }
}
