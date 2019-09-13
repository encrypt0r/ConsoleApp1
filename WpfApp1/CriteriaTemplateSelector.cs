using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public class CriteriaTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BoolTemplate { get; set; }
        public DataTemplate CompareTemplate { get; set; }
        public DataTemplate GroupTemplate { get; set; }
        public DataTemplate LookupTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is BooleanCriterionViewModel)
                return BoolTemplate;

            if (item is NumberCriterionViewModel || item is TextCriterionViewModel)
                return CompareTemplate;

            if (item is OrCriterionViewModel || item is AndCriterionViewModel)
                return GroupTemplate;

            if (item is LookupCriterionViewModel)
                return LookupTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
