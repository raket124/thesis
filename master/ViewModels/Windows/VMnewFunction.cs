using master.Basis;
using master.Models.Contract;
using master.Utils;
using master.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Windows
{
    public class VMnewFunction : MyBindableBase
    {
        private Function function;
        public Function Function
        {
            get { return this.function; }
        }

        public string Name
        {
            get { return this.function.Name; }
            set
            {
                this.function.Name = value;
                this.NotifyPropertyChanged();
            }
        }
        public Function.ACCESSIBILITY Accessibility
        {
            get { return this.function.Access; }
            set
            {
                this.function.Access = value;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged("EnableUserSelect");
            }
        }

        private NewFunctionWindow parent;
        private List<string> existingNames;
        private List<string> participants;
        public List<string> Participants
        {
            get { return this.participants; }
        }

        public DelegateCommand CommandOk { get; private set; }
        public DelegateCommand<object> CommandTextChanged { get; private set; }

        public VMnewFunction(NewFunctionWindow parent, List<string> existingNames, List<string> participants)
        {
            this.function = new Function(string.Empty, Function.ACCESSIBILITY.Public);

            this.parent = parent;
            this.existingNames = existingNames;
            this.participants = participants;

            this.CommandOk = new DelegateCommand(this.Ok, this.CanOk);
            this.CommandTextChanged = new DelegateCommand<object>(this.TextChanged);
        }

        private void Ok()
        {
            this.parent.DialogResult = true;
        }

        private bool CanOk()
        {
            return this.Name != string.Empty && !this.existingNames.Contains(this.Name);
        }

        private void TextChanged(object text)
        {
            this.Name = text as string;
            CommandOk.RaiseCanExecuteChanged();
        }

        public IList<Function.ACCESSIBILITY> Accessibilities
        {
            get { return EnumUtil.EnumToList<Function.ACCESSIBILITY>(); }
        }

        public bool EnableUserSelect
        {
            get
            {
                switch (this.function.Access)
                {
                    case Function.ACCESSIBILITY.Public:
                    case Function.ACCESSIBILITY.Private:
                        return false;
                    case Function.ACCESSIBILITY.Controlled:
                        return true;
                    default:
                        throw new Exception("This should not happen.");
                }
            }
        }
    }
}

