using GraphX.Controls;
using master.Models;
using Prism.Commands;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Drawing;
using master.Models.Data;
using master.Models.Data.Component.Components;
using System.ComponentModel;
using master.Models.Data.Component;

namespace master.Graphs
{
    class MyGraphArea : GraphArea<BaseVertex, DataEdge, BidirectionalGraph<BaseVertex, DataEdge>>
    {
        protected MyGraph graph;
        protected ZoomControl zoomControl;
        public ZoomControl ZoomControl
        {
            get { return this.zoomControl; }
            set { this.zoomControl = value; }
        }
        protected DataModel model;
        public DataModel Model
        {
            get { return this.model; }
            set
            {
                this.model = value;
                this.GenerateGraph();
            }
        }
        public new MyLogicCore LogicCore
        {
            get { return base.LogicCore as MyLogicCore; }
            set { base.LogicCore = value; }
        }

        protected readonly Dictionary<Type, bool> activeObjects;
        protected bool activeReferences;
        protected bool activeAbstractions;

        public ICommand CommandAsset { get; private set; }
        public ICommand CommandConcept { get; private set; }
        public ICommand CommandEnum { get; private set; }
        public ICommand CommandEvent { get; private set; }
        public ICommand CommandParticipant { get; private set; }
        public ICommand CommandTransaction { get; private set; }

        public ICommand CommandReference { get; private set; }
        public ICommand CommandAbstraction { get; private set; }

        public MyGraphArea() : base()
        {
            this.graph = new MyGraph();
            this.zoomControl = null;
            this.model = null;

            this.activeObjects = new Dictionary<Type, bool>()
            {
                {typeof(MyAsset), true },
                {typeof(MyConcept), true },
                {typeof(MyEnum), true },
                {typeof(MyEvent), true },
                {typeof(MyParticipant), true },
                {typeof(MyTransaction), true }
            };
            this.activeReferences = true;
            this.activeAbstractions = true;

            this.LogicCore = new MyLogicCore(this);

            this.CommandAsset = new DelegateCommand<bool?>(this.CommandObject<MyAsset>);
            this.CommandConcept = new DelegateCommand<bool?>(this.CommandObject<MyConcept>);
            this.CommandEnum = new DelegateCommand<bool?>(this.CommandObject<MyEnum>);
            this.CommandEvent = new DelegateCommand<bool?>(this.CommandObject<MyEvent>);
            this.CommandParticipant = new DelegateCommand<bool?>(this.CommandObject<MyParticipant>);
            this.CommandTransaction = new DelegateCommand<bool?>(this.CommandObject<MyTransaction>);

            this.CommandReference = new DelegateCommand<bool?>(this.CommandReferences);
            this.CommandAbstraction = new DelegateCommand<bool?>(this.CommandAbstractions);

            this.SetVerticesDrag(true, true);
        }

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
            this.AddComponents<MyAsset>(components);
            this.AddComponents<MyConcept>(components);
            this.AddComponents<MyEnum>(components);
            this.AddComponents<MyEvent>(components);
            this.AddComponents<MyParticipant>(components);
            this.AddComponents<MyTransaction>(components);

            this.graph.AddVertexRange(components.Values);

            this.AddEdges<MyAsset>(components);
            this.AddEdges<MyConcept>(components);
            //Exclude enums due to no possible links
            this.AddEdges<MyEvent>(components);
            this.AddEdges<MyParticipant>(components);
            this.AddEdges<MyTransaction>(components);

            this.GenerateGraph(this.graph);
            this.ZoomToFill();
        }

        private void AddComponents<T>(Dictionary<string, BaseVertex> output) where T : Base
        {
            if (this.activeObjects[typeof(T)])
                foreach (T c in this.model.GetComponent<T>())
                {
                    if (typeof(T) == typeof(MyEnum)) //Account for special case
                    {
                        output.Add(c.Name, new EnumVertex(c as MyEnum));
                        continue;
                    }
                    if (typeof(T).BaseType == typeof(Base))
                        output.Add(c.Name, new BaseVertex(c as Base));
                    if (typeof(T).BaseType == typeof(Inheritance))
                        output.Add(c.Name, new InheritanceVertex(c as Inheritance));
                    if (typeof(T).BaseType == typeof(Identity))
                        output.Add(c.Name, new IdentityVertex(c as Identity)); 
                }   
        }

        private void AddEdges<T>(Dictionary<string, BaseVertex> refs) where T : Inheritance
        {
            if (!this.activeObjects[typeof(T)] || (!activeReferences && !activeAbstractions))
                return;

            foreach (T c in this.model.GetComponent<T>())
            {
                if (activeAbstractions)
                    if (refs.TryGetValue(c.Parent, out _))
                        this.graph.AddEdge(new DataEdge(refs[c.Parent], refs[c.Name]) { Color = Brushes.Blue });

                if (activeReferences)
                    foreach (Variable var in c.Components)
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
