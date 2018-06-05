using GraphX.Controls;
using QuickGraph;

namespace master.Graphs
{
    public class MyGraphArea : GraphArea<BaseVertex, DataEdge, BidirectionalGraph<BaseVertex, DataEdge>>
    {
        public MyGraphArea() : base()
        {
            this.SetVerticesDrag(true, true);
        }
    }
}
