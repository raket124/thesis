using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace master
{
    public class Test : INotifyPropertyChanged
    {
        private double _bindableDoubleValue;
        private string _selectedSubItem;

        public Test()
        {
            this.BindableDoubleValue = 10;
            for (int i = 0; i < 5; i++)
            {
                SubItemCollection.Add(new SubTest($"Sub item {i}"));
            }
        }

        public Test(int itemIndex) : this()
        {
            this.Index = itemIndex;
            this.Caption = $"Item {itemIndex}";
        }

        public int Index { get; set; }

        public string Caption { get; set; }

        public ObservableCollection<SubTest> SubItemCollection { get; set; } = new ObservableCollection<SubTest>();

        public string SelectedSubItem
        {
            get { return _selectedSubItem; }
            set
            {
                if (value == _selectedSubItem) return;
                _selectedSubItem = value;
                OnPropertyChanged();
            }
        }

        public double BindableDoubleValue
        {
            get { return _bindableDoubleValue; }
            set
            {
                if (value.Equals(_bindableDoubleValue)) return;
                _bindableDoubleValue = value;
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
