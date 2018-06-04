using master.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
using master.Windows;
using Microsoft.Win32;
using master.Windows.Controls;
using master.Blocks;
using master.Files;
using master.ViewModels;

namespace master
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var main = new VMmain(this);
            this.DataContext = main;
            this.Menu.DataContext = main.Menu;
            contractTreeView.DataContext = main.Model.Contracts;

            //test.Content = new VariableConfigStackPanel();
            //test2.Content = new VariableConfigStackPanel();

            //this.contractListBox.DataContext = main;
            //this.blockListBox.DataContext = main;
        }
    }
}
