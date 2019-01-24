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
using System.Windows.Shapes;

namespace TIR
{
    /// <summary>
    /// Interaction logic for AddReviewWindow.xaml
    /// </summary>
    public partial class AddReviewWindow : Window
    {
        private Ciezarowki currentTir;
        public AddReviewWindow(Ciezarowki currentTir)
        {
            InitializeComponent();
            this.currentTir = currentTir;
            firmBox.ItemsSource = new Queries().getAllCompanies();

        }

        private void addReview(object sender, RoutedEventArgs e)
        {
            
            Przeglady newReview = new Przeglady();
            newReview.data_przegladu= DateTime.ParseExact(firstDateBox.Text, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
            newReview.data_nastepnego_serwisu= DateTime.ParseExact(secondDateBox.Text, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
            newReview.nr_rejestracyjny_ciezarowki = currentTir.nr_rejestracyjny_ciezarowki;
            newReview.nr_nip_serwisu =((Firmy_serwisujace) firmBox.SelectedItem).nr_nip;
            new Queries().addReview(newReview);
            this.Close();
        }

        private void Anuuj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
