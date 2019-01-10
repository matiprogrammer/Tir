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

            employeList.ItemsSource = Queries.Instance.findEmploye(EmployeSearching.Text); ;
        }

        private void ClearEmploye(object sender, RoutedEventArgs e)
        {
            fillEmployesList();
            EmployeSearching.Text = "";
            //cargos.IsSelected = true;
        }

        private void fillEmployesList()
        {
           
            employeList.ItemsSource = Queries.Instance.getAllEmployes();
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
            Ciezarowki tir= Queries.Instance.findTirByPesel(selectedItem.nr_pesel);
            tir.nr_pesel_kierowcy = null;
            Queries.Instance.deleteEmploye(selectedItem);
            fillEmployesList();
        }

        private void EditEmploye(object sender, RoutedEventArgs e)
        {
            NewEditEmploye employeWindow = new NewEditEmploye(true, this);
            employeWindow.ShowDialog();
            //fillEmployesList();
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
            tirList.ItemsSource = Queries.Instance.getAllTirs(); 
        }

        private void NewTir(object sender, RoutedEventArgs e)
        {
            NewEditTir tirWindow = new NewEditTir(false);
            tirWindow.ShowDialog();
            fillTirList();

        }

        private void SearchTir(object sender, RoutedEventArgs e)
        {

            var searchTirs = Queries.Instance.findTir(TirSearching.Text);
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
            
        }

        private void DeleteTir(object sender, RoutedEventArgs e)
        {
           Ciezarowki selectedItem = (Ciezarowki)tirList.SelectedItem;
            Queries.Instance.deleteTir(selectedItem);
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

       
        
    }
}
