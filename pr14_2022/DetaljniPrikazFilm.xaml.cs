using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace pr14_2022
{
    /// <summary>
    /// Interaction logic for DetaljniPrikazFilm.xaml
    /// </summary>
    public partial class DetaljniPrikazFilm : Window
    {

        private string fajl_pomocni = "";
        private string slika_pomocna = "";
        public DetaljniPrikazFilm(int index)
        {
            Film film = MainWindow.Films[index];
            InitializeComponent();

            textBoxNaziv.Text = film.nazivFilma;
            textBoxBroj.Text = "Ocjena filma je: " + Convert.ToString(film.ocjenaFilma) + ".";
            textBoxDatum.Text = "Datum dodavanja je: " + film.datumDodavanja.ToString() + ".";
            textBoxFajl.Text = "Fajl je: " + film.Fajl.ToString();
            slika_pomocna = film.Slika;

            Uri uri = new Uri(film.Slika);
            imgSlika.Source = new BitmapImage(uri);

            TextRange textRange;
            System.IO.FileStream fileStream;

            fajl_pomocni = film.Fajl;

            if (System.IO.File.Exists(film.Fajl))
            {
                textRange = new TextRange(richTextBoxFilms.Document.ContentStart, richTextBoxFilms.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(film.Fajl, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
            }
        }

        #region Dugme zatvori
        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #endregion
        #region Pomjeraj prozora
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
    }
}
