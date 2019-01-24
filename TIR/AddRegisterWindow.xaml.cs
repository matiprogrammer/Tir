using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddRegisterWindow.xaml
    /// </summary>
    public partial class AddRegisterWindow : Window
    { private Ciezarowki selectedTir;
        private List<Czesci> addedParts;
        public AddRegisterWindow(Ciezarowki tir)
        {
            InitializeComponent();
            this.selectedTir = tir;
            addedParts = new List<Czesci>();
            companyBox.ItemsSource = new Queries().getAllCompanies();
            partsList.ItemsSource = addedParts;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9].");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NewPart(object sender, RoutedEventArgs e)
        {
            Czesci newPart = new Czesci();
            newPart.nazwa_czesci = partNameBox.Text;
            newPart.koszt_czesci = decimal.Parse(priceBox.Text, CultureInfo.InvariantCulture);
            new Queries().addPart(newPart);
            addedParts.Add(newPart);
            partsList.Items.Refresh();
            partPriceBox.Text = "";
            partNameBox.Text = "";
            
        }

        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitRegister(object sender, RoutedEventArgs e)
        {
            Queries query = new Queries();
            Rejestr_napraw register = new Rejestr_napraw();
            register.data_naprawy= DateTime.ParseExact(repairDateBox.Text, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
            register.nr_rejestracyjny_ciezarowki = selectedTir.nr_rejestracyjny_ciezarowki;
            register.nr_nip_serwisu = ((Firmy_serwisujace)companyBox.SelectedItem).nr_nip;
            register.koszt_robocizny =decimal.Parse(priceBox.Text, CultureInfo.InvariantCulture);

            query.addRegisterRepairs(register);

            var lastRegister = query.getLastRegister();
            foreach(var part in addedParts)
            {
                Wymienione_czesci changedParts = new Wymienione_czesci();
                changedParts.id_czesci = part.id_czesci;
                changedParts.nr_faktury = lastRegister.nr_faktury;

                query.addChangedPart(changedParts);
            }

            this.Close();
        }
    }
}
