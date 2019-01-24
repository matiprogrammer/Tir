using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TIR
{
    public class Commands
    {
        public static RoutedUICommand AddEditCompanyCommmand { get; private set; }
        public static RoutedUICommand AddEditCustomerCommand { get; private set; }
        public static RoutedUICommand AddEditEmployeCommand { get; private set; }

        public static RoutedUICommand EditItemOnListCommand { get; private set; }
        public static RoutedUICommand DeleteItemFromListCommand { get; private set; }
        static Commands()
        {
            AddEditCompanyCommmand = new RoutedUICommand("Add or Edit Company", "AddEditCompany", typeof(Commands));
            AddEditCustomerCommand = new RoutedUICommand("Add or Edit Customer", "AddEditCustomer", typeof(Commands));
            AddEditEmployeCommand = new RoutedUICommand("Add or Edit Employe", "AddEditEmploye", typeof(Commands));

            EditItemOnListCommand = new RoutedUICommand("Edit selected item on specific list", "EditItemOnList", typeof(Commands));
            DeleteItemFromListCommand = new RoutedUICommand("Delete selected item from specific list", "DeleteItemFromList", typeof(Commands));
        }
    }
}
