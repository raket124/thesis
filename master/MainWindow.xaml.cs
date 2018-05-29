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

namespace master
{
    public partial class MainWindow : Window
    {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private const string defaultFileName = "Blockchain definition";

        private ObjectView objectView;
        private CodeView codeView;
        private TestView testView;
        private List<object> contractList;

        public MainWindow()
        {
            InitializeComponent();
            this.openFileDialog = new OpenFileDialog()
            {
                Filter = "Blockchain definition (*.bcd)|*.bcd"
            };
            this.saveFileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = ".bcd"
            };
            this.objectView = null;

            DataContext = new ContractListBox();

            //test.Content = new VariableConfigStackPanel();
            //test2.Content = new VariableConfigStackPanel();

            var a = new BlockChainObject();
            a.Set(BlockChainObject.OPTION.Add, BlockChainObject.OBJECT_CATEGORY.Asset, false, "com.biz", "Business", "movementDeparture.to");

            var b = a.ToCode();

            //Create a stream to serialize the object to.  
            //MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.  
            //var ser = new DataContractJsonSerializer(typeof(Mfile));
            //ser.WriteObject(ms, doc);
            //byte[] json = ms.ToArray();
            //ms.Close();
            //string x = Encoding.UTF8.GetString(json, 0, json.Length);

            //CTOreader r = new CTOreader();
            //r.AddFile(@"C:\Users\T\Documents\Visual Studio 2017\Projects\master\master\ExampleFiles\sample.cto");
            //r.ProcessFile(0);
        }

        private void FileOpenClick(object sender, RoutedEventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == true)
            {
                //Refresh UI
            }
        }

        private void FileSaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void FileSaveAsClick(object sender, RoutedEventArgs e)
        {

        }

        private void DataModelClick(object sender, RoutedEventArgs e)
        {
            if (this.objectView == null || !this.objectView.IsLoaded)
                this.objectView = new ObjectView();
            this.objectView.Show();
        }

        private void ChainCodeClick(object sender, RoutedEventArgs e)
        {
            if (this.codeView == null || !this.codeView.IsLoaded)
                this.codeView = new CodeView();
            this.codeView.Show();
        }

        private void TestWindowClick(object sender, RoutedEventArgs e)
        {
            if (this.testView == null || !this.testView.IsLoaded)
                this.testView = new TestView();
            this.testView.Show();
        }
        
        private void FileSave(string fileName)
        {

        }
    }
}
