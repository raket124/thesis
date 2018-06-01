using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;

namespace master.Windows.Controls
{
    class ContractListBox : ListBox
    {
        public ObservableCollection<CloneTest> ClonableCollection1 { get; set; } = new ObservableCollection<CloneTest>();

        public ObservableCollection<CloneTest> ClonableCollection2 { get; set; } = new ObservableCollection<CloneTest>();


        public ContractListBox() : base()
        {
            this.Add(ClonableCollection1, 3);
            this.Add(ClonableCollection2, 10);
        }

        private void Add(ObservableCollection<CloneTest> oc, int count)
        {
            for (int i = 0; i < count; i++)
            {
                oc.Add(new CloneTest());
                //oc.Add(string.Format("Block {0:00}", i + 1));


                //< Grid Background = "#FDA07E" >
                //< StackPanel Orientation = "Horizontal" >
                //< Button Margin = "20 0 10 0"
                //VerticalAlignment = "Center"
                //Command = "{Binding ButtonTestCommand}"
                //Content = "{Binding Caption}" />
                //< RadioButton VerticalAlignment = "Center"
                //Content = "Option A"
                //IsChecked = "{Binding BindableOptionA}" />
                //< RadioButton VerticalAlignment = "Center"
                //Content = "Option B"
                //IsChecked = "{Binding BindableOptionB}" />
                //< TextBlock VerticalAlignment = "Center" Text = "{Binding BindableValue}" />
                //</ StackPanel >
                //</ Grid >
            }
        }
    }
}
