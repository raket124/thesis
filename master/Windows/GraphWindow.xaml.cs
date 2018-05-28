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
            this.graphArea.LogicCore = this.GetCore();
            this.graphArea.GenerateGraph(graph);
            this.zoomctrl.ZoomToFill();
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

        private Dictionary<Type, bool> GetActiveComponents()
        {
            return new Dictionary<Type, bool>()
            {
                {typeof(Masset), this.CheckBoxAssets.IsChecked.GetValueOrDefault() },
                {typeof(Mconcept), this.CheckBoxConcepts.IsChecked.GetValueOrDefault() },
                {typeof(Menum), this.CheckBoxEnums.IsChecked.GetValueOrDefault() },
                {typeof(Mevent), this.CheckBoxEvents.IsChecked.GetValueOrDefault() },
                {typeof(Mparticipant), this.CheckBoxParticipants.IsChecked.GetValueOrDefault() },
                {typeof(Mtransaction), this.CheckBoxTransactions.IsChecked.GetValueOrDefault() }
            };
        }

        private void BuildGraph()
        {
            this.graph.Clear();
            var activeComponents = this.GetActiveComponents();
            var activeReferences = this.CheckBoxReferences.IsChecked.GetValueOrDefault();
            var activeAbstractions = this.CheckBoxAbstractions.IsChecked.GetValueOrDefault();
            var components = new Dictionary<string, DataVertex>();
            //var references = doc.GetReferenceTable(activeComponents);

            this.AddComponents<Masset>(activeComponents, components);
            this.AddComponents<Mconcept>(activeComponents, components);
            //this.AddComponents<Menum>(activeComponents, components);
            this.AddComponents<Mevent>(activeComponents, components);
            this.AddComponents<Mparticipant>(activeComponents, components);
            this.AddComponents<Mtransaction>(activeComponents, components);

            if (this.CheckBoxEnums.IsChecked == true)
                foreach (Menum a in doc.GetComponent<Menum>())
                    components.Add(a.Name, View(a.Name, a.Options));


            foreach (var gb in components.Values)
                this.graph.AddVertex(gb);

            this.AddEdges<Masset>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Mconcept>(activeComponents, activeReferences, activeAbstractions, components);
            //this.AddEdges<Menum>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Mevent>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Mparticipant>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Mtransaction>(activeComponents, activeReferences, activeAbstractions, components);

            graphArea.GenerateGraph(this.graph);
            zoomctrl.ZoomToFill();
        }
        private void AddComponents<T>(Dictionary<Type, bool> active, Dictionary<string, DataVertex> output) where T : Minheritance
        {
            if (active[typeof(T)])
                foreach (T c in doc.GetComponent<T>())
                    output.Add(c.Name, View(c.Name, c.Components));
        }
        private void AddEdges<T>(Dictionary<Type, bool> active, bool activeRefs, bool activeAbs, Dictionary<string, DataVertex> refs) where T : Minheritance
        {
            if (!active[typeof(T)] || (!activeRefs && !activeAbs))
                return;

            foreach (T c in doc.GetComponent<T>())
            {
                if (activeAbs)
                    if (refs.TryGetValue(c.Parent, out _))
                        this.graph.AddEdge(new DataEdge(refs[c.Parent], refs[c.Name]) { Text = string.Empty });

                if (activeRefs)
                    foreach (Mvariable var in c.Components)
                        if (refs.TryGetValue(var.Type, out _))
                            this.graph.AddEdge(new DataEdge(refs[var.Type], refs[c.Name]));
            }

            //foreach (Masset a in doc.getComponent<Masset>())
            //{
            //    if (references.TryGetValue(a.Parent, out Tuple<Mfile.TYPES, int> parent))
            //        this.graph.AddEdge(new DataEdge(components[a.Parent], components[a.Name]));
            //    foreach (Mvariable b in a.Components)
            //        if (references.TryGetValue(b.Type, out Tuple<Mfile.TYPES, int> output))
            //            this.graph.AddEdge(new DataEdge(components[b.Type], components[a.Name]));
            //}
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
