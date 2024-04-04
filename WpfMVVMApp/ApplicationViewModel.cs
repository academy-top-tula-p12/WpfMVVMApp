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

        ApplicationCommand addCommand;
        ApplicationCommand deleteCommand;
        ApplicationCommand openFileCommand;
        ApplicationCommand saveFileCommand;

        IAppDialogService appDialogService;
        IAppFileService appFileService;


        public Employee EmployeeSelected
        {
            get => employeeSelected;
            set
            {
                employeeSelected = value;
                OnPropertyChanged();
            }
        }

        public ApplicationViewModel(IAppDialogService appDialogService, IAppFileService appFileService)
        {
            this.appDialogService = appDialogService;
            this.appFileService = appFileService;

            Employees = new ObservableCollection<Employee>()
            {
                new() { Name = "Bobby", Age = 34, Salary = 58000M },
                new() { Name = "Sammy", Age = 21, Salary = 76000M },
                new() { Name = "Tommy", Age = 29, Salary = 49000M },
                new() { Name = "Jimmy", Age = 31, Salary = 64000M },
            };
        }

        public ApplicationCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new ApplicationCommand(obj =>
                    {
                        Employee employee = new();
                        Employees.Insert(0, employee);
                        EmployeeSelected = employee;
                    }));
            }
        }

        public ApplicationCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new ApplicationCommand(
                        obj =>
                        {
                            if (obj is not null && obj is Employee employee)
                                Employees.Remove(employee);
                        },
                        obj => Employees.Count > 0
                        ));
            }
        }

        public ApplicationCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ??
                    (openFileCommand = new ApplicationCommand(
                        obj =>
                        {
                            try
                            {
                               if(appDialogService.OpenFileDialog())
                                {
                                    var employees = appFileService.Open(appDialogService.FileName);
                                    Employees.Clear();
                                    foreach(var e in employees)
                                        Employees.Add(e);
                                    appDialogService.ShowMessage("Employees is loaded");
                                }
                            }
                            catch(Exception e)
                            {
                                appDialogService.ShowMessage(e.Message);
                            }
                        }
                        ));
            }
        }

        public ApplicationCommand SaveFileCommand
        {
            get
            {
                return saveFileCommand ??
                    (saveFileCommand = new ApplicationCommand(
                        obj =>
                        {
                            try
                            {
                                if(appDialogService.SaveFileDialog())
                                {
                                    appFileService.Save(appDialogService.FileName, Employees);
                                    appDialogService.ShowMessage("Employees saved to file");
                                }
                            }
                            catch (Exception e)
                            {
                                appDialogService.ShowMessage(e.Message);
                            }
                        },
                        obj => Employees.Count > 0
                        ));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
