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
    /// Interaction logic for TirDetails.xaml
    /// </summary>
    public partial class TirDetails : Window
    {
        
        public TirDetails()
        {
            InitializeComponent();
            Ciezarowki selectedTir = (Ciezarowki)((MainWindow)Application.Current.MainWindow).tirList.SelectedItem;
            nr_rejestracyjnyTextBox.Text = selectedTir.nr_rejestracyjny_ciezarowki;
            rocznikTextBox.Text = selectedTir.rocznik.ToString();
            modelTextBox.Text = selectedTir.model;
            producentTextBox.Text = selectedTir.producent;
            kolorTextBox.Text = selectedTir.kolor;
            loadTextBox.Text = selectedTir.maksymalne_dopuszczalne_obciazenie.ToString();

           var currentDriver= new Queries().findEmployeByPesel(selectedTir.nr_pesel_kierowcy);
            foreach (var driver in currentDriver)
                kierowcaTextBox.Text = driver.imie + " "+driver.nazwisko;


            //cargoList.ItemsSource=

        }


        private void SearchCargo(object sender, RoutedEventArgs e)
        {
            cargoList.ItemsSource = new Queries().findCargo(CargoSearching.Text);
        }

        private void ClearCargo(object sender, RoutedEventArgs e)
        {
            cargoList.ItemsSource = new Queries().getAllCargos();
        }

        private void NewCargo(object sender, RoutedEventArgs e)
        {

        }

        private void EditCargo(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCargo(object sender, RoutedEventArgs e)
        {

        }

        private void CargoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void currentDriverClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
