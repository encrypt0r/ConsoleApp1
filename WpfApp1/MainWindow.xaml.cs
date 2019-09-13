using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string DepartmentId { get; set; }
            public bool IsStudent { get; set; }
        }

        private CriterionViewModel _criterion;
        private readonly Person _person;

        public MainWindow()
        {
            InitializeComponent();

            personPanel.DataContext = _person = new Person
            {
                Name = "Muhammad",
                Age = 22,
                DepartmentId = "1",
                IsStudent = false
            };
        }

        private void RunCriteriaButton_Click(object sender, RoutedEventArgs e)
        {
            var inflated = _criterion.Inflate();
            var matches = inflated.Matches(_person) ? "✔" : "❌";
            logTextBox.Text += $"'{_criterion.Title}' {matches}\n";
        }

        private void BoolCriterionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var supportedTypes = new List<Type> { typeof(bool) };

            var properties = typeof(Person).GetProperties()
                .Where(p => supportedTypes.Contains(p.PropertyType))
                .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                .ToList();

            criteriaPane.Content = _criterion = new BooleanCriterionViewModel
            {
                Properties = properties
            };
        }

        private void NumebrCriterionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var supportedTypes = new List<Type> { typeof(double), typeof(int), typeof(decimal), typeof(byte), typeof(float), typeof(short) };

            var properties = typeof(Person).GetProperties()
                .Where(p => supportedTypes.Contains(p.PropertyType))
                .Select(p => new SimpleItem<string> { Text = p.Name, Value = p.Name })
                .ToList();

            criteriaPane.Content = _criterion = new NumberCriterionViewModel
            {
                Properties = properties
            };
        }

        private void AddCriterionButton_Click(object sender, RoutedEventArgs e)
        {
            addContextMenu.IsOpen = true;
        }

        private void OrCriterionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var properties = typeof(Person).GetProperties().ToList();
            criteriaPane.Content = _criterion = new OrCriterionViewModel { Properties = properties };
        }
    }
}
