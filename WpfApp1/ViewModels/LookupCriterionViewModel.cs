using ConsoleApp1;
using System.Collections.Generic;

namespace WpfApp1.ViewModels
{
    public class LookupCriterionViewModel : CriterionViewModel
    {
        public LookupCriterionViewModel(IEnumerable<PropertyLookup> propertyLookups)
        {
            Properties = propertyLookups;
        }

        public string Reference { get; set; }
        public IEnumerable<PropertyLookup> Properties { get; }
        public PropertyLookup SelectedProperty { get; set; }

        public TextComparison Comparison { get; set; }
        public IEnumerable<SimpleItem<TextComparison>> Comparisons => new List<SimpleItem<TextComparison>>
        {
            new SimpleItem<TextComparison> { Text = "=", Value = TextComparison.Equal },
            new SimpleItem<TextComparison> { Text = "!=", Value = TextComparison.NotEqual },
        };

        public override Criterion Inflate() 
        {
            return new TextCriterion(Title, SelectedProperty.PropertyName, Reference, Comparison);
        }
    }
}
