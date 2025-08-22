using pr14_2022.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

namespace pr14_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        

        public static bool chexkBoxOznacen = false;

        public static BindingList<Film> brisanje = new BindingList<Film>();
        public static BindingList<Film> Brisanje { get => brisanje; set => brisanje = value; }

        public static readonly DataIO serializer = new DataIO();
        public static BindingList<Film> Films { get; set; }

        public ObservableCollection<User> korisniciUcitani = new ObservableCollection<User>();

        string korisnik1;
        string korisnik2;
        string lozinka1;
        string lozinka2;

        public MainWindow()
        {
            #region Inicijalizacija

            Films = serializer.DeSerializeObject<BindingList<Film>>("film.xml");
            if (Films== null)
            {
                Films= new BindingList<Film>();
            }

            DataContext = this;
            #endregion

            InitializeComponent();
        }

        #region Dugme dodaj

        private void btnDodaj_Click(object sender, RoutedEventArgs e)    //Dodaj
        {
            Dodavanje dodaj = new Dodavanje();
            dodaj.ShowDialog();
        }
        #endregion

        #region Dugme za zatvaranje
        private void btnZatvori_Click(object sender, RoutedEventArgs e)  //Zatvori
        {
            this.Close();
        }

        #endregion


        #region Dugme obrisi
        private void btnObrisi_Click(object sender, RoutedEventArgs e)  //Obrisi
        {
            var rezultat = MessageBox.Show("Da li ste sigurni da želite da obrišete izabrane filmove?","Potvrda brisanja", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (rezultat == MessageBoxResult.Yes)
            {
                for (int i = 0; i < brisanje.Count; i++)
                {
                    Films.Remove(brisanje[i]);
                }

                for (int i = 0; i < brisanje.Count; i++)
                {
                    if (brisanje[i] != null)
                    {
                        string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, brisanje[i].Fajl);
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (IOException exp)
                        {
                            Console.WriteLine(exp.Message);
                        }
                    }
                }
            }
        }
        #endregion

        #region Promjena selekcije u DataGrid
        private void dataGridFilmovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chexkBoxOznacen == true)
            {
                brisanje.Add((Film)dataGridFilmovi.SelectedItem);
            }
            chexkBoxOznacen = false;
        }
        #endregion


        #region Hyperlink
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            DataIO dataIOUictani = new DataIO();
            korisniciUcitani = dataIOUictani.DeSerializeObject<ObservableCollection<User>>("korisnici.xaml");

            foreach (var korisnik in korisniciUcitani)
            {
                if (korisnik.ime == "admin")
                {
                   korisnik1 = korisnik.ime;
                    lozinka1 = korisnik.lozinka;
                }
                else if (korisnik.ime == "posjetilac")
                {
                    korisnik2 = korisnik.ime;
                    lozinka2 = korisnik.lozinka;
                }
            }

            Logovanje logovanje = new Logovanje();

            
            if (Logovanje.ime.Equals(korisnik1) && Logovanje.sifra.Equals(lozinka1))
            {
                Izmjena izmijeni = new Izmjena(dataGridFilmovi.SelectedIndex);
                izmijeni.ShowDialog();
            }
            else if (Logovanje.ime.Equals(korisnik2) && Logovanje.sifra.Equals(lozinka2))
            {
               DetaljniPrikazFilm procitaj = new DetaljniPrikazFilm(dataGridFilmovi.SelectedIndex);
                procitaj.textBoxNaziv.IsEnabled = false;
                procitaj.textBoxDatum.IsEnabled = false;
                procitaj.textBoxBroj.IsEnabled = false;
               procitaj.richTextBoxFilms.IsEnabled = false;
                procitaj.imgSlika.IsEnabled = false;
                procitaj.textBoxFajl.IsEnabled = false;
                procitaj.RichTextBoxText.FontSize = 25;
               procitaj.ShowDialog();

            }
            else
            {
                MessageBox.Show("Pogresan unos admina ili posjetioca.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #endregion

        #region Za brisanje
        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            chexkBoxOznacen = true;

        }
        #endregion

        #region Cuvanje u fajl

        private void Window_Closing(object sender, CancelEventArgs e)   //Sacuvaj u fajl
        {
            serializer.SerializeObject<BindingList<Film>>(Films, "film.xml");

          //  Films.Clear();
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
