using Computer_Serivce.Database;
using Computer_Serivce.Helpers;
using Computer_Serivce.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Computer_Serivce.ViewModel
{
    public class AddRepairViewModel
    {
        private string _brand;
        private string _model;
        private string _description;
        private string _repairedYear;
        private string _serialNumber;
        private string _yearMade;
        private DatabaseService dbService = new DatabaseService();

        public Action CloseAction { get; set; }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string RepairedYear
        {
            get => _repairedYear;
            set
            {
                _repairedYear = value;
                OnPropertyChanged(nameof(RepairedYear));
            }
        }

        public string SerialNumber
        {
            get => _serialNumber;
            set
            {
                _serialNumber = value;
                OnPropertyChanged(nameof(SerialNumber));
            }
        }

        public string YearMade
        {
            get => _yearMade;
            set
            {
                _yearMade = value;
                OnPropertyChanged(nameof(YearMade));
            }
        }

        public ICommand AddRepairCommand;

        public AddRepairViewModel()
        {
            AddRepairCommand = new DelegateCommand(AddRepair);
        }

        private void AddRepair()
        {
            Repair repair = new Repair
            {
                ServiceType = "",
                Description = Description,
                YearOfService = int.Parse(RepairedYear),
            };

            Computer computer = new Computer
            {
                Brand = Brand,
                Model = Model,
                SerialNumber = SerialNumber
            };

            dbService.AddRepair(repair, computer);

            // CloseAction?.Invoke();
        }

        public void ClearProps()
        {
            RepairedYear = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Description = string.Empty;
            SerialNumber = string.Empty;
            YearMade = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
