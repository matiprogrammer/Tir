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
            fillCustomerList();
            fillCargoList();
            fillCompanyList();
        }

        private void SearchEmploye(object sender, RoutedEventArgs e)
        {

            employeList.ItemsSource = new Queries().findEmploye(EmployeSearching.Text);
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

        private void DeleteEmploye()
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

        private void EditEmploye()
        {
            NewEditEmploye employeWindow = new NewEditEmploye(true, this);
            employeWindow.ShowDialog();
            fillEmployesList();
            employeList.ItemsSource = new Queries().findEmploye(EmployeSearching.Text);

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
            // Można uprościć?
            var searchTirs = new Queries().findTir(TirSearching.Text);
            if (searchTirs != null)
                tirList.ItemsSource = searchTirs;
        }

        private void ClearTir(object sender, RoutedEventArgs e)
        {
            fillTirList();
            TirSearching.Text = "";
        }

        private void EditTir()
        {
            NewEditTir tirWindow = new NewEditTir(true);
            tirWindow.ShowDialog();
            fillTirList();
            tirList.ItemsSource = new Queries().findTir(TirSearching.Text);                     //Potencjalne źródło problemów?    
        }

        private void DeleteTir()
        {
            Ciezarowki selectedItem = (Ciezarowki)tirList.SelectedItem;
            new Queries().deleteTir(selectedItem);
            fillTirList();
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
            NewEditCargo newCargoWindow = new NewEditCargo(false,null );
            newCargoWindow.ShowDialog();
            fillCargoList();
        }

        private void EditCargo()
        {
            NewEditCargo newCargoWindow = new NewEditCargo(true,(Ladunki)cargoList.SelectedItem);
            newCargoWindow.ShowDialog();
            fillCargoList();
            cargoList.ItemsSource = new Queries().findCargo(CargoSearching.Text);
        }

        private void DeleteCargo()
        {
            Ladunki selectedCargo = (Ladunki)cargoList.SelectedItem;
            new Queries().deleteCargo(selectedCargo);
            fillCargoList();
        }

        #region Klienci

        private void fillCustomerList()
        {
            CustomerList.ItemsSource = new Queries().getAllCustomers();
        }

        private void SearchCustomer(object sender, RoutedEventArgs e)
        {
            CustomerList.ItemsSource = new Queries().findCustomer(CustomerSearching.Text);
        }

        private void ClearCustomer(object sender, RoutedEventArgs e)
        {
            CustomerList.ItemsSource = new Queries().getAllCustomers();
            CustomerSearching.Text = "";
        }

        private void NewCustomer(object sender, RoutedEventArgs e)
        {
            NewEditCustomer newEditCustomerWindow = new NewEditCustomer(false);
            newEditCustomerWindow.ShowDialog();
            fillCustomerList();
        }

        private void EditCustomer()
        {
            NewEditCustomer newEditCustomerWindow = new NewEditCustomer(true);
            newEditCustomerWindow.ShowDialog();
            fillCustomerList();
            CustomerList.ItemsSource = new Queries().findCustomer(CustomerSearching.Text);
        }

        private void DeleteCustomer()
        {
            Klienci selectedCustomer = (Klienci)CustomerList.SelectedItem;
            new Queries().deleteCustomer(selectedCustomer);
            fillCustomerList();
        }

        #endregion
        
        #region Firmy Serwisujące
        private void fillCompanyList()
        {
            CompanyList.ItemsSource = new Queries().getAllCompanies();
        }

        private void SearchCompany(object sender, RoutedEventArgs e)
        {
            CompanyList.ItemsSource = new Queries().findCompany(CompanySearching.Text);
        }

        private void ClearCompany(object sender, RoutedEventArgs e)
        {
            CompanyList.ItemsSource = new Queries().getAllCompanies();
            CompanySearching.Text = "";
        }

        private void NewCompany(object sender, RoutedEventArgs e)
        {
            NewEditCompany newEditCompanyWindow = new NewEditCompany(false);
            newEditCompanyWindow.ShowDialog();
            fillCompanyList();
        }

        private void EditCompany()
        {
            NewEditCompany newEditCompanyWindow = new NewEditCompany(true);
            newEditCompanyWindow.ShowDialog();
            fillCompanyList();
            CompanyList.ItemsSource = new Queries().findCompany(CompanySearching.Text);
        }

        private void DeleteCompany()
        {
            Firmy_serwisujace selectedCompany = (Firmy_serwisujace)CompanyList.SelectedItem;
            new Queries().deleteCompany(selectedCompany);
            fillCompanyList();
        }
        #endregion

        private void EditDeleteItemOnList_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var param = (string)e.Parameter;
            switch (param)
            {
                case "Employe":
                    e.CanExecute = employeList.SelectedIndex > -1 ? true : false;
                    break;
                case "Cargo":
                    e.CanExecute = cargoList.SelectedIndex > -1 ? true : false;
                    break;
                case "Customer":
                    e.CanExecute = CustomerList.SelectedIndex > -1 ? true : false;
                    break;
                case "TIR":
                    e.CanExecute = tirList.SelectedIndex > -1 ? true : false;
                    break;
                case "Company":
                    e.CanExecute = CompanyList.SelectedIndex > -1 ? true : false;
                    break;
            }
        }

        private void EditItemOnList_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var param = (string)e.Parameter;
            switch (param)
            {
                case "Employe":
                    EditEmploye();
                    break;
                case "Cargo":
                    EditCargo();
                    break;
                case "Customer":
                    EditCustomer();
                    break;
                case "TIR":
                    EditTir();
                    break;
                case "Company":
                    EditCompany();
                    break;
            }
        }


        private void DeleteItemFromList_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var param = (string)e.Parameter;
            switch (param)
            {
                case "Employe":
                    DeleteEmploye();
                    break;
                case "Cargo":
                    DeleteCargo();
                    break;
                case "Customer":
                    DeleteCustomer();
                    break;
                case "TIR":
                    DeleteTir();
                    break;
                case "Company":
                    DeleteCompany();
                    break;
            }
        }
    }
}
