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
        public bool isEdit;
        public NewEditCargo(bool isEdit)
        {

            InitializeComponent();
            this.isEdit = isEdit;
            Queries query = new Queries();

            tirComboBox.ItemsSource = query.getAllTirs();
            senderList.ItemsSource = query.getAllCustomers();
            recipientList.ItemsSource = query.getAllCustomers();

            if (isEdit)
            {

                Ladunki selectedCargo= (Ladunki)((MainWindow)Application.Current.MainWindow).cargoList.SelectedItem;
                


                nameBox.Text = selectedCargo.nazwa_ladunku;
                weightBox.Text = Convert.ToString(selectedCargo.waga);
                kindBox.Text = selectedCargo.rodzaj_ladunku;
                startAddresBox.Text = selectedCargo.adres_startowy;
                destinationAddresBox.Text = selectedCargo.adres_docelowy;
                var currentTir = query.findTirByNr(selectedCargo.nr_rejestracyjny_ciezarowki);
                foreach (var tir in currentTir)
                    tirComboBox.SelectedItem = currentTir;
                var currentSender = query.findCustomerByID(selectedCargo.id_nadawcy);
                foreach (var sender in currentSender)
                    choosenSender.Text = sender.imie;
                add.Content = "zapisz";
            }
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
            Queries query = new Queries();
            Ladunki newCargo= new Ladunki();
            newCargo.nazwa_ladunku = nameBox.Text;
            newCargo.waga = Int32.Parse( weightBox.Text);
            newCargo.adres_docelowy = destinationAddresBox.Text;
            newCargo.adres_startowy = startAddresBox.Text;
            newCargo.rodzaj_ladunku = kindBox.Text;
            newCargo.nr_rejestracyjny_ciezarowki = tirComboBox.SelectedValue.ToString();
            newCargo.id_nadawcy = ((Klienci)senderList.SelectedItem).id_klienta;
            newCargo.id_odbiorcy= ((Klienci)recipientList.SelectedItem).id_klienta;
            newCargo.data_nadania= DateTime.ParseExact(sendDatePicker.Text, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            newCargo.data_odbioru = DateTime.ParseExact(receiveDatePicker.Text, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            query.addCargo(newCargo);


            query.submitChanges();

            this.Close();
        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
