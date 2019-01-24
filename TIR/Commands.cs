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
        static Commands()
        {
            AddEditCompanyCommmand = new RoutedUICommand("Add or Edit Company", "AddEditCompany", typeof(Commands));
        }
    }
}
