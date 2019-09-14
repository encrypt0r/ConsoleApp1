using WpfApp1.ViewModels;

namespace WpfApp1
{
    public class Person : BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { Set(ref _age, value); }
        }

        private string _departmentId;
        public string DepartmentId
        {
            get { return _departmentId; }
            set { Set(ref _departmentId, value); }
        }

        private bool _isStudent;
        public bool IsStudent
        {
            get { return _isStudent; }
            set { Set(ref _isStudent, value); }
        }

    }
}
