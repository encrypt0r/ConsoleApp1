using System.Collections.Generic;
using ConsoleApp1;

namespace WpfApp1.ViewModels
{
    public class NumberCriterionViewModel : CriterionViewModel
    {
        public NumberCriterionViewModel(IEnumerable<SimpleItem<string>> properties)
        {
            Header = "Number";
            Properties = properties;
        }
        public string Property { get; set; }
        public double Reference { get; set; }
        public Comparison Comparison { get; set; }
        public IEnumerable<SimpleItem<Comparison>> Lookup { get; } = new List<SimpleItem<Comparison>>
        {
            new SimpleItem<Comparison> { Text = "=", Value = Comparison.Equal },
            new SimpleItem<Comparison> { Text = "!=", Value = Comparison.NotEqual },
            new SimpleItem<Comparison> { Text = ">", Value = Comparison.GreaterThan },
            new SimpleItem<Comparison> { Text = ">=", Value = Comparison.GreaterThanOrEqual },
            new SimpleItem<Comparison> { Text = "<", Value = Comparison.LessThan },
            new SimpleItem<Comparison> { Text = "<=", Value = Comparison.LessThanOrEqual },
        };

        public IEnumerable<SimpleItem<string>> Properties { get; }

        public override Criterion Inflate()
        {
            return new NumberCriterion(Title, Property, Reference, Comparison);
        }
    }
}
