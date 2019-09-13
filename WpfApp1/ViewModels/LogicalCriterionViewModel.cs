using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using ConsoleApp1;

namespace WpfApp1.ViewModels
{
    public abstract class LogicalCriterionViewModel : CriterionViewModel
    {
        public LogicalCriterionViewModel(string header)
        {
            Buttons = new List<SimpleItem<ICommand>>
            {
                new SimpleItem<ICommand> { Text = "Add boolean", Value = new DelegateCommand(ExecuteAddBool) },
                new SimpleItem<ICommand> { Text = "Add Number", Value = new DelegateCommand(ExecuteAddNumber) },
                new SimpleItem<ICommand> { Text = "Add Or", Value = new DelegateCommand(ExecuteAddOr) },
                new SimpleItem<ICommand> { Text = "Add And", Value = new DelegateCommand(ExecuteAddAnd) },
                new SimpleItem<ICommand> { Text = "Add Text", Value = new DelegateCommand(ExecuteAddText) },
                new SimpleItem<ICommand> { Text = "Add Lookup", Value = new DelegateCommand(ExecuteAddLookup) },
            };

            Header = header;
        }

        public List<SimpleItem<ICommand>> Buttons { get; }

        public List<PropertyInfo> Properties { get; set; }

        public ObservableCollection<CriterionViewModel> Items { get; } =
            new ObservableCollection<CriterionViewModel>();

        private void ExecuteAddNumber(object obj)
        {
            var supportedTypes = new List<Type> { typeof(double), typeof(int), typeof(decimal), typeof(byte), typeof(float), typeof(short) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType))
                                    .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                                    .ToList();

            AddCriterion(new NumberCriterionViewModel { Properties = properties });
        }

        private void ExecuteAddText(object obj)
        {
            var supportedTypes = new List<Type> { typeof(string) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType))
                                    .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                                    .ToList();

            AddCriterion(new TextCriterionViewModel { Properties = properties });
        }

        private void ExecuteAddLookup(object obj)
        {
            var supportedTypes = new List<Type> { typeof(string) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType) && p.Name.Length > 2 && p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase))
                                    .Select(p =>
                                    new PropertyLookup
                                    {
                                        PropertyDisplayName = p.Name.Substring(0, p.Name.Length - 2),
                                        PropertyName = p.Name,
                                        Lookup = GetLookupForProperty(p.Name)
                                    })
                                    .ToList();

            AddCriterion(new LookupCriterionViewModel(properties));
        }

        private IEnumerable<SimpleItem<string>> GetLookupForProperty(string property)
        {
            if (property == "DepartmentId")
            {
                return new List<SimpleItem<string>>
                {
                    new SimpleItem<string> { Text = "Concrete", Value = "1" },
                    new SimpleItem<string> { Text = "Soilworks", Value = "2" },
                    new SimpleItem<string> { Text = "Asphalt", Value = "3" },
                };
            }

            return new List<SimpleItem<string>>();
        }

        private void ExecuteAddOr(object obj)
        {
            AddCriterion(new OrCriterionViewModel { Properties = Properties });
        }

        private void ExecuteAddAnd(object obj)
        {
            AddCriterion(new AndCriterionViewModel { Properties = Properties });
        }

        private void ExecuteAddBool(object obj)
        {
            var supportedTypes = new List<Type> { typeof(bool) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType))
                                             .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                                             .ToList();

            AddCriterion(new BooleanCriterionViewModel { Properties = properties });
        }

        private void AddCriterion(CriterionViewModel criterion)
        {
            criterion.CloseClicked += Criterion_CloseClicked;
            Items.Add(criterion);
        }

        private void RemoveCriterion(CriterionViewModel criterion)
        {
            criterion.CloseClicked -= Criterion_CloseClicked;
            Items.Remove(criterion);
        }

        private void Criterion_CloseClicked(object sender, EventArgs e)
        {
            RemoveCriterion((CriterionViewModel)sender);
        }
    }

    public class OrCriterionViewModel : LogicalCriterionViewModel
    {
        public OrCriterionViewModel() : base("Or Group")
        {

        }

        public override Criterion Inflate()
        {
            var children = Items.Select(c => c.Inflate());
            return new OrCriterion(Title, children);
        }
    }

    public class AndCriterionViewModel : LogicalCriterionViewModel
    {
        public AndCriterionViewModel() : base("And Group")
        {

        }

        public override Criterion Inflate()
        {
            var children = Items.Select(c => c.Inflate());
            return new AndCriterion(Title, children);
        }
    }
}
