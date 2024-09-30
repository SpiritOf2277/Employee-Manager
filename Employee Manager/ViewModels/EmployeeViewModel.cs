using Employee_Manager.Database;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Employee_Manager.Views;
using Employee_Manager.Commands;

namespace Employee_Manager.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeRepository _repository;
        private ObservableCollection<Employee> _employees;
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set { _employees = value; OnPropertyChanged(nameof(Employees)); }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }

        // Команды
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public EmployeeViewModel()
        {
            _repository = new EmployeeRepository();
            LoadEmployees();

            AddCommand = new RelayCommand(AddEmployee);
            UpdateCommand = new RelayCommand(UpdateEmployee, CanModifyEmployee);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanModifyEmployee);
            RefreshCommand = new RelayCommand(LoadEmployees);
        }

        private void LoadEmployees()
        {
            Employees = _repository.GetAllEmployees();
        }

        private void AddEmployee(object parameter)
        {
            var employee = new Employee();
            var window = new EmployeeWindow(employee);
            if (window.ShowDialog() == true) {
                _repository.AddEmployee(employee);
                LoadEmployees();
            }
        }

        private void UpdateEmployee(object parameter)
        {
            var employee = SelectedEmployee;
            var window = new EmployeeWindow(employee);
            if (window.ShowDialog() == true) {
                _repository.UpdateEmployee(employee);
                LoadEmployees();
            }
        }

        private void DeleteEmployee(object parameter)
        {
            if (SelectedEmployee != null) {
                _repository.DeleteEmployee(SelectedEmployee.Id);
                LoadEmployees();
            }
        }

        private bool CanModifyEmployee(object parameter)
        {
            return SelectedEmployee != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
