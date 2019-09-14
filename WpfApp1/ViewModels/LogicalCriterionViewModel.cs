using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using ConsoleApp1;

namespace WpfApp1.ViewModels
{
    public abstract class LogicalCriterionViewModel : CriterionViewModel
    {
        public LogicalCriterionViewModel(string header, IEnumerable<PropertyInfo> properties)
        {
            Header = header;
            Properties = properties;
        }

        public CriterionCollection Criteria { get; } = new CriterionCollection();

        public IEnumerable<PropertyInfo> Properties { get; }
    }

    public class OrCriterionViewModel : LogicalCriterionViewModel
    {
        public OrCriterionViewModel(IEnumerable<PropertyInfo> properties) : base("Or Group", properties)
        {

        }

        public override Criterion Inflate()
        {
            var children = Criteria.Select(c => c.Inflate());
            return new OrCriterion(Title, children);
        }
    }

    public class AndCriterionViewModel : LogicalCriterionViewModel
    {
        public AndCriterionViewModel(IEnumerable<PropertyInfo> properties) : base("And Group", properties)
        {

        }

        public override Criterion Inflate()
        {
            var children = Criteria.Select(c => c.Inflate());
            return new AndCriterion(Title, children);
        }
    }
}
