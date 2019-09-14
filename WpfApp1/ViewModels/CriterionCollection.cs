using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class CriterionCollection : ObservableCollection<CriterionViewModel>
    {
        public CriterionCollection()
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

            Properties = typeof(Person).GetProperties();
        }

        public IEnumerable<PropertyInfo> Properties { get; set; }

        public ICommand CloseCommand { get; }
        public IEnumerable<SimpleItem<ICommand>> Buttons { get; }

        private void ExecuteAddNumber(object obj)
        {
            var supportedTypes = new List<Type> { typeof(double), typeof(int), typeof(decimal), typeof(byte), typeof(float), typeof(short) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType))
                                    .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                                    .ToList();

            Add(new NumberCriterionViewModel(properties));
        }

        private void ExecuteAddText(object obj)
        {
            var supportedTypes = new List<Type> { typeof(string) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType))
                                    .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                                    .ToList();

            Add(new TextCriterionViewModel { Properties = properties });
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

            Add(new LookupCriterionViewModel(properties));
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
            Add(new OrCriterionViewModel(Properties));
        }

        private void ExecuteAddAnd(object obj)
        {
            Add(new AndCriterionViewModel(Properties));
        }

        private void ExecuteAddBool(object obj)
        {
            var supportedTypes = new List<Type> { typeof(bool) };

            var properties = Properties.Where(p => supportedTypes.Contains(p.PropertyType))
                                             .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                                             .ToList();

            Add(new BooleanCriterionViewModel(properties));
        }

        public new void Add(CriterionViewModel criterion)
        {
            criterion.CloseClicked += Criterion_CloseClicked;
            base.Add(criterion);
        }

        public new void Remove(CriterionViewModel criterion)
        {
            criterion.CloseClicked -= Criterion_CloseClicked;
            base.Remove(criterion);
        }

        private void Criterion_CloseClicked(object sender, EventArgs e)
        {
            Remove((CriterionViewModel)sender);
        }

    }
}
