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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Common;
using System.Configuration;
using System.ComponentModel;

namespace TIR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {     


        public MainWindow()
        {
           
            InitializeComponent();
            fillEmployesList();
            fillTirList();

        }

        private void SearchEmploye(object sender, RoutedEventArgs e)
        {

            employeList.ItemsSource = new Queries().findEmploye(EmployeSearching.Text); ;
        }

        private void ClearEmploye(object sender, RoutedEventArgs e)
        {
            fillEmployesList();
            EmployeSearching.Text = "";
            //cargos.IsSelected = true;
        }

        private void fillEmployesList()
        {
            var x = new Queries().getAllEmployes();
            employeList.ItemsSource = x;
            foreach (var xx in x)
                Console.WriteLine("pracownicy"+xx.nr_pesel);

        }

        private void NewEmploye(object sender, RoutedEventArgs e)
        {
            NewEditEmploye employeWindow = new NewEditEmploye(false,this);
            employeWindow.ShowDialog();
            fillEmployesList();
            
        }

        private void DeleteEmploye(object sender, RoutedEventArgs e)
        {
            Pracownicy selectedItem =(Pracownicy)employeList.SelectedItem;
            // string nr_pesel = selectedItem.nr_pesel;
            //var employe = Queries.Instance.findEmployeByPesel(nr_pesel);
            //if (selectedItem.stanowisko.ToLower() == "kierowca")
            //{
            //    Ciezarowki tir = Queries.Instance.findTirByPesel(selectedItem.nr_pesel);
            //    if(tir!=null)
            //    tir.nr_pesel_kierowcy = null;
            //}
            new Queries().deleteEmploye(selectedItem);
            fillEmployesList();
            employeList.Items.Refresh();
            tirList.Items.Refresh();
                        fillTirList();
            
        }

        private void EditEmploye(object sender, RoutedEventArgs e)
        {
            NewEditEmploye employeWindow = new NewEditEmploye(true, this);
            employeWindow.ShowDialog();
            fillEmployesList();
            employeList.Items.Refresh();
            
        }

        private void EmployeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeList.SelectedIndex > -1)
            {
                EmployeDeleteButton.IsEnabled = true;
                EmployeEditButton.IsEnabled = true;
            }
            else
            {
                EmployeDeleteButton.IsEnabled = false;
                EmployeEditButton.IsEnabled = false;
            }

        }

        //------------------------------------------------------------------------------------------------------------ Ciezarowki------------------------------------------------

        private void fillTirList()
        {   
            //var x= Queries.Instance.getAllTirs();
            var x= new Queries().getAllTirs();
            tirList.ItemsSource = x;
            foreach(var xx in x)
            Console.WriteLine(xx.nr_pesel_kierowcy);
        }

        private void NewTir(object sender, RoutedEventArgs e)
        {
            NewEditTir tirWindow = new NewEditTir(false);
            tirWindow.ShowDialog();
            fillTirList();

        }

        private void SearchTir(object sender, RoutedEventArgs e)
        {

            var searchTirs = new Queries().findTir(TirSearching.Text);
            if (searchTirs != null)
                tirList.ItemsSource = searchTirs;
        }

        private void ClearTir(object sender, RoutedEventArgs e)
        {
            fillTirList();
            TirSearching.Text = "";
        }

        private void EditTir(object sender, RoutedEventArgs e)
        {
            NewEditTir tirWindow = new NewEditTir(true);
            tirWindow.ShowDialog();
            fillTirList();
            
        }

        private void DeleteTir(object sender, RoutedEventArgs e)
        {
           Ciezarowki selectedItem = (Ciezarowki)tirList.SelectedItem;
            new Queries().deleteTir(selectedItem);
            fillTirList();
        }

        private void TirSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tirList.SelectedIndex > -1)
            {
                TirDeleteButton.IsEnabled = true;
                TirEditButton.IsEnabled = true;
            }
            else
            {
                TirDeleteButton.IsEnabled = false;
                TirEditButton.IsEnabled = false;
            }
        }

        private void tirChoosen(object sender, MouseButtonEventArgs e)
        {
            TirDetails tirDetailsWindow = new TirDetails();
            tirDetailsWindow.Show();
        }

        //----------------------------------------------------------------------------------------------------------------LADUNKI-------------------------------------
        private void fillCargoList()
        {
            cargoList.ItemsSource = new Queries().getAllCargos();
        }
        private void SearchCargo(object sender, RoutedEventArgs e)
        {
            cargoList.ItemsSource = new Queries().findCargo(CargoSearching.Text);
        }

        private void ClearCargo(object sender, RoutedEventArgs e)
        {
            cargoList.ItemsSource = new Queries().getAllCargos();
            CargoSearching.Text = "";
        }

        private void NewCargo(object sender, RoutedEventArgs e)
        {
            NewEditCargo newCargoWindow = new NewEditCargo();
            newCargoWindow.ShowDialog();
        }

        private void EditCargo(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCargo(object sender, RoutedEventArgs e)
        {
            Ladunki selectedCargo = (Ladunki)cargoList.SelectedItem;
            new Queries().deleteCargo(selectedCargo);
            fillCargoList();
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
    }
}
