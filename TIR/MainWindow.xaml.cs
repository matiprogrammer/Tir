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
       public LinqToSQLDataContext dc = new LinqToSQLDataContext(Properties.Settings.Default.TransportCompanyConnectionString);

        public MainWindow()
        {
           
            InitializeComponent();
            fillEmployesList();
            fillTirList();

        }

        private void SearchEmploye(object sender, RoutedEventArgs e)
        {
            var searchEmployes = from p in dc.Pracownicies
                                 where p.nazwisko.Contains(EmployeSearching.Text) || p.imie.Contains(EmployeSearching.Text) || p.stanowisko.Contains(EmployeSearching.Text) ||p.nr_pesel== EmployeSearching.Text
                                 select p;
            if(searchEmployes!=null)
            employeList.ItemsSource = searchEmployes;
        }

        private void ClearEmploye(object sender, RoutedEventArgs e)
        {
            fillEmployesList();
            EmployeSearching.Text = "";
            //cargos.IsSelected = true;
        }

        private void fillEmployesList()
        {
            var employes = from p in dc.Pracownicies
                           select p;
            employeList.ItemsSource = employes;
        }

        private void NewEmploye(object sender, RoutedEventArgs e)
        {
            NewEditEmploye employeWindow = new NewEditEmploye(false,this);
            employeWindow.ShowDialog();
            fillEmployesList();
            
        }

        private void DeleteEmploye(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = employeList.SelectedItem;
            string nr_pesel = selectedItem.nr_pesel;
            var employe = from p in dc.Pracownicies
                          where p.nr_pesel == nr_pesel
                          select p;

            dc.Pracownicies.DeleteOnSubmit((Pracownicy)employeList.SelectedItem);
            dc.SubmitChanges();
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
            var tirs = from p in dc.Ciezarowkis
                           select p;
            tirList.ItemsSource = tirs;
        }

        private void NewTir(object sender, RoutedEventArgs e)
        {
            NewEditTir tirWindow = new NewEditTir();
            tirWindow.ShowDialog();
            fillTirList();

        }

        private void SearchTir(object sender, RoutedEventArgs e)
        {
            int nr_rejestracyjny;
            var result = Int32.TryParse(TirSearching.Text, out nr_rejestracyjny);
            if (!result)
                nr_rejestracyjny = 0;
            var searchTirs = from p in dc.Ciezarowkis
                                 where p.model.Contains(TirSearching.Text) || p.producent.Contains(TirSearching.Text) || p.nr_pesel_kierowcy.Contains(TirSearching.Text) || p.nr_rejestracyjny_ciezarowki== nr_rejestracyjny
                                 select p;
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

        }

        private void DeleteTir(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = tirList.SelectedItem;
            int nr_rejestracyjny = selectedItem.nr_rejestracyjny_ciezarowki;
            var employe = from p in dc.Ciezarowkis
                          where p.nr_rejestracyjny_ciezarowki == nr_rejestracyjny
                          select p;

            dc.Ciezarowkis.DeleteOnSubmit((Ciezarowki)tirList.SelectedItem);
            dc.SubmitChanges();
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
    }
}
