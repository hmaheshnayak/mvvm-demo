using MvvmDemo.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;

namespace MvvmDemo.ViewModels
{
    class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
                return errors[propertyName];

            return null;
        }

        protected override void SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            base.SetProperty(ref property, value, propertyName);
            ValidateProperty<T>(propertyName, value);            
        }

        void ValidateProperty<T>(string propertyName, T value)
        {
            ICollection<System.ComponentModel.DataAnnotations.ValidationResult> validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            ValidationContext context = new ValidationContext(this);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(value, context, validationResults);


            if (validationResults.Any())
            {
                errors[propertyName] = validationResults.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                errors.Remove(propertyName);
            }

            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
