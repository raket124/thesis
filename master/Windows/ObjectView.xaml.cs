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
using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;

namespace master.Windows
{
    public partial class ObjectView : Window
    {
        private Mfile doc;
        private MyGraph graph;

        public ObjectView()
        {
            InitializeComponent();
            this.doc = Mfile.KoopmanCTO();


            Random Rand = new Random();

            //Create data graph object
            var graph = new MyGraph();

            //Create and add vertices using some DataSource for ID's
            for (int i = 0; i < 100; i++)
                graph.AddVertex(new DataVertex() { ID = i, Text = string.Format("<--{0}-->", i) });

            var vlist = graph.Vertices.ToList();
            //Generate random edges for the vertices
            foreach (var item in vlist)
            {
                if (Rand.Next(0, 50) > 25) continue;
                var vertex2 = vlist[Rand.Next(0, graph.VertexCount - 1)];
                graph.AddEdge(new DataEdge(item, vertex2, Rand.Next(1, 50))
                { Text = string.Format("{0} -> {1}", item, vertex2) });
            }


            var LogicCore = new MyLogicCore()
            {
                DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK,
                DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA,
                DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER,
                AsyncAlgorithmCompute = false
            };
            LogicCore.DefaultLayoutAlgorithmParams = LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            ((KKLayoutParameters)LogicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            LogicCore.DefaultOverlapRemovalAlgorithmParams = LogicCore.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

            graphArea.SetVerticesDrag(true, true);
            graphArea.SetEdgesDrag(true);


            graphArea.LogicCore = LogicCore;
            graphArea.GenerateGraph(graph);


        }

        private void InformationChecked(object sender, RoutedEventArgs e)
        {
            //if (this.graph != null)
            //    this.BuildGraph();
        }

        private void BuildGraph()
        {
            //this.graph.Clear();
            //var references = doc.GetReferenceTable();
            //var components = new Dictionary<string, GroupBox>();

            //if(this.CheckBoxAssets.IsChecked == true)
            //    foreach (Masset a in doc.getComponent<Masset>())
            //        components.Add(a.Name, View(a.Name, a.Components, Brushes.OrangeRed));

            ////foreach (Mconcept a in doc.getComponent<Mconcept>())
            ////    temp.Add(a.Name, View(a.Name, a.Components, Brushes.YellowGreen));
            ////foreach (Menum a in doc.getComponent<Menum>())
            ////    temp.Add(a.Name, View(a.Name, a.Options));
            ////foreach (Mevent a in doc.getComponent<Mevent>())
            ////    temp.Add(a.Name, View(a.Name, a.Components, Brushes.Gray));
            ////foreach (Mparticipant a in doc.getComponent<Mparticipant>())
            ////    temp.Add(a.Name, View(a.Name, a.Components, Brushes.BlueViolet));
            //this.graph.AddVertexRange(components.Values);
            ////graphLayout.Graph = this.graph;
            ////graphLayout.Relayout();
        }

        //private void Zzz()
        //{
        //    Mfile doc = Mfile.KoopmanCTO();
        //    var references = doc.GetReferenceTable();
        //    var temp = new Dictionary<string, GroupBox>();

        //    var X = new BidirectionalGraph<object, IEdge<object>>();

        //    //Add all
        //    foreach (Masset a in doc.getComponent<Masset>())
        //        temp.Add(a.Name, View(a.Name, a.Components, Brushes.OrangeRed));
        //    foreach (Mconcept a in doc.getComponent<Mconcept>())
        //        temp.Add(a.Name, View(a.Name, a.Components, Brushes.YellowGreen));
        //    foreach (Menum a in doc.getComponent<Menum>())
        //        temp.Add(a.Name, View(a.Name, a.Options));
        //    foreach (Mevent a in doc.getComponent<Mevent>())
        //        temp.Add(a.Name, View(a.Name, a.Components, Brushes.Gray));
        //    foreach (Mparticipant a in doc.getComponent<Mparticipant>())
        //        temp.Add(a.Name, View(a.Name, a.Components, Brushes.BlueViolet));

        //    X.AddVertexRange(temp.Values);

        //    //Add refs
        //    foreach (Masset a in doc.getComponent<Masset>())
        //    {
        //        if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
        //            X.AddEdge(new Edge<object>(temp[a.Parent], temp[a.Name]));
        //        foreach (Mvariable b in a.Components)
        //            if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
        //                X.AddEdge(new Edge<object>(temp[b.Type], temp[a.Name]));
        //    }

        //    foreach (Mconcept a in doc.getComponent<Mconcept>())
        //    {
        //        if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
        //            X.AddEdge(new Edge<object>(temp[a.Parent], temp[a.Name]));
        //        foreach (Mvariable b in a.Components)
        //            if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
        //                X.AddEdge(new Edge<object>(temp[b.Type], temp[a.Name]));
        //    }

        //    foreach (Mevent a in doc.getComponent<Mevent>())
        //    {
        //        if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
        //            X.AddEdge(new Edge<object>(temp[a.Parent], temp[a.Name]));
        //        foreach (Mvariable b in a.Components)
        //            if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
        //                X.AddEdge(new Edge<object>(temp[b.Type], temp[a.Name]));
        //    }

        //    foreach (Mparticipant a in doc.getComponent<Mparticipant>())
        //    {
        //        if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
        //            X.AddEdge(new Edge<object>(temp[a.Parent], temp[a.Name]));
        //        foreach (Mvariable b in a.Components)
        //            if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
        //                X.AddEdge(new Edge<object>(temp[b.Type], temp[a.Name]));
        //    }

        //    graphLayout.Graph = X;
        //}

        private GroupBox View(string name, List<string> vars)
        {
            var output = new GroupBox() { Header = name };
            var stack = new StackPanel();
            foreach (string item in vars)
            {
                string temp = string.Empty;
                temp += "  o ";
                temp += item;
                stack.Children.Add(new TextBlock() { Text = temp, ToolTip="hello" });
            }

            output.Content = stack;
            return output;
        }

        private GroupBox View(string name, List<Mvariable> vars, Brush color)
        {
            var output = new GroupBox() { Header = name, Background = color };
            var stack = new StackPanel();
            foreach (Mvariable item in vars)
            {
                string temp = string.Empty;
                temp += item.Relation == Mvariable.RELATION.variable ? "  o " : "--> ";
                temp += item.Type + " " + (item.List ? "[]" : string.Empty);
                temp += item.Name;
                stack.Children.Add(new TextBlock() { Text = temp });
            }

            output.Content = stack;
            return output;
        }
    }
}
