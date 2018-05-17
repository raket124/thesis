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
using master.Util;
using GraphX.PCL.Logic.Algorithms.EdgeRouting;

namespace master.Windows
{
    public partial class ObjectView : Window
    {
        private Mfile doc;
        private MyGraph graph;

        public ObjectView()
        {
            InitializeComponent();
            InitializeComboBoxes();
            this.doc = Mfile.KoopmanCTO();
            this.graph = new MyGraph();

            graphArea.SetVerticesDrag(true, true);
            graphArea.SetEdgesDrag(true);
            graphArea.LogicCore = this.GetCore();

            this.BuildGraph();
        }

        private void InitializeComboBoxes()
        {
            this.InitializeComboBox<LayoutAlgorithmTypeEnum>(this.ComboBoxLayout, 7);
            this.InitializeComboBox<OverlapRemovalAlgorithmTypeEnum>(this.ComboBoxOverlapRemoval, 0);
            this.InitializeComboBox<EdgeRoutingAlgorithmTypeEnum>(this.ComboBoxEdgeRouting, 2);
        }
        private void InitializeComboBox<T>(ComboBox cb, int index)
        {
            cb.ItemsSource = Enum.GetValues(typeof(T));
            cb.SelectedIndex = index;
            cb.SelectionChanged += ComboBoxGraph_SelectionChanged;
        }
        private void ComboBoxGraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            graphArea.LogicCore = this.GetCore();
            graphArea.GenerateGraph(graph);
        }

        private void HandleLayout(MyLogicCore core)
        {
            var late = (LayoutAlgorithmTypeEnum)this.ComboBoxLayout.SelectedItem;
            if (late == LayoutAlgorithmTypeEnum.Sugiyama && this.graph.Edges.Count() == 0)
            {
                MessageBox.Show("The 'Sugiyama' algorithm requires atleast one connection.\nDefaulting to 'EfficientSugiyama'");
                this.ComboBoxLayout.SelectedItem = LayoutAlgorithmTypeEnum.EfficientSugiyama;
                return;
            }
            core.DefaultLayoutAlgorithm = late;

            if (late == LayoutAlgorithmTypeEnum.EfficientSugiyama)
            {
                var prms = core.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.EfficientSugiyama) as EfficientSugiyamaLayoutParameters;
                prms.EdgeRouting = SugiyamaEdgeRoutings.Orthogonal;
                prms.LayerDistance = prms.VertexDistance = 100;
                core.EdgeCurvingEnabled = false;
                core.DefaultLayoutAlgorithmParams = prms;
                this.ComboBoxEdgeRouting.SelectedItem = EdgeRoutingAlgorithmTypeEnum.None;
            }
            else
                core.EdgeCurvingEnabled = true;
                
            if (late == LayoutAlgorithmTypeEnum.BoundedFR)
                core.DefaultLayoutAlgorithmParams = core.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.BoundedFR);
            if (late == LayoutAlgorithmTypeEnum.FR)
                core.DefaultLayoutAlgorithmParams = core.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.FR);
        }
        private void HandleOverlapRemoval(MyLogicCore core)
        {
            core.DefaultOverlapRemovalAlgorithm = (OverlapRemovalAlgorithmTypeEnum)this.ComboBoxOverlapRemoval.SelectedItem;
            if (core.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.FSA || core.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.OneWayFSA)
            {
                core.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 30;
                core.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 30;
            }
        }
        private void HandleEdgeRouting(MyLogicCore core)
        {
            core.DefaultEdgeRoutingAlgorithm = (EdgeRoutingAlgorithmTypeEnum)this.ComboBoxEdgeRouting.SelectedItem;
            if (core.DefaultEdgeRoutingAlgorithm == EdgeRoutingAlgorithmTypeEnum.Bundling)
            {
                BundleEdgeRoutingParameters prm = new BundleEdgeRoutingParameters();
                core.DefaultEdgeRoutingAlgorithmParams = prm;
                prm.Iterations = 200;
                prm.SpringConstant = 5;
                prm.Threshold = .1f;
                core.EdgeCurvingEnabled = true;
            }
            else
                core.EdgeCurvingEnabled = false;
        }
        private MyLogicCore GetCore()
        {
            var LogicCore = new MyLogicCore();
            this.HandleLayout(LogicCore);
            this.HandleOverlapRemoval(LogicCore);
            this.HandleEdgeRouting(LogicCore);
            return LogicCore;
        }

        private void Information_Checked(object sender, RoutedEventArgs e)
        {
            if (this.graph != null)
                this.BuildGraph();
        }

        private void BuildGraph()
        {
            this.graph.Clear();
            var references = doc.GetReferenceTable();
            var components = new Dictionary<string, DataVertex>();

            if (this.CheckBoxAssets.IsChecked == true)
                foreach (Masset a in doc.getComponent<Masset>())
                    components.Add(a.Name, View(a.Name, a.Components));
            if (this.CheckBoxConcepts.IsChecked == true)
                foreach (Mconcept a in doc.getComponent<Mconcept>())
                    components.Add(a.Name, View(a.Name, a.Components));
            if (this.CheckBoxEnums.IsChecked == true)
                foreach (Menum a in doc.getComponent<Menum>())
                    components.Add(a.Name, View(a.Name, a.Options));
            if (this.CheckBoxEvents.IsChecked == true)
                foreach (Mevent a in doc.getComponent<Mevent>())
                    components.Add(a.Name, View(a.Name, a.Components));
            if (this.CheckBoxParticipants.IsChecked == true)
                foreach (Mparticipant a in doc.getComponent<Mparticipant>())
                    components.Add(a.Name, View(a.Name, a.Components));
            if (this.CheckBoxTransactions.IsChecked == true)
                foreach (Mtransaction a in doc.getComponent<Mtransaction>())
                    components.Add(a.Name, View(a.Name, a.Components));

            foreach (var gb in components.Values)
                this.graph.AddVertex(gb);


            foreach (Masset a in doc.getComponent<Masset>())
            {
                if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
                    this.graph.AddEdge(new DataEdge(components[a.Parent], components[a.Name]));
                foreach (Mvariable b in a.Components)
                    if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
                        this.graph.AddEdge(new DataEdge(components[b.Type], components[a.Name]));
            }
            foreach (Mconcept a in doc.getComponent<Mconcept>())
            {
                if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
                    this.graph.AddEdge(new DataEdge(components[a.Parent], components[a.Name]));
                foreach (Mvariable b in a.Components)
                    if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
                        this.graph.AddEdge(new DataEdge(components[b.Type], components[a.Name]));
            }
            foreach (Mevent a in doc.getComponent<Mevent>())
            {
                if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
                    this.graph.AddEdge(new DataEdge(components[a.Parent], components[a.Name]));
                foreach (Mvariable b in a.Components)
                    if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
                        this.graph.AddEdge(new DataEdge(components[b.Type], components[a.Name]));
            }
            foreach (Mparticipant a in doc.getComponent<Mparticipant>())
            {
                if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
                    this.graph.AddEdge(new DataEdge(components[a.Parent], components[a.Name]));
                foreach (Mvariable b in a.Components)
                    if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
                        this.graph.AddEdge(new DataEdge(components[b.Type], components[a.Name]));
            }

            graphArea.GenerateGraph(this.graph);
        }

        private DataVertex View(string name, List<Mvariable> vars)
        {
            var output = new DataVertex() { Name = name };
            foreach (Mvariable item in vars)
            {
                string temp = string.Empty;
                temp += item.Relation == Mvariable.RELATION.variable ? "  o " : "--> ";
                temp += item.Type + " " + (item.List ? "[]" : string.Empty);
                temp += item.Name;
                output.Vars.Add(temp);
            }
            return output;
        }

        private DataVertex View(string name, List<string> vars)
        {
            var output = new DataVertex() { Name = name };
            foreach (string item in vars)
            {
                string temp = string.Empty;
                temp += "  o ";
                temp += item;
                output.Vars.Add(temp);
            }
            return output;
        }
    }
}
