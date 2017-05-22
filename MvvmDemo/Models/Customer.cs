using MvvmDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDemo.Models
{
    class Customer : ValidatableBindableBase
    {
        private Guid id;

        public Guid Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string firstName;

        [Required]
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        private string lastName;
        
        [Required]
        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private string email;

        [EmailAddress]
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string phone;

        [Phone]
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        public Customer()
        {
            firstName = string.Empty;
            lastName = string.Empty;
            email = string.Empty;
            phone = string.Empty;
        }

        public void ResetProperties()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
    }
}
