using master.Basis;
using master.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Windows
{
    public class VMnewGroup : MyBindableBase
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged();
            }
        }

        private NewGroupWindow parent;
        private List<string> existingNames;

        public DelegateCommand CommandOk { get; private set; }
        public DelegateCommand<object> CommandTextChanged { get; private set; }

        public VMnewGroup(NewGroupWindow parent, List<string> existingNames)
        {
            this.name = string.Empty;
            this.parent = parent;
            this.existingNames = existingNames;

            this.CommandOk = new DelegateCommand(this.Ok, this.CanOk);
            this.CommandTextChanged = new DelegateCommand<object>(this.TextChanged);
        }

        private void Ok()
        {
            this.parent.DialogResult = true;
        }

        private bool CanOk()
        {
            return this.name != string.Empty && !this.existingNames.Contains(this.name);
        }

        private void TextChanged(object text)
        {
            this.name = text as string;
            CommandOk.RaiseCanExecuteChanged();
        }
    }
}
