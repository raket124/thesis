using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using master.Models;
using master.Graphs;
using GraphX.PCL.Common.Enums;
using master.Models.Data;

namespace master.Windows
{
    public partial class GraphWindow : Window
    {
        public GraphWindow()
        {
            InitializeComponent();
            this.graphArea.ZoomControl = this.zoomctrl;
            this.graphArea.Model = DataModelDemo.Example3();
            this.DataContext = this.graphArea;
        }
    }
}
