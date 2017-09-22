using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceJewel.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EcommerceJewel.VariableTemplate
{
    public class VariableTiles : DataTemplateSelector
    {

        public DataTemplate NecklacesBigTemplate { get; set; }
        public DataTemplate NecklacesSmallTemplate { get; set; }
        public DataTemplate RingsBigTemplate { get; set; }
        public DataTemplate RingsSmallTemplate { get; set; }
        public DataTemplate BraceletsBigTemplate { get; set; }
        public DataTemplate BraceletsSmallTemplate { get; set; }
        public DataTemplate EarringsBigTemplate { get; set; }
        public DataTemplate EarringsSmallTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item.GetType() == typeof(JewelryDataItem))
            {
                    if ((item as JewelryDataItem).UniqueId.StartsWith("NecklacesBig"))
                        return NecklacesBigTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("NecklacesSmall"))
                        return NecklacesSmallTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("RingsBig"))
                        return RingsBigTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("RingsSmall"))
                        return RingsSmallTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("BraceletsBig"))
                        return BraceletsBigTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("BraceletsSmall"))
                        return BraceletsSmallTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("EarringsBig"))
                        return EarringsBigTemplate;
                    if ((item as JewelryDataItem).UniqueId.StartsWith("EarringsSmall"))
                        return EarringsSmallTemplate;
            }
            return base.SelectTemplateCore(item, container);
        }

    }
}
