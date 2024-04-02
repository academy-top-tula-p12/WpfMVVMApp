using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVMApp
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        Employee employeeSelected;
        public ObservableCollection<Employee> Employees { get; set; }

        public Employee EmployeeSelected
        {
            get => employeeSelected;
            set
            {
                employeeSelected = value;
                OnPropertyChanged();
            }
        }

        public ApplicationViewModel()
        {
            Employees = new ObservableCollection<Employee>()
            {
                new() { Name = "Bobby", Age = 34, Salary = 58000M },
                new() { Name = "Sammy", Age = 21, Salary = 76000M },
                new() { Name = "Tommy", Age = 29, Salary = 49000M },
                new() { Name = "Jimmy", Age = 31, Salary = 64000M },
            };
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
