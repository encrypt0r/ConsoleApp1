using System.Collections.Generic;
using System.Reflection;
using ConsoleApp1;

namespace WpfApp1.ViewModels
{
    public class BooleanCriterionViewModel : CriterionViewModel
    {
        public BooleanCriterionViewModel(IEnumerable<SimpleItem<string>> properties) 
        {
            Properties = properties;
        }

        public string Property { get; set; }
        public bool Reference { get; set; }

        public IEnumerable<SimpleItem<bool>> Lookup { get; } = new List<SimpleItem<bool>>
        {
            new SimpleItem<bool> { Text = "Yes", Value = true },
            new SimpleItem<bool> { Text = "No", Value = false },
        };
        public IEnumerable<SimpleItem<string>> Properties { get; }

        public override Criterion Inflate()
        {
            return new BooleanCriterion(Title, Property, Reference);
        }
    }
}
