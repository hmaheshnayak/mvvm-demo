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
    class MainWindowViewModel : BindableBase
    {
        CustomerListViewModel customerViewModel;

        OrderViewModel orderViewModel;

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
                case "customers":
                default:
                    CurrentViewModel = customerViewModel;
                    break;
            }
        }
    }
}
