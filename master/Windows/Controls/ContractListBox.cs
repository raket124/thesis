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
        public ObservableCollection<string> ClonableCollection1 { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> ClonableCollection2 { get; set; } = new ObservableCollection<string>();


        public ContractListBox() : base()
        {
            ClonableCollection1.Add("Block 01");
            ClonableCollection1.Add("Block 02");
            ClonableCollection1.Add("Block 03");

            ClonableCollection2.Add("Block 01");
            ClonableCollection2.Add("Block 02");
            ClonableCollection2.Add("Block 03");
            ClonableCollection2.Add("Block 04");
            ClonableCollection2.Add("Block 05");
            ClonableCollection2.Add("Block 06");
            ClonableCollection2.Add("Block 07");
            ClonableCollection2.Add("Block 08");
            ClonableCollection2.Add("Block 09");
            ClonableCollection2.Add("Block 10");
            ClonableCollection2.Add("Block 11");
        }
    }
}
