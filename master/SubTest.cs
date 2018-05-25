using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace master
{
    class SubTest : INotifyPropertyChanged
    {
        private string _bindableValue;
        private bool _bindableOptionA;
        private bool _bindableOptionB;

        public SubTest(string caption)
        {
            this.Caption = caption;
            this.ButtonTestCommand = new SimpleCommand(o => { this.BindableValue = $"Button clicked at {DateTime.UtcNow.ToLocalTime()}"; });
        }

        public string Caption { get; set; }

        public ICommand ButtonTestCommand { get; set; }

        public string BindableValue
        {
            get { return _bindableValue; }
            set
            {
                if (value == _bindableValue) return;
                _bindableValue = value;
                OnPropertyChanged();
            }
        }

        public bool BindableOptionA
        {
            get { return _bindableOptionA; }
            set
            {
                if (value == _bindableOptionA) return;
                _bindableOptionA = value;
                OnPropertyChanged();
            }
        }

        public bool BindableOptionB
        {
            get { return _bindableOptionB; }
            set
            {
                if (value == _bindableOptionB) return;
                _bindableOptionB = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return this.Caption;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
