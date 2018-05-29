using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.EdgeRouting;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using GraphX.PCL.Logic.Models;
using QuickGraph;

namespace master.Graphs
{
    public class MyLogicCore : GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>
    {
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
        }

        public void SetDefaultOverlapRemovalAlgorithm(OverlapRemovalAlgorithmTypeEnum algorithm)
        {
            this.DefaultOverlapRemovalAlgorithm = algorithm;
            if (this.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.FSA || this.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.OneWayFSA)
            {
                this.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 30;
                this.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 30;
            }
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
        }
    }
}
