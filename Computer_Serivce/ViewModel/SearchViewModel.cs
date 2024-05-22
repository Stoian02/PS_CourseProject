using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Computer_Serivce.Database;
using Computer_Serivce.Helpers;
using Computer_Serivce.Model;
using Computer_Serivce.View.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Computer_Serivce.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Computer> _computers;
        private ObservableCollection<Repair> _repairs;
        private string _repairedYear;
        private string _brand;
        private string _model;
        private DatabaseService dbService = new DatabaseService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SearchRepairsCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        public ObservableCollection<Computer> Computers
        {
            get => _computers;
            set
            {
                _computers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Repair> Repairs
        {
            get => _repairs;
            set
            {
                _repairs = value;
                OnPropertyChanged();
            }
        }

        public string RepairedYear
        {
            get => _repairedYear;
            set
            {
                _repairedYear = value;
                OnPropertyChanged();
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged();
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public SearchViewModel()
        {
            Computers = new ObservableCollection<Computer>();
            Repairs = new ObservableCollection<Repair>();
            SearchRepairsCommand = new DelegateCommand(SearchRepairs);
            ClearCommand = new DelegateCommand(ClearFields);
            LoadComputers();
            LoadRepairs();
        }

        private void LoadComputers()
        {
            try
            {
                var records = dbService.GetAllComputers();
                foreach (var record in records)
                {
                    Computers.Add(record);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void LoadRepairs()
        {
            try
            {
                var records = dbService.GetAllRepairsWIthComputers();
                foreach (var record in records)
                {
                    Repairs.Add(record);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void SearchComputers()
        {
            Computers = new ObservableCollection<Computer>(dbService.SearchComputers(_repairedYear, _brand, _model));
        }
        private void SearchRepairs()
        {
            // Old search
            // Repairs = new ObservableCollection<Repair>(dbService.SearchRepairs(_repairedYear, _brand, _model));
            // OnPropertyChanged(nameof(Repairs));

            var filters = new List<Expression<Func<Repair, bool>>>();
            int? searchYear = string.IsNullOrWhiteSpace(RepairedYear) ? null : int.Parse(RepairedYear);
            Expression<Func<Repair, int>> yearExpression = r => r.YearOfService;
            Func<IQueryable<Repair>, IIncludableQueryable<Repair, object>> include = query => query.Include(r => r.Computer);

            if (!string.IsNullOrWhiteSpace(Brand))
            {
                filters.Add(r => EF.Functions.Like(r.Computer.Brand, $"%{Brand}%"));
            }
            if (!string.IsNullOrWhiteSpace(Model))
            {
                filters.Add(r => EF.Functions.Like(r.Computer.Model, $"%{Model}%"));
            }


            var results = dbService.Search<Repair>(filters, yearExpression, searchYear, include);

            if (!results.Any())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var addRepairWindow = new AddRepairWindow();
                    if (addRepairWindow.ShowDialog() == true)
                    {
                        var viewModel = addRepairWindow.DataContext as AddRepairViewModel;

                        if (viewModel != null)
                        {
                            var newRepair = new Repair
                            {
                                YearOfService = int.Parse(viewModel.RepairedYear),
                                Computer = new Computer
                                {
                                    SerialNumber = viewModel.SerialNumber,
                                    Brand = viewModel.Brand,
                                    Model = viewModel.Model,
                                },
                                ServiceType = ""
                            };

                            LoadRepairs();
                        }
                    }
                });
            }
            else
            {
                Repairs = new ObservableCollection<Repair>(results);
                OnPropertyChanged(nameof(Repairs));
            }


        }

        private void ClearFields()
        {
            RepairedYear = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Repairs.Clear();
            LoadRepairs();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
