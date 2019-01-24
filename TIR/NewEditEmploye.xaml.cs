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
                this.Title = "Edytuj pracownika";
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
            if (nr_pesel.Length != 11)
            {
                MessageBox.Show("Numer PESEL pracownika musi składać się z dokładnie 11 cyfr!", "Nieprawidłowy numer PESEL", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwisko.Length < 2)
            {
                MessageBox.Show("Nazwisko pracownika musi zawierać przynajmniej 2 znaki!", "Zbyt krótkie nazwisko pracownika", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nazwisko.Length > 50)
            {
                MessageBox.Show("Nazwisko pracownika nie może być dłuższe niż 50 znaków!", "Za długie nazwisko pracownika", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (imie.Length < 2)
            {
                MessageBox.Show("Imię pracownika musi zawierać przynajmniej 2 znaki!", "Zbyt krótkie imię pracownika", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (imie.Length > 30)
            {
                MessageBox.Show("Imię pracownika nie może być dłuższe niż 30 znaków!", "Za długie imię pracownika", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pensja >= 10000000000)
            {
                MessageBox.Show("Podana pensja pracownika jest za duża!", "Zbyt wysoka pensja pracownika", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pensja < 0)
            {
                MessageBox.Show("Pensja pracownika nie może być wartością ujemną!", "Ujemna pensja pracownika", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (stanowisko.Length < 3)
            {
                MessageBox.Show("Nazwa stanowiska pracownika musi mieć conajmniej 3 znaki!", "Zbyt krótka nazwa stanowiska" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (stanowisko.Length > 50)
            {
                MessageBox.Show("Podana nazwa stanowiska przekracza 50 znaków!", "Za długa nazwa stanowiska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (adres_zamieszkania.Length < 8)
            {
                MessageBox.Show("Adres zamieszkania pracownika musi mieć conajmniej 8 znaków!", "Zbyt krótki adres zamieszkania", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_zamieszkania.Length > 100)
            {
                MessageBox.Show("Podany adres zamieszkania nie może być dłuższy niż 100 znaków!", "Za długi adres zamieszkania", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_zatrudnienia.Length < 8)
            {
                MessageBox.Show("Adres zatrudnienia pracownika musi mieć conajmniej 8 znaków!", "Zbyt krótki adres zatrudnienia", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (adres_zatrudnienia.Length > 100)
            {
                MessageBox.Show("Podany adres zatrudnienia nie może być dłuższy niż 100 znaków!", "Za długi adres zatrudnienia", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
