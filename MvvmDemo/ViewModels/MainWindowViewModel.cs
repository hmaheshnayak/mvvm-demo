using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmDemo.ViewModels
{
    class RelayCommand<T> : ICommand
    {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object obj)
        {
            return canExecute == null || canExecute((T)obj);
        }

        public void Execute(object obj)
        {
            execute((T)obj);
        }
    }
    class RelayCommand : ICommand
    {
        private Action execute;
        private Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object obj)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object obj)
        {
            execute();
        }
    }

    class MainWindowViewModel : BindableBase
    {
        CustomerListViewModel customerViewModel;

        OrderViewModel orderViewModel;

        AddEditCustomerViewModel addCustomerViewModel;

        public AddEditCustomerViewModel AddCustomerViewModel
        {
            get
            {
                if (addCustomerViewModel == null)
                    addCustomerViewModel = new AddEditCustomerViewModel();
                return addCustomerViewModel;
            }
            set
            {
                addCustomerViewModel = value;
            }
        }

        private BindableBase currentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { SetProperty<BindableBase>(ref currentViewModel, value); }
        }

        public MainWindowViewModel()
        {
            customerViewModel = new CustomerListViewModel();
            orderViewModel = new OrderViewModel();
            //addCustomerViewModel = new AddEditCustomerViewModel();
            NavCommand = new RelayCommand<string>(OnNav, null);
        }

        public RelayCommand<string> NavCommand { get; set; }   
        
        void OnNav(string viewName)
        {
            switch (viewName)
            {
                case "orders":
                    CurrentViewModel = orderViewModel;
                    break;
                case "addcustomer":
                    CurrentViewModel = AddCustomerViewModel;
                    break;
                case "customers":
                default:
                    CurrentViewModel = customerViewModel;
                    break;
            }
        }
    }
}
