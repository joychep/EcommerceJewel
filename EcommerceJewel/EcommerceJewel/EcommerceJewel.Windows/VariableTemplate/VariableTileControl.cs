using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceJewel.Data;
using Windows.UI.Xaml.Controls;

namespace EcommerceJewel.VariableTemplate
{
    public class VariableTileControl : GridView
    {
        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            if (item.GetType() == typeof(JewelryDataItem))
            {
                var viewModel = item as JewelryDataItem;
                element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, viewModel.ColSpan);
                element.SetValue(VariableSizedWrapGrid.RowSpanProperty, viewModel.RowSpan);
                base.PrepareContainerForItemOverride(element, item);
            }
        }
    }
}