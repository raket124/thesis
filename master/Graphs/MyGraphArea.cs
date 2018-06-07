using GraphX.Controls;
using master.Models;
using Prism.Commands;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Drawing;

namespace master.Graphs
{
    class MyGraphArea : GraphArea<BaseVertex, DataEdge, BidirectionalGraph<BaseVertex, DataEdge>>
    {
        private ZoomControl zoomControl;
        private MyGraph graph;
        private Dmodel model;

        private readonly Dictionary<Type, bool> activeObjects;
        private bool activeReferences;
        private bool activeAbstractions;

        public MyGraphArea() : base()
        {
            this.ZoomControl = null;
            this.graph = new MyGraph();
            this.model = null;

            this.activeObjects = new Dictionary<Type, bool>()
            {
                {typeof(Dasset), true },
                {typeof(Dconcept), true },
                {typeof(Denum), true },
                {typeof(Devent), true },
                {typeof(Dparticipant), true },
                {typeof(Dtransaction), true }
            };
            this.activeReferences = true;
            this.activeAbstractions = true;

            this.LogicCore = new MyLogicCore() { Parent = this };

            this.CommandAsset = new DelegateCommand<bool?>(this.CommandObject<Dasset>);
            this.CommandConcept = new DelegateCommand<bool?>(this.CommandObject<Dconcept>);
            this.CommandEnum = new DelegateCommand<bool?>(this.CommandObject<Denum>);
            this.CommandEvent = new DelegateCommand<bool?>(this.CommandObject<Devent>);
            this.CommandParticipant = new DelegateCommand<bool?>(this.CommandObject<Dparticipant>);
            this.CommandTransaction = new DelegateCommand<bool?>(this.CommandObject<Dtransaction>);

            this.CommandReference = new DelegateCommand<bool?>(this.CommandReferences);
            this.CommandAbstraction = new DelegateCommand<bool?>(this.CommandAbstractions);

            this.SetVerticesDrag(true, true);
        }

        public ZoomControl ZoomControl
        {
            get { return this.zoomControl; }
            set { this.zoomControl = value; }
        }
        public new MyLogicCore LogicCore
        {
            get { return base.LogicCore as MyLogicCore; }
            set { base.LogicCore = value; }
        }
        public Dmodel Model
        {
            get { return this.model; }
            set
            {
                this.model = value;
                this.GenerateGraph();
            }
        }

        public ICommand CommandAsset { get; private set; }
        public ICommand CommandConcept { get; private set; }
        public ICommand CommandEnum { get; private set; }
        public ICommand CommandEvent { get; private set; }
        public ICommand CommandParticipant { get; private set; }
        public ICommand CommandTransaction { get; private set; }

        public ICommand CommandReference { get; private set; }
        public ICommand CommandAbstraction { get; private set; }

        private void CommandObject<T>(bool? param)
        {
            this.activeObjects[typeof(T)] = param.GetValueOrDefault();
            this.GenerateGraph();
        }
        private void CommandReferences(bool? param)
        {
            this.activeReferences = param.GetValueOrDefault();
            this.GenerateGraph();
        }
        private void CommandAbstractions(bool? param)
        {
            this.activeAbstractions = param.GetValueOrDefault();
            this.GenerateGraph();
        }

        public void GenerateGraph()
        {
            if (this.model == null)
                return;

            this.graph.Clear();

            var components = new Dictionary<string, BaseVertex>();
            this.AddComponents<Dasset>(components);
            this.AddComponents<Dconcept>(components);
            this.AddComponents<Denum>(components);
            this.AddComponents<Devent>(components);
            this.AddComponents<Dparticipant>(components);
            this.AddComponents<Dtransaction>(components);

            this.graph.AddVertexRange(components.Values);

            this.AddEdges<Dasset>(components);
            this.AddEdges<Dconcept>(components);
            //Exclude enums due to no possible links
            this.AddEdges<Devent>(components);
            this.AddEdges<Dparticipant>(components);
            this.AddEdges<Dtransaction>(components);

            this.GenerateGraph(this.graph);
            this.ZoomToFill();
        }

        private void AddComponents<T>(Dictionary<string, BaseVertex> output) where T : Dbase
        {
            if (this.activeObjects[typeof(T)])
                foreach (T c in this.model.GetComponent<T>())
                {
                    if (typeof(T) == typeof(Denum)) //Account for special case
                    {
                        output.Add(c.Name, new EnumVertex(c as Denum));
                        continue;
                    }
                    if (typeof(T).BaseType == typeof(Dbase))
                        output.Add(c.Name, new BaseVertex(c as Dbase));
                    if (typeof(T).BaseType == typeof(Dinheritance))
                        output.Add(c.Name, new InheritanceVertex(c as Dinheritance));
                    if (typeof(T).BaseType == typeof(Didentity))
                        output.Add(c.Name, new IdentityVertex(c as Didentity)); 
                }   
        }

        private void AddEdges<T>(Dictionary<string, BaseVertex> refs) where T : Dinheritance
        {
            if (!this.activeObjects[typeof(T)] || (!activeReferences && !activeAbstractions))
                return;

            foreach (T c in this.model.GetComponent<T>())
            {
                if (activeAbstractions)
                    if (refs.TryGetValue(c.Parent, out _))
                        this.graph.AddEdge(new DataEdge(refs[c.Name], refs[c.Parent]) { Color = Brushes.Blue });

                if (activeReferences)
                    foreach (Mvariable var in c.Components)
                        if (refs.TryGetValue(var.Type, out _))
                            this.graph.AddEdge(new DataEdge(refs[var.Type], refs[c.Name]));
            }
        }


        private void ZoomToFill()
        {
            if (this.zoomControl != null)
                this.zoomControl.ZoomToFill();
        }
    }
}
