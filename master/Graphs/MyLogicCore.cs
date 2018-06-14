using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.EdgeRouting;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using GraphX.PCL.Logic.Models;
using master.Utils;
using Prism.Commands;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace master.Graphs
{
    class MyLogicCore : GXLogicCore<BaseVertex, DataEdge, BidirectionalGraph<BaseVertex, DataEdge>>
    {
        private MyGraphArea parent;

        public MyLogicCore() : base()
        {
            this.parent = null;

            this.CommandLayout = new DelegateCommand<object>((value) => this.SetDefaultLayoutAlgorithm((LayoutAlgorithmTypeEnum)value));
            this.CommandOverlapRemoval = new DelegateCommand<object>((value) => this.SetDefaultOverlapRemovalAlgorithm((OverlapRemovalAlgorithmTypeEnum)value));
            this.CommandEdgeRouting = new DelegateCommand<object>((value) => this.SetDefaultEdgeRoutingAlgorithm((EdgeRoutingAlgorithmTypeEnum)value));
        }

        public MyGraphArea Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        private void NotifyParent()
        {
            if (this.parent != null)
                this.parent.GenerateGraph();
        }

        public ICommand CommandLayout { get; private set; }
        public ICommand CommandOverlapRemoval { get; private set; }
        public ICommand CommandEdgeRouting { get; private set; }

        public void SetDefaultLayoutAlgorithm(LayoutAlgorithmTypeEnum algorithm)
        {
            this.DefaultLayoutAlgorithm = algorithm;
            if (this.DefaultLayoutAlgorithm == LayoutAlgorithmTypeEnum.EfficientSugiyama)
            {
                var prms = this.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.EfficientSugiyama) as EfficientSugiyamaLayoutParameters;
                prms.EdgeRouting = SugiyamaEdgeRoutings.Orthogonal;
                prms.LayerDistance = prms.VertexDistance = 100;
                this.EdgeCurvingEnabled = false;
                this.DefaultLayoutAlgorithmParams = prms;
            }
            else
                this.EdgeCurvingEnabled = true;

            if (this.DefaultLayoutAlgorithm == LayoutAlgorithmTypeEnum.BoundedFR)
                this.DefaultLayoutAlgorithmParams = this.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.BoundedFR);
            if (this.DefaultLayoutAlgorithm == LayoutAlgorithmTypeEnum.FR)
                this.DefaultLayoutAlgorithmParams = this.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.FR);

            this.NotifyParent();
        }

        public void SetDefaultOverlapRemovalAlgorithm(OverlapRemovalAlgorithmTypeEnum algorithm)
        {
            this.DefaultOverlapRemovalAlgorithm = algorithm;
            if (this.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.FSA || this.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.OneWayFSA)
            {
                this.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 30;
                this.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 30;
            }

            this.NotifyParent();
        }

        public void SetDefaultEdgeRoutingAlgorithm(EdgeRoutingAlgorithmTypeEnum algorithm)
        {
            this.DefaultEdgeRoutingAlgorithm = algorithm;
            if (this.DefaultEdgeRoutingAlgorithm == EdgeRoutingAlgorithmTypeEnum.Bundling)
            {
                BundleEdgeRoutingParameters prm = new BundleEdgeRoutingParameters();
                this.DefaultEdgeRoutingAlgorithmParams = prm;
                prm.Iterations = 200;
                prm.SpringConstant = 5;
                prm.Threshold = .1f;
                this.EdgeCurvingEnabled = true;
            }
            else
                this.EdgeCurvingEnabled = false;

            this.NotifyParent();
        }

        public IList<LayoutAlgorithmTypeEnum> LayoutAlgorithms
        {
            get { return EnumUtil.EnumToList<LayoutAlgorithmTypeEnum>(); }
        }

        public IList<OverlapRemovalAlgorithmTypeEnum> OverlapRemovalAlgorithms
        {
            get { return EnumUtil.EnumToList<OverlapRemovalAlgorithmTypeEnum>(); }
        }

        public IList<EdgeRoutingAlgorithmTypeEnum> EdgeRoutingAlgorithms
        {
            get { return EnumUtil.EnumToList<EdgeRoutingAlgorithmTypeEnum>(); }
        }
    }
}
