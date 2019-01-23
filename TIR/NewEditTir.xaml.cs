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

        public NewEditTir(bool isEdit)
        {
            Queries query = new Queries();
            this.isEdit = isEdit;
            InitializeComponent();
                driverBox.ItemsSource = query.findEmployeByJob("kierowca");
  

            if (isEdit)
            {
                
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


        private void addTir(object sender, RoutedEventArgs e)
        {
            Queries query = new Queries();
            if (!isEdit)
            {
                Ciezarowki tir = new Ciezarowki();
                tir.nr_rejestracyjny_ciezarowki = nrBox.Text;
                tir.rocznik = Int32.Parse(yearBox.Text);
                tir.maksymalne_dopuszczalne_obciazenie = Int32.Parse(loadBox.Text);
                tir.model = modelBox.Text;
                tir.producent = producentBox.Text;
                tir.kolor = colorBox.Text;
                tir.nr_pesel_kierowcy = driverBox.SelectedValue.ToString();
                query.addTir(tir);
               
            }
            else
            {
                var update = query.findTirByNr(nrBox.Text);

                foreach (var tir in update)
                {
                    tir.nr_rejestracyjny_ciezarowki = nrBox.Text;
                    tir.rocznik = Int32.Parse(yearBox.Text);
                    tir.maksymalne_dopuszczalne_obciazenie = Int32.Parse(loadBox.Text);
                    tir.model = modelBox.Text;
                    tir.producent = producentBox.Text;
                    tir.kolor = colorBox.Text;
                    tir.nr_pesel_kierowcy = driverBox.SelectedValue.ToString();
                }

            }
            query.submitChanges();

            this.Close();
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
    }
}
