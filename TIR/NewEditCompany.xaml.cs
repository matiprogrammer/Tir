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
    /// Logika interakcji dla klasy NewEditCompany.xaml
    /// </summary>
    public partial class NewEditCompany : Window
    {
        public bool isEdit;
        private Firmy_serwisujace selectedCompany;

        public NewEditCompany(bool isEdit)
        {
            InitializeComponent();
            this.isEdit = isEdit;

            if (isEdit)
            {
                this.selectedCompany = (Firmy_serwisujace)((MainWindow)Application.Current.MainWindow).CompanyList.SelectedItem;

                CompanyNameBox.Text = selectedCompany.nazwa;
                CompanyAddressBox.Text = selectedCompany.adres;
                CompanyPhoneNumberBox.Text = selectedCompany.nr_telefonu;

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

        private void AddEditCompanyCommmand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = string.IsNullOrEmpty(CompanyNameBox.Text) || string.IsNullOrEmpty(CompanyAddressBox.Text)
                || string.IsNullOrEmpty(CompanyPhoneNumberBox.Text) ? false : true;
        }

        private void AddEditCompanyCommmand_Executed(object sender, ExecutedRoutedEventArgs e)
        {            
            string nazwa = CompanyNameBox.Text;
            string adres = CompanyAddressBox.Text;
            string nr_telefonu = CompanyPhoneNumberBox.Text;

            #region Sprawdzanie długości wprowadzonych danych
            if (nazwa.Length < 3)
            {
                MessageBox.Show("Nazwa firmy serwisującej musi mieć conajmniej 3 znaki!", "Zbyt krótka nazwa", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwa.Length > 50)
            {
                MessageBox.Show("Nazwa firmy serwisującej nie może być dłuższa niż 50 znaków!", "Za długa nazwa", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres.Length < 5)
            {
                MessageBox.Show("Adres firmy serwisującej musi mieć conajmniej 8 znaków!", "Zbyt krótki adres", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres.Length > 100)
            {
                MessageBox.Show("Podany adres firmy serwisującej nie może być dłuższy niż 100 znaków!", "Za długi adres", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nr_telefonu.Length != 9)
            {
                MessageBox.Show("Numer telefonu do firmy serwisującej musi składać się z dokładnie 9 cyfr!", "Nieprawidłowy numer telefonu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            Queries query = new Queries();
            if (!isEdit)
            {
                Firmy_serwisujace newCompany = new Firmy_serwisujace();
                newCompany.nazwa = nazwa;
                newCompany.adres = adres;
                newCompany.nr_telefonu = nr_telefonu;

                query.addCompany(newCompany);
            }
            else
            {
                var modifiedCompany = query.findCompanyByNIP(selectedCompany.nr_nip);

                foreach (var company in modifiedCompany)
                {
                    company.nazwa = nazwa;
                    company.adres = adres;
                    company.nr_telefonu = nr_telefonu;
                }
            }
            query.submitChanges();

            this.Close();
        }
    }
}
