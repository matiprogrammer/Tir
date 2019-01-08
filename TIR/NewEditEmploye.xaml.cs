using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public NewEditEmploye(bool isEdit, Window mainWindow)
        {
            InitializeComponent();
            this.isEdit = isEdit;

            if (isEdit)
            {
                dynamic selectedItem = ((MainWindow)Application.Current.MainWindow).employeList.SelectedItem;
                peselBox.Text = selectedItem.nr_pesel;
                surnameBox.Text = selectedItem.nazwisko;
                nameBox.Text = selectedItem.imie;
                salaryBox.Text = selectedItem.pensja.ToString();
                jobBox.Text = selectedItem.stanowisko;
                address1Box.Text = selectedItem.adres_zamieszkania;
                address2Box.Text = selectedItem.adres_zatrudnienia;
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

                ((MainWindow)Application.Current.MainWindow).dc.Pracownicies.InsertOnSubmit(employe);
                
            }
            else
            {
                var update = from p in ((MainWindow)Application.Current.MainWindow).dc.Pracownicies
                             where p.nr_pesel==peselBox.Text
                             select p;

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
            ((MainWindow)Application.Current.MainWindow).dc.SubmitChanges();

            this.Close();
        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
