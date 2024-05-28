using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023242.WpfClient
{
    public class CustomerWindowViewModel : ObservableRecipient
    {
        public RestCollection<Customer> Customers { get; set; }

        private Customer selectedCustomer;

        public ICommand CreateCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != null)
                {
                    selectedCustomer = new Customer()
                    {
                        CustomerId = value.CustomerId,
                        CustomerName = value.CustomerName,
                        CustomerAge = value.CustomerAge,
                    };
                }
                OnPropertyChanged();
                (DeleteCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CustomerWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Customers = new RestCollection<Customer>("http://localhost:35357/", "Customer", "hub");

                CreateCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Add(new Customer()
                    {
                        CustomerName = SelectedCustomer.CustomerName,
                        CustomerAge = SelectedCustomer.CustomerAge,
                    });
                });
                UpdateCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Update(SelectedCustomer);
                });
                DeleteCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Delete(SelectedCustomer.CustomerId);
                },
                () => { return SelectedCustomer != null; });

                SelectedCustomer = new Customer() { CustomerName = "New Customer", CustomerAge = 20 };
            }
        }
    }
}
