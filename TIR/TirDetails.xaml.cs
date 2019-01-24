﻿using System;
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


            refreshLists();
            

        }
        public void refreshLists()
        {
            Queries query = new Queries();
            cargoList.ItemsSource = query.findCargosByTir(selectedTir.nr_rejestracyjny_ciezarowki);
            reviewList.ItemsSource = query.findReviewsByTir(selectedTir.nr_rejestracyjny_ciezarowki);
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
            refreshLists();
            CargoSearching.Text = "";
        }

        private void EditCargo(object sender, RoutedEventArgs e)
        {
            NewEditCargo editCargoWindow = new NewEditCargo(true,(Ladunki)cargoList.SelectedItem);
            editCargoWindow.ShowDialog();
            refreshLists();
        }

        private void DeleteCargo(object sender, RoutedEventArgs e)
        {
            new Queries().deleteCargo((Ladunki)cargoList.SelectedItem);
            refreshLists();
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
        private void Cleareview(object sender, RoutedEventArgs e)
        {
            reviewList.ItemsSource = new Queries().findReviewsByTir(selectedTir.nr_rejestracyjny_ciezarowki);
            
            refreshLists();
        }

        private void Deletereview(object sender, RoutedEventArgs e)
        {
            new Queries().deleteReview(selectedTir.nr_rejestracyjny_ciezarowki, ((Przeglady)reviewList.SelectedItem).data_przegladu);
            refreshLists();
        }
        private void Addeview(object sender, RoutedEventArgs e)
        {

        }


        #endregion


    }
}
