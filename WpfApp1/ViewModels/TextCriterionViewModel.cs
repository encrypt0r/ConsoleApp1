using System.Collections.Generic;
using ConsoleApp1;

namespace WpfApp1.ViewModels
{
    public class TextCriterionViewModel : CriterionViewModel
    {
        public TextCriterionViewModel()
        {
            Header = "Text";
        }

        public string Property { get; set; }
        public string Reference { get; set; }
        public TextComparison Comparison { get; set; }
        public IEnumerable<SimpleItem<TextComparison>> Lookup { get; } = new List<SimpleItem<TextComparison>>
        {
            new SimpleItem<TextComparison> { Text = "=", Value = TextComparison.Equal },
            new SimpleItem<TextComparison> { Text = "!=", Value = TextComparison.NotEqual },
            new SimpleItem<TextComparison> { Text = "Contains", Value = TextComparison.Contains },
            new SimpleItem<TextComparison> { Text = "Starts with", Value = TextComparison.StartsWith },
            new SimpleItem<TextComparison> { Text = "Ends with", Value = TextComparison.EndsWith },
        };

        public IEnumerable<SimpleItem<string>> Properties { get; set; }

        public override Criterion Inflate()
        {
            return new TextCriterion(Title, Property, Reference, Comparison);
        }
    }
}
