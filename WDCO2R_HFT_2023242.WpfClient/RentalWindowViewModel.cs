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
    public class RentalWindowViewModel : ObservableRecipient
    {
        public RestCollection<Rental> Rentals { get; set; }

        private Rental selectedRental;

        public ICommand CreateRentalCommand { get; set; }
        public ICommand UpdateRentalCommand { get; set; }
        public ICommand DeleteRentalCommand { get; set; }

        public Rental SelectedRental
        {
            get { return selectedRental; }
            set
            {
                if (value != null)
                {
                    selectedRental = new Rental()
                    {
                        RentId = value.RentId,
                        Name = value.Name,
                        Price = value.Price,
                        CustomerId = value.CustomerId,
                        TimeLeft = value.TimeLeft,
                        BoardGame = value.BoardGame,
                        Customer = value.Customer,
                    };
                }
                OnPropertyChanged();
                (DeleteRentalCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateRentalCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public RentalWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Rentals = new RestCollection<Rental>("http://localhost:35357/", "Rental", "hub");

                CreateRentalCommand = new RelayCommand(() =>
                {
                    Rentals.Add(new Rental()
                    {
                        Name = SelectedRental.Name,
                        Price = SelectedRental.Price,
                        TimeLeft = SelectedRental.TimeLeft,
                        RentId = SelectedRental.RentId
                        ,
                    });
                });
                UpdateRentalCommand = new RelayCommand(() =>
                {
                    Rentals.Update(SelectedRental);
                });
                DeleteRentalCommand = new RelayCommand(() =>
                {
                    Rentals.Delete(SelectedRental.RentId);
                },
                () => { return SelectedRental != null; });

                SelectedRental = new Rental() { Name = "New Rental", TimeLeft = 2000, RentId = 100 };
            }
        }
    }
}
