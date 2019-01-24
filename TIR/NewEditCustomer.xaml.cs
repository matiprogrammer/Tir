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
    /// Logika interakcji dla klasy NewEditCustomer.xaml
    /// </summary>
    public partial class NewEditCustomer : Window
    {
        public bool isEdit;
        private Klienci selectedCustomer;

        public NewEditCustomer(bool isEdit)
        {
            InitializeComponent();
            this.isEdit = isEdit;

            if (isEdit)
            {
                this.Title = "Edytuj klienta";
                this.selectedCustomer = (Klienci)((MainWindow)Application.Current.MainWindow).CustomerList.SelectedItem;

                CustomerFirstNameBox.Text = selectedCustomer.imie;
                CustomerLastNameBox.Text = selectedCustomer.nazwisko;
                CustomerAddressBox.Text = selectedCustomer.adres_zamieszkania;
                CustomerPhoneNumberBox.Text = selectedCustomer.nr_telefonu;
                add.Content = "Zapisz";
            }
        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddEditCustomer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string imie = CustomerFirstNameBox.Text;
            string nazwisko = CustomerLastNameBox.Text;
            string adres_zamieszkania = CustomerAddressBox.Text;
            string nr_telefonu = CustomerPhoneNumberBox.Text;

            #region Sprawdzanie długości wprowadzonych danych
            if (imie.Length < 2)
            {
                MessageBox.Show("Imię klienta musi zawierać przynajmniej 2 znaki!", "Zbyt krótkie imię klienta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (imie.Length > 30)
            {
                MessageBox.Show("Imię klienta nie może być dłuższe niż 30 znaków!", "Za długie imię klienta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwisko.Length < 2)
            {
                MessageBox.Show("Nazwisko klienta musi zawierać przynajmniej 2 znaki!", "Zbyt krótkie nazwisko klienta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwisko.Length > 50)
            {
                MessageBox.Show("Nazwisko klienta nie może być dłuższe niż 50 znaków!", "Za długie nazwisko klienta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_zamieszkania.Length < 8)
            {
                MessageBox.Show("Adres zamieszkania klienta musi mieć conajmniej 8 znaków!", "Zbyt krótki adres", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_zamieszkania.Length > 100)
            {
                MessageBox.Show("Podany adres zamieszkania nie może być dłuższy niż 100 znaków!", "Za długi adres", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nr_telefonu.Length != 9)
            {
                MessageBox.Show("Numer telefonu do klienta musi składać się z dokładnie 9 cyfr!", "Nieprawidłowy numer telefonu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            Queries query = new Queries();
            if (!isEdit)
            {
                Klienci newCustomer = new Klienci();
                newCustomer.imie = imie;
                newCustomer.nazwisko = nazwisko;
                newCustomer.adres_zamieszkania = adres_zamieszkania;
                newCustomer.nr_telefonu = nr_telefonu;

                query.addCustomer(newCustomer);
            }
            else
            {
                var modifiedCustomer = query.findCustomerByID(selectedCustomer.id_klienta);


                modifiedCustomer.imie = imie;
                modifiedCustomer.nazwisko = nazwisko;
                modifiedCustomer.adres_zamieszkania = adres_zamieszkania;
                modifiedCustomer.nr_telefonu = nr_telefonu;

            }
            query.submitChanges();

            this.Close();
        }

        private void AddEditCustomer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = string.IsNullOrEmpty(CustomerFirstNameBox.Text) || string.IsNullOrEmpty(CustomerLastNameBox.Text)
                || string.IsNullOrEmpty(CustomerAddressBox.Text) || string.IsNullOrEmpty(CustomerPhoneNumberBox.Text) ? false : true;
        }
    }
}
