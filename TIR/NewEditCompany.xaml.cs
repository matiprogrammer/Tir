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

        private void AddCompany(object sender, RoutedEventArgs e)
        {
            Queries query = new Queries();
            if (!isEdit)
            {
                Firmy_serwisujace newCompany = new Firmy_serwisujace();
                newCompany.nazwa = CompanyNameBox.Text;
                newCompany.adres = CompanyAddressBox.Text;
                newCompany.nr_telefonu = CompanyPhoneNumberBox.Text;

                query.addCompany(newCompany);
            }
            else
            {
                var modifiedCompany = query.findCompanyByNIP(selectedCompany.nr_nip);

                foreach (var company in modifiedCompany)
                {
                    company.nazwa = CompanyNameBox.Text;
                    company.adres = CompanyAddressBox.Text;
                    company.nr_telefonu = CompanyPhoneNumberBox.Text;
                }
            }
            query.submitChanges();

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
