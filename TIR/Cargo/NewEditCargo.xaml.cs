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

namespace TIR
{
    /// <summary>
    /// Interaction logic for NewEditCargo.xaml
    /// </summary>
    public partial class NewEditCargo : Window
    {
        public NewEditCargo()
        {
            InitializeComponent();
        }

        private void senderButton_Click(object sender, RoutedEventArgs e)
        {
            if (senderGrid.Visibility == Visibility.Hidden)
            {
                senderGrid.Visibility = Visibility.Visible;

                senderGrid.Height = Double.NaN;

            }
            else
            {
                senderGrid.Visibility = Visibility.Hidden;
                senderGrid.Height = 0;
            }
        }
        private void searchSenderButton(object sender, RoutedEventArgs e)
        {

        }
        private void recipientButton_Click(object sender, RoutedEventArgs e)
        {
            if (recipientGrid.Visibility == Visibility.Hidden)
            {
                recipientGrid.Visibility = Visibility.Visible;

                recipientGrid.Height = Double.NaN;

            }
            else
            {
                recipientGrid.Visibility = Visibility.Hidden;
                recipientGrid.Height = 0;
            }
        }
        private void searchRecipientButton(object sender, RoutedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SenderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RecipientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addCargo(object sender, RoutedEventArgs e)
        {

        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {

        }
    }
}
