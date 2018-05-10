using master.FileReaders;
using master.Models;
using QuickGraph;
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

namespace master
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;

        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get { return _graphToVisualize; }
        }

        public MainWindow()
        {
            TestMyStuff();

            InitializeComponent();
            //test.Content = new VariableConfigStackPanel();
            //test2.Content = new VariableConfigStackPanel();
            
        }

        public void TestMyStuff()
        {
            Mfile doc = Mfile.KoopmanCTO();


            //foreach (Masset a in doc.GetAssets())
            //{
            //    var g = new ListBoxItem()
            //    {
            //        Content = a.Name
            //    };
            //    LeftListBoxOfDragDropKeyStatesSample.Items.Add(g);
            //}

            var X = new BidirectionalGraph<object, IEdge<object>>();
            foreach (Masset a in doc.GetAssets())
            {
                X.AddVertex(a.Name);
            }
            _graphToVisualize = X;


            //var g = new BidirectionalGraph<object, IEdge<object>>();

            ////add the vertices to the graph
            //string[] vertices = new string[5];
            //for (int i = 0; i < 5; i++)
            //{
            //    vertices[i] = i.ToString();
            //    g.AddVertex(vertices[i]);
            //}

            //g.AddVertex(new Button()
            //{
            //    Content = "hello"
            //});

            ////add some edges to the graph
            //g.AddEdge(new Edge<object>(vertices[0], vertices[1]));
            //g.AddEdge(new Edge<object>(vertices[1], vertices[2]));
            //g.AddEdge(new Edge<object>(vertices[2], vertices[3]));
            //g.AddEdge(new Edge<object>(vertices[3], vertices[1]));
            //g.AddEdge(new Edge<object>(vertices[1], vertices[4]));

            



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
    }
}
