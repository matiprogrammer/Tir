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
    /// Interaction logic for NewEditEmploye.xaml
    /// </summary>
    public partial class NewEditEmploye : Window
    {
        public bool isEdit;
        private Pracownicy selectedEmploye;

        public decimal pensja;
        public NewEditEmploye(bool isEdit, Window mainWindow)
        {
            InitializeComponent();
            this.isEdit = isEdit;

            if (isEdit)
            {
                this.selectedEmploye =(Pracownicy)((MainWindow)Application.Current.MainWindow).employeList.SelectedItem;
                peselBox.Text = selectedEmploye.nr_pesel;
                surnameBox.Text = selectedEmploye.nazwisko;
                nameBox.Text = selectedEmploye.imie;
                salaryBox.Text = selectedEmploye.pensja.ToString();
                jobBox.Text = selectedEmploye.stanowisko;
                address1Box.Text = selectedEmploye.adres_zamieszkania;
                address2Box.Text = selectedEmploye.adres_zatrudnienia;
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

        private void AddEditEmploye_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = string.IsNullOrEmpty(peselBox.Text) || string.IsNullOrEmpty(surnameBox.Text) || string.IsNullOrEmpty(nameBox.Text) ||
                !decimal.TryParse(salaryBox.Text, out pensja) || string.IsNullOrEmpty(jobBox.Text) || string.IsNullOrEmpty(address1Box.Text) ||
                string.IsNullOrEmpty(address2Box.Text) ? false : true;
        }

        private void AddEditEmploye_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string nr_pesel = peselBox.Text;
            string nazwisko = surnameBox.Text;
            string imie = nameBox.Text;
            string stanowisko = jobBox.Text;
            string adres_zamieszkania = address1Box.Text;
            string adres_zatrudnienia = address2Box.Text;

            #region Walidacja danych pracownika
            #endregion

            Queries query = new Queries();
            if (!isEdit)
            {
                Pracownicy employe = new Pracownicy();
                employe.nr_pesel = nr_pesel;
                employe.nazwisko = nazwisko;
                employe.imie = imie;
                employe.pensja = pensja;
                employe.stanowisko = stanowisko;
                employe.adres_zamieszkania = adres_zamieszkania;
                employe.adres_zatrudnienia = adres_zatrudnienia;

                query.addEmploye(employe);
            }
            else
            {

                var update = query.findEmployeByPesel(nr_pesel);

                foreach (var employe in update)
                {
                    employe.nr_pesel = nr_pesel;
                    employe.nazwisko = nazwisko;
                    employe.imie = imie;
                    employe.pensja = pensja;
                    employe.stanowisko = stanowisko;
                    employe.adres_zamieszkania = adres_zamieszkania;
                    employe.adres_zatrudnienia = adres_zatrudnienia;
                }

            }
            query.submitChanges();

            this.Close();
        }
    }
}
