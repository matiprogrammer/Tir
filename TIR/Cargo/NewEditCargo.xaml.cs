using System;
using System.Collections.Generic;
using System.Globalization;
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
        Ladunki selectedCargo;
        public int waga;
        public NewEditCargo(bool isEdit, Ladunki selectedCargo)
        {

            InitializeComponent();
            this.isEdit = isEdit;
            Queries query = new Queries();

            tirComboBox.ItemsSource = query.getAllTirs();
            senderList.ItemsSource = query.getAllCustomers();
            recipientList.ItemsSource = query.getAllCustomers();

            if (isEdit)
            {
                this.Title = "Edytuj ładunek";
                this.selectedCargo = selectedCargo;
                nameBox.Text = selectedCargo.nazwa_ladunku;
                weightBox.Text = Convert.ToString(selectedCargo.waga);
                kindBox.Text = selectedCargo.rodzaj_ladunku;
                startAddresBox.Text = selectedCargo.adres_startowy;
                destinationAddresBox.Text = selectedCargo.adres_docelowy;
                var currentTir = query.findTirByNr(selectedCargo.nr_rejestracyjny_ciezarowki);

                foreach (var tir in currentTir)
                {
                    tirComboBox.SelectedItem = tir;
                }

                var currentSender = query.findCustomerByID(selectedCargo.id_nadawcy);
                    choosenSender.Text = currentSender.imie+" " + currentSender.nazwisko + " "+ currentSender.adres_zamieszkania;

                var currentRecipient = query.findCustomerByID(selectedCargo.id_odbiorcy);
                    choosenRecipient.Text = currentRecipient.imie + " " + currentRecipient.nazwisko + " " + currentRecipient.adres_zamieszkania;

                sendDatePicker.Text = selectedCargo.data_nadania.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                receiveDatePicker.Text = selectedCargo.data_odbioru?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                recipientList.SelectedItem = currentRecipient;
                senderList.SelectedItem = currentSender;
                add.Content = "Zapisz";
            }
        }

        private void senderButton_Click(object sender, RoutedEventArgs e)
        {
            if (senderGrid.Visibility == Visibility.Hidden)
            {
                senderGrid.Visibility = Visibility.Visible;

                senderGrid.Height = Double.NaN;
                senderButton.Content = "Schowaj";
   
            }
            else
            {
                senderGrid.Visibility = Visibility.Hidden;
                senderGrid.Height = 0;
                senderButton.Content = "Wybierz";
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
                recipientButton.Content = "Schowaj";

            }
            else
            {
                recipientGrid.Visibility = Visibility.Hidden;
                recipientGrid.Height = 0;
                recipientButton.Content = "Wybierz";
            }
        }
        private void searchRecipientButton(object sender, RoutedEventArgs e)
        {
            recipientList.ItemsSource = new Queries().findCustomer(recipientSearching.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SenderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Klienci)senderList.SelectedItem) != null)
            {
                Klienci klient = ((Klienci)senderList.SelectedItem);
                choosenSender.Text = klient.imie + " " + klient.nazwisko + " " + klient.adres_zamieszkania;
                senderButton.Content = "Schowaj";
            }
        }

        private void RecipientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Klienci)recipientList.SelectedItem != null))
            {
                Klienci klient = ((Klienci)recipientList.SelectedItem);
                choosenRecipient.Text = klient.imie + " " + klient.nazwisko + " " + klient.adres_zamieszkania;
            }

        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddEditCargo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !int.TryParse(weightBox.Text, out waga) || string.IsNullOrEmpty(nameBox.Text) || string.IsNullOrEmpty(kindBox.Text) ||
                string.IsNullOrEmpty(startAddresBox.Text) || string.IsNullOrEmpty(destinationAddresBox.Text) ? false : true;
        }

        private void AddEditCargo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string nazwa_ladunku = nameBox.Text;
            string adres_docelowy = destinationAddresBox.Text;
            string adres_startowy = startAddresBox.Text;
            string rodzaj_ladunku = kindBox.Text;

            #region Walidacja danych
            if (adres_docelowy.Length < 8)
            {
                MessageBox.Show("Adres docelowy musi mieć conajmniej 8 znaków!", "Zbyt krótki adres docelowy", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_docelowy.Length > 100)
            {
                MessageBox.Show("Podany adres docelowy nie może być dłuższy niż 100 znaków!", "Za długi adres docelowy", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_startowy.Length < 8)
            {
                MessageBox.Show("Adres startowy musi mieć conajmniej 8 znaków!", "Zbyt krótki adres startowy", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_startowy.Length > 100)
            {
                MessageBox.Show("Podany adres startowy nie może być dłuższy niż 100 znaków!", "Za długi adres startowy", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwa_ladunku.Length < 3)
            {
                MessageBox.Show("Nazwa ładunku musi mieć conajmniej 3 znaki!", "Zbyt krótka nazwa ładunku", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwa_ladunku.Length > 30)
            {
                MessageBox.Show("Podana nazwa ładunku nie może być dłuższa niż 30 znaków!", "Za długa nazwa ładunku", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (rodzaj_ladunku.Length < 3)
            {
                MessageBox.Show("Nazwa ładunku musi mieć conajmniej 3 znaki!", "Zbyt krótka nazwa rodzaju ładunku", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (rodzaj_ladunku.Length > 20)
            {
                MessageBox.Show("Podana nazwa ładunku nie może być dłuższa niż 20 znaków!", "Za długa nazwa rodzaju ładunku", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            Queries query = new Queries();
            if (!isEdit)
            {
                Ladunki newCargo = new Ladunki();
                newCargo.nazwa_ladunku = nazwa_ladunku;
                newCargo.waga = waga;
                newCargo.adres_docelowy = adres_docelowy;
                newCargo.adres_startowy = adres_startowy;
                newCargo.rodzaj_ladunku = rodzaj_ladunku;
                if (tirComboBox.SelectedValue != null)
                    newCargo.nr_rejestracyjny_ciezarowki = tirComboBox.SelectedValue.ToString();
                newCargo.id_nadawcy = ((Klienci)senderList.SelectedItem).id_klienta;
                newCargo.id_odbiorcy = ((Klienci)recipientList.SelectedItem).id_klienta;
                newCargo.data_nadania = DateTime.ParseExact(sendDatePicker.Text, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                if (receiveDatePicker.Text != "")
                    newCargo.data_odbioru = DateTime.ParseExact(receiveDatePicker.Text, "yyyy-MM-dd",
                                               System.Globalization.CultureInfo.InvariantCulture);
                query.addCargo(newCargo);
            }
            else
            {
                var update = query.findCargoById(selectedCargo.id_ladunku);
                foreach (var cargo in update)
                {
                    cargo.waga = waga;
                    cargo.nazwa_ladunku = nazwa_ladunku;
                    cargo.rodzaj_ladunku = nazwa_ladunku;
                    cargo.adres_startowy = adres_startowy;
                    cargo.adres_docelowy = adres_docelowy;
                    if (tirComboBox.SelectedValue != null)
                        cargo.nr_rejestracyjny_ciezarowki = tirComboBox.SelectedValue.ToString();
                    cargo.id_nadawcy = ((Klienci)(senderList.SelectedItem)).id_klienta;
                    cargo.id_odbiorcy = ((Klienci)(recipientList.SelectedItem)).id_klienta;
                    cargo.data_nadania = DateTime.ParseExact(sendDatePicker.Text, "yyyy-MM-dd",
                                          System.Globalization.CultureInfo.InvariantCulture);
                    cargo.data_odbioru = DateTime.ParseExact(receiveDatePicker.Text, "yyyy-MM-dd",
                                               System.Globalization.CultureInfo.InvariantCulture);
                    Console.WriteLine(cargo.id_ladunku);
                }
            }

            query.submitChanges();

            this.Close();
        }
    }
}
