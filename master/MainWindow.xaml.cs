using master.FileReaders;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace master
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            test.Content = new VariableConfigStackPanel();
            test2.Content = new VariableConfigStackPanel();
            TestMyStuff();
        }

        public void TestMyStuff()
        {
            CTOreader r = new CTOreader();
            r.AddFile(@"C:\Users\T\Documents\Visual Studio 2017\Projects\master\master\ExampleFiles\sample.cto");
            r.ProcessFile(0);

        }
    }
}
