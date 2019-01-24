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
        private Ciezarowki selectedTir;
        public TirDetails()
        {
            Queries query = new Queries();
            InitializeComponent();
            selectedTir = (Ciezarowki)((MainWindow)Application.Current.MainWindow).tirList.SelectedItem;
            nr_rejestracyjnyTextBox.Text = selectedTir.nr_rejestracyjny_ciezarowki;
            rocznikTextBox.Text = selectedTir.rocznik.ToString();
            modelTextBox.Text = selectedTir.model;
            producentTextBox.Text = selectedTir.producent;
            kolorTextBox.Text = selectedTir.kolor;
            loadTextBox.Text = selectedTir.maksymalne_dopuszczalne_obciazenie.ToString();

           var currentDriver= new Queries().findEmployeByPesel(selectedTir.nr_pesel_kierowcy);
            foreach (var driver in currentDriver)
                kierowcaTextBox.Text = driver.imie + " "+driver.nazwisko;


            refreschLists();
            

        }
        public void refreschLists()
        {
            Queries query = new Queries();
            cargoList.ItemsSource = query.findCargosByTir(selectedTir.nr_rejestracyjny_ciezarowki);
            reviewList.ItemsSource = query.findReviewsByTir(selectedTir.nr_rejestracyjny_ciezarowki);
            registerList.ItemsSource = query.findRegistersByTir(selectedTir.nr_rejestracyjny_ciezarowki);
        }

        private void currentDriverClick(object sender, MouseButtonEventArgs e)
        {

        }
        #region Ladunki ciezrowki

        private void SearchCargo(object sender, RoutedEventArgs e)
        {
            cargoList.ItemsSource = new Queries().findCargo(CargoSearching.Text, selectedTir.nr_rejestracyjny_ciezarowki);
        }

        private void ClearCargo(object sender, RoutedEventArgs e)
        {
            refreschLists();
            CargoSearching.Text = "";
        }

        private void EditCargo(object sender, RoutedEventArgs e)
        {
            NewEditCargo editCargoWindow = new NewEditCargo(true,(Ladunki)cargoList.SelectedItem);
            editCargoWindow.ShowDialog();
            refreschLists();
        }

        private void DeleteCargo(object sender, RoutedEventArgs e)
        {
            new Queries().deleteCargo((Ladunki)cargoList.SelectedItem);
            refreschLists();
        }

        private void CargoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cargoList.SelectedIndex > -1)
            {
                CargoDeleteButton.IsEnabled = true;
                CargoEditButton.IsEnabled = true;
            }
            else
            {
                CargoDeleteButton.IsEnabled = false;
                CargoEditButton.IsEnabled = false;
            }
        }
        #endregion


        #region Przeglady
        private void reviewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reviewList.SelectedIndex > -1)
                reviewDeleteButton.IsEnabled = true;
            
            else
                reviewDeleteButton.IsEnabled = false;

            
        }
    

        private void Deletereview(object sender, RoutedEventArgs e)
        {
            new Queries().deleteReview(selectedTir.nr_rejestracyjny_ciezarowki, ((Przeglady)reviewList.SelectedItem).data_przegladu);
            refreschLists();
        }
        private void Addreview(object sender, RoutedEventArgs e)
        {
            AddReviewWindow newReviewWindow = new AddReviewWindow(selectedTir);
            newReviewWindow.ShowDialog();
            refreschLists();
        }



        #endregion


        private void SearchRegister(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRegister(object sender, RoutedEventArgs e)
        {
            new Queries().deleteRegister(((Rejestr_napraw)registerList.SelectedItem).nr_faktury);
        }

        private void addRegister(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cargoList.SelectedIndex > -1)
            {
                registerDeleteButton.IsEnabled = true;
            }
            else
            {
                registerDeleteButton.IsEnabled = false;
            }
        }
    }
}
