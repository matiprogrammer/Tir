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

        private void addEmploye(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
            {
                Pracownicy employe = new Pracownicy();
                employe.nr_pesel = peselBox.Text;
                employe.nazwisko = surnameBox.Text;
                employe.imie = nameBox.Text;
                employe.pensja = decimal.Parse(salaryBox.Text);
                employe.stanowisko = jobBox.Text;
                employe.adres_zamieszkania = address1Box.Text;
                employe.adres_zatrudnienia = address2Box.Text;

                Queries.Instance.addEmploye(employe);
            }
            else
            {
                var update = Queries.Instance.findEmployeByPesel(peselBox.Text);

                foreach(var employe in update)
                {
                    employe.nr_pesel= peselBox.Text;
                    employe.nazwisko = surnameBox.Text;
                    employe.imie = nameBox.Text;
                    employe.pensja = decimal.Parse(salaryBox.Text);
                    employe.stanowisko = jobBox.Text;
                    employe.adres_zamieszkania = address1Box.Text;
                    employe.adres_zatrudnienia = address2Box.Text;
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
