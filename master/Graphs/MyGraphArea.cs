using GraphX.Controls;
using QuickGraph;

namespace master.Graphs
{
    public class MyGraphArea : GraphArea<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>
    {
        public MyGraphArea() : base()
        {
            this.SetVerticesDrag(true, true);
        }
    }
}
