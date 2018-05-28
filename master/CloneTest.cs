using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public class CloneTest : Test, ICloneable
    {
        public CloneTest()
        {
        }

        public CloneTest(int itemIndex) : base(itemIndex)
        {
        }

        public object Clone()
        {
            var clonableItemModel = new CloneTest();
            clonableItemModel.BindableDoubleValue = this.BindableDoubleValue;
            clonableItemModel.SubItemCollection.Clear();
            foreach (var subItem in this.SubItemCollection)
            {
                clonableItemModel.SubItemCollection.Add(subItem);
            }
            clonableItemModel.SelectedSubItem = this.SelectedSubItem;
            clonableItemModel.Index = this.Index;
            clonableItemModel.Caption = $"Cloned Item {this.Index}";
            return clonableItemModel;
        }
    }
}
