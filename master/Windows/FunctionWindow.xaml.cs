using master.Models.Contract;
using master.Utils;
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

namespace master.Windows
{
    /// <summary>
    /// Interaction logic for FunctionWindow.xaml
    /// </summary>
    public partial class FunctionWindow : Window
    {
        private Function function;
        public Function Function
        {
            get { return this.function; }
        }


        public List<object> Items { get; set; }

        public FunctionWindow()
        {
            InitializeComponent();

            this.function = new Function(string.Empty, Function.ACCESSIBILITY.Public);


            Items = new List<object>
            {
                "A",
                "B",
                "C"
            };
            this.DataContext = this;
        }

        public IList<Function.ACCESSIBILITY> Accessibility
        {
            get { return EnumUtil.EnumToList<Function.ACCESSIBILITY>(); }
        }
    }
}
