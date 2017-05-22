using MvvmDemo.Models;
using MvvmDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmDemo.ViewModels
{
    class AddEditCustomerViewModel : BindableBase
    {
        private bool editMode;

        public bool EditMode
        {
            get { return editMode; }
            set { SetProperty(ref editMode, value); }
        }

        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { SetProperty(ref customer, value); }
        }

        public RelayCommand SaveCommand { get; set; }
        
        public RelayCommand CancelCommand { get; set; }

        public AddEditCustomerViewModel()
        {
            customer = new Customer();
            Customer.ErrorsChanged += Customer_ErrorsChanged;

            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel, () => true);

            Customer.ResetProperties();
        }

        private void Customer_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        void OnCancel()
        {

        }

        void OnSave()
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        bool CanSave()
        {
            return !Customer.HasErrors;
        }

    }
}
