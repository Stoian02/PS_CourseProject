using Computer_Serivce.Helpers;
using Computer_Serivce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Computer_Serivce.View.Windows
{
    /// <summary>
    /// Interaction logic for AddRepairWindow.xaml
    /// </summary>
    public partial class AddRepairWindow : Window
    {
        public AddRepairWindow()
        {
            InitializeComponent();
        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddRepairButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddRepairViewModel viewModel)
            {
                if (viewModel.AddRepairCommand.CanExecute(null))
                {
                    viewModel.AddRepairCommand.Execute(null);
                    DialogResult = true;
                    Close();
                }
            }
        }
    }
}
