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

namespace master.Windows
{
    public partial class GraphWindow : Window
    {
        private Dmodel doc;
        private MyGraph graph;

        public GraphWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            this.doc = DmodelDemo.KoopmanCTO();
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

        private MyLogicCore GetCore()
        {
            var LogicCore = new MyLogicCore();
            LogicCore.SetDefaultLayoutAlgorithm((LayoutAlgorithmTypeEnum)this.ComboBoxLayout.SelectedItem);
            LogicCore.SetDefaultOverlapRemovalAlgorithm((OverlapRemovalAlgorithmTypeEnum)this.ComboBoxOverlapRemoval.SelectedItem);
            LogicCore.SetDefaultEdgeRoutingAlgorithm((EdgeRoutingAlgorithmTypeEnum)this.ComboBoxEdgeRouting.SelectedItem);
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
                {typeof(Dasset), this.CheckBoxAssets.IsChecked.GetValueOrDefault() },
                {typeof(Dconcept), this.CheckBoxConcepts.IsChecked.GetValueOrDefault() },
                {typeof(Denum), this.CheckBoxEnums.IsChecked.GetValueOrDefault() },
                {typeof(Devent), this.CheckBoxEvents.IsChecked.GetValueOrDefault() },
                {typeof(Dparticipant), this.CheckBoxParticipants.IsChecked.GetValueOrDefault() },
                {typeof(Dtransaction), this.CheckBoxTransactions.IsChecked.GetValueOrDefault() }
            };
        }

        private void BuildGraph()
        {
            this.graph.Clear();
            var activeComponents = this.GetActiveComponents();
            var activeReferences = this.CheckBoxReferences.IsChecked.GetValueOrDefault();
            var activeAbstractions = this.CheckBoxAbstractions.IsChecked.GetValueOrDefault();
            var components = new Dictionary<string, BaseVertex>();
            //var references = doc.GetReferenceTable(activeComponents);

            this.AddComponents<Dasset>(activeComponents, components);
            this.AddComponents<Dconcept>(activeComponents, components);
            //this.AddComponents<Menum>(activeComponents, components);
            this.AddComponents<Devent>(activeComponents, components);
            this.AddComponents<Dparticipant>(activeComponents, components);
            this.AddComponents<Dtransaction>(activeComponents, components);

            if (this.CheckBoxEnums.IsChecked == true)
                foreach (Denum a in doc.GetComponent<Denum>())
                    components.Add(a.Name, View(a.Name, a.Options));


            foreach (var gb in components.Values)
                this.graph.AddVertex(gb);

            this.AddEdges<Dasset>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Dconcept>(activeComponents, activeReferences, activeAbstractions, components);
            //this.AddEdges<Menum>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Devent>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Dparticipant>(activeComponents, activeReferences, activeAbstractions, components);
            this.AddEdges<Dtransaction>(activeComponents, activeReferences, activeAbstractions, components);

            graphArea.GenerateGraph(this.graph);
            zoomctrl.ZoomToFill();
        }
        private void AddComponents<T>(Dictionary<Type, bool> active, Dictionary<string, BaseVertex> output) where T : Dinheritance
        {
            if (active[typeof(T)])
                foreach (T c in doc.GetComponent<T>())
                    output.Add(c.Name, View(c.Name, c.Components));
        }
        private void AddEdges<T>(Dictionary<Type, bool> active, bool activeRefs, bool activeAbs, Dictionary<string, BaseVertex> refs) where T : Dinheritance
        {
            if (!active[typeof(T)] || (!activeRefs && !activeAbs))
                return;

            foreach (T c in doc.GetComponent<T>())
            {
                if (activeAbs)
                    if (refs.TryGetValue(c.Parent, out _))
                        this.graph.AddEdge(new DataEdge(refs[c.Parent], refs[c.Name]));

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

        private BaseVertex View(string name, List<Mvariable> vars)
        {
            var output = new BaseVertex() { Name = name };
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

        private BaseVertex View(string name, List<string> vars)
        {
            var output = new BaseVertex() { Name = name };
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
