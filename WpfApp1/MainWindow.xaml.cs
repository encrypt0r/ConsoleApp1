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
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel = new MainWindowViewModel();
            _viewModel.Person = new Person
            {
                Name = "Muhammad",
                Age = 22,
                DepartmentId = "1",
                IsStudent = false
            };
        }

        private void RunCriteriaButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var criterion in _viewModel.Criteria)
            {
                var inflated = criterion.Inflate();
                var matches = inflated.Matches(_viewModel.Person) ? "✔" : "❌";
                logTextBox.Text += $"'{criterion.Title}' {matches}\n";
            }
        }
    }
}
