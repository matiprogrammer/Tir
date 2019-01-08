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
    /// Interaction logic for NewEditTir.xaml
    /// </summary>
    public partial class NewEditTir : Window
    {
        public NewEditTir()
        {
            InitializeComponent();
            var searchDrivers = from p in ((MainWindow)Application.Current.MainWindow).dc.Pracownicies
                                where p.stanowisko.ToLower() == "kierowca"
                                 select p;
            if (searchDrivers != null)
                driverBox.ItemsSource = searchDrivers;
        }

        private void addTir(object sender, RoutedEventArgs e)
        {
            

            Ciezarowki tir = new Ciezarowki();
                tir.nr_rejestracyjny_ciezarowki =Int32.Parse(nrBox.Text);
            tir.rocznik = Int32.Parse(yearBox.Text);
            tir.maksymalne_dopuszczalne_obciazenie = Int32.Parse(loadBox.Text);
            tir.model = modelBox.Text;
            tir.producent = producentBox.Text;
            tir.kolor = colorBox.Text;
            tir.nr_pesel_kierowcy = driverBox.SelectedValue.ToString();
            ((MainWindow)Application.Current.MainWindow).dc.Ciezarowkis.InsertOnSubmit(tir);
            ((MainWindow)Application.Current.MainWindow).dc.SubmitChanges();

            this.Close();
        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
