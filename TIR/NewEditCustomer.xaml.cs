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
                this.selectedCustomer = (Klienci)((MainWindow)Application.Current.MainWindow).CustomerList.SelectedItem;
                CustomerIDBox.Text = selectedCustomer.id_klienta.ToString();
                CustomerFirstNameBox.Text = selectedCustomer.imie;
                CustomerLastNameBox.Text = selectedCustomer.nazwisko;
                CustomerAddressBox.Text = selectedCustomer.adres_zamieszkania;
                CustomerPhoneNumberBox.Text = selectedCustomer.nr_telefonu;
                add.Content = "Zapisz";
            }
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
            {
                Klienci newCustomer = new Klienci();
                newCustomer.id_klienta = int.Parse(CustomerIDBox.Text);
                newCustomer.imie = CustomerFirstNameBox.Text;
                newCustomer.nazwisko = CustomerLastNameBox.Text;
                newCustomer.adres_zamieszkania = CustomerAddressBox.Text;
                newCustomer.nr_telefonu = CustomerPhoneNumberBox.Text;

                Queries.Instance.addCustomer(newCustomer);
            }
            else
            {
                var modifiedCustomer = Queries.Instance.findCustomerByID(int.Parse(CustomerIDBox.Text));

                foreach (var customer in modifiedCustomer)
                {
                    customer.id_klienta = int.Parse(CustomerIDBox.Text);
                    customer.imie = CustomerFirstNameBox.Text;
                    customer.nazwisko = CustomerLastNameBox.Text;
                    customer.adres_zamieszkania = CustomerAddressBox.Text;
                    customer.nr_telefonu = CustomerPhoneNumberBox.Text;
                }
            }
            Queries.Instance.submitChanges();

            this.Close();
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
    }
}
