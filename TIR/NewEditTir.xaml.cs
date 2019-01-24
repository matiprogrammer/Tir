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
    /// 

     
    public partial class NewEditTir : Window
    {
        public bool isEdit;

        int rocznik;
        int maksymalne_dopuszczalne_obciazenie;

        public NewEditTir(bool isEdit)
        {
            Queries query = new Queries();
            this.isEdit = isEdit;
            InitializeComponent();
                driverBox.ItemsSource = query.findEmployeByJob("kierowca");
  

            if (isEdit)
            {
                this.Title = "Edytuj ciężarówkę";
                
                Ciezarowki selectedTir =(Ciezarowki)((MainWindow)Application.Current.MainWindow).tirList.SelectedItem;
                string peseltmp = selectedTir.nr_pesel_kierowcy;
              

                yearBox.Text = Convert.ToString(selectedTir.rocznik); 
                loadBox.Text = Convert.ToString(selectedTir.maksymalne_dopuszczalne_obciazenie);
                modelBox.Text = selectedTir.model;
                producentBox.Text = selectedTir.producent;
                colorBox.Text = selectedTir.kolor;
                nrBox.Text = Convert.ToString(selectedTir.nr_rejestracyjny_ciezarowki);
                var currentDriver= query.findEmployeByPesel(selectedTir.nr_pesel_kierowcy);
                foreach (var driver in currentDriver)
                    driverBox.SelectedItem = driver;
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

        private void AddEditTir_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Regex regex = new Regex("^[A-Z]{2,3}[0-9]{1}[A-Z0-9]+$");

            string nr_rejestracyjny_ciezarowki = nrBox.Text;
            string model = modelBox.Text;
            string producent = producentBox.Text;
            string kolor = colorBox.Text;

            #region Walidacja danych
            if (!regex.IsMatch(nr_rejestracyjny_ciezarowki))
            {
                MessageBox.Show("Podany numer rejestracyjny ciężarówki jest nieprawidłowy. Numer musi zawierać 2 lub 3 wielkie litery, przynajmniej 1 cyfrę a potem cyfry i/lub wielkie ltery do 10 znaków!",
                    "Nieprawidłowy format numery rejestracyjnego", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(nr_rejestracyjny_ciezarowki.Length > 10)
            {
                MessageBox.Show("Numer rejestracyjny ciężarówki nie może zawierać ponad 10 znaków!", "Zbyt długi numer rejestracyjny", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(rocznik < 1970 && rocznik > 2020)
            {
                MessageBox.Show("Podany rocznik ciężarówki nie mieści się w przedziale 1970-2020!", "Nieprawidłowy rocznik", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(maksymalne_dopuszczalne_obciazenie < 0)
            {
                MessageBox.Show("Maksymalne dopuszczalne obciążenie nie może być wartością ujemną!", "Ujemna wartość obciążenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(kolor.Length < 3)
            {
                MessageBox.Show("Nazwa koloru musi się składać z conajmniej 3 znaków!", "Zbyt krótka nazwa koloru", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(kolor.Length > 15)
            {
                MessageBox.Show("Podana nazwa koloru przekracza liczbę 15 znaków!", "Za długa nazwa koloru", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model.Length < 3)
            {
                MessageBox.Show("Model ciężarówki musi się składać z conajmniej 3 znaków!", "Zbyt krótka nazwa modelu", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model.Length > 15)
            {
                MessageBox.Show("Podana nazwa modelu ciężarówki przekracza liczbę 15 znaków!", "Za długa nazwa modelu", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (producent.Length < 3)
            {
                MessageBox.Show("Nazwa producenta musi się składać z conajmniej 3 znaków!", "Zbyt krótka nazwa producenta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (producent.Length > 15)
            {
                MessageBox.Show("Podana nazwa producenta przekracza liczbę 15 znaków!", "Za długa nazwa producenta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

            Queries query = new Queries();
            if (!isEdit)
            {
                Ciezarowki tir = new Ciezarowki();
                tir.nr_rejestracyjny_ciezarowki = nr_rejestracyjny_ciezarowki;
                tir.rocznik = rocznik;
                tir.maksymalne_dopuszczalne_obciazenie =  maksymalne_dopuszczalne_obciazenie;
                tir.model = model;
                tir.producent = producent;
                tir.kolor = kolor;
                tir.nr_pesel_kierowcy = driverBox.SelectedValue.ToString();
                query.addTir(tir);

            }
            else
            {
                var update = query.findTirByNr(nrBox.Text);

                foreach (var tir in update)
                {
                    tir.nr_rejestracyjny_ciezarowki = nr_rejestracyjny_ciezarowki;
                    tir.rocznik = rocznik;
                    tir.maksymalne_dopuszczalne_obciazenie = maksymalne_dopuszczalne_obciazenie;
                    tir.model = model;
                    tir.producent = producent;
                    tir.kolor = kolor;
                    tir.nr_pesel_kierowcy = driverBox.SelectedValue.ToString();
                }

            }
            query.submitChanges();

            this.Close();
        }

        private void AddEditTir_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = string.IsNullOrEmpty(nrBox.Text) || !int.TryParse(yearBox.Text, out rocznik) ||
                !int.TryParse(loadBox.Text, out maksymalne_dopuszczalne_obciazenie) || string.IsNullOrEmpty(modelBox.Text)
                || string.IsNullOrEmpty(producentBox.Text) || string.IsNullOrEmpty(loadBox.Text) || string.IsNullOrEmpty(colorBox.Text)
                    ? false : true;
        }
    }
}
