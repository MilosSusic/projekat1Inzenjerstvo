using Notification.Wpf;
using pr14_2022.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace pr14_2022
{
    /// <summary>
    /// Interaction logic for Logovanje.xaml
    /// </summary>
    public partial class Logovanje : Window
    {
        #region Pomocna polja
        public static string ime;
        public static string sifra;
        public string korisnik1;
        public string korisnik2;
        public string lozinka1;
        public string lozinka2;
        #endregion
        private readonly NotificationManager notificationManager = new NotificationManager();
        public ObservableCollection<User> korisnici = new ObservableCollection<User>();
        public ObservableCollection<User> korisniciUcitani = new ObservableCollection<User>();

        
        public Logovanje()
        {
            #region Početne vrijednosti
            InitializeComponent();
            textBoxIme.Text = "unesite ime";
            textBoxIme.Foreground = Brushes.Black;

            korisnici.Add(new User("admin", UserRole.Admin, "1234"));
            korisnici.Add(new User("posjetilac", UserRole.Visitor, "4321"));

            var DataIO = new DataIO();
            DataIO.SerializeObject(korisnici, "korisnici.xaml");

            DataIO dataIOUictani = new DataIO();
            korisniciUcitani= dataIOUictani.DeSerializeObject<ObservableCollection<User>>("korisnici.xaml");
            #endregion
        }


        #region Dugme za prijavu
        private void btnPrijava_Click(object sender, RoutedEventArgs e) 
        {
           
            
            if (Validate())
            {
                
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

                if (textBoxIme.Text.Trim().Equals(korisnik1) && passwordBoxSifra.Password.Equals(lozinka1))
                {
                    var toast = new ToastNotification("Uspjesno", "Uspjesno ste se ulogovali kao admin.", NotificationType.Success);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    ime = korisnik1; 
                    sifra =lozinka1;

                    MessageBox.Show("Sada ce vam se prikazati svi filmovi.","Obavjestenje",MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.Yes);
                    MainWindow window = new MainWindow();
                    window.ShowDialog();

                }
                else if (textBoxIme.Text.Trim().Equals(korisnik2) && passwordBoxSifra.Password.Equals(lozinka2))
                {

                    var toast = new ToastNotification("Uspjesno", "Uspjesno ste se ulogovali kao posjetilac.", NotificationType.Success);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    MessageBox.Show("Dobrodošli na sajt Kriminalisticih filmova kao posjetilac .", "Obavještenje!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                    ime = korisnik2;
                   sifra = lozinka2;
                    MainWindow window = new MainWindow();
                    window.dataGridFilmovi.CanUserAddRows = false;
                    window.dataGridFilmovi.CanUserDeleteRows = false;
                    window.dataGridFilmovi.CanUserReorderColumns = false;
                    window.dataGridFilmovi.CanUserResizeColumns = false;
                    window.dataGridFilmovi.CanUserResizeRows = false;
                    window.dataGridFilmovi.CanUserSortColumns = false;
                    window.btnObrisi.IsEnabled = false;
                    window.btnDodaj.IsEnabled = false;
                    window.btnObrisi.Visibility = Visibility.Hidden;
                    window.btnDodaj.Visibility = Visibility.Hidden;
                    window.ShowDialog();

                }
                else if (textBoxIme.Text.Trim().Equals(korisnik1) && !passwordBoxSifra.Password.Equals(lozinka1))
                {
                    var toast = new ToastNotification("Greska", "Niste se uspjesno ulogovali.", NotificationType.Error);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    passwordBoxSifra.Password = "";
                    labelSifraGreska.Content = "Pogresna sifra za admina";
                    passwordBoxSifra.BorderBrush = Brushes.Red;
                }
                else if (!textBoxIme.Text.Trim().Equals(korisnik1) && passwordBoxSifra.Password.Equals(lozinka1))
                {
                    var toast = new ToastNotification("Greska", "Niste se uspjesno ulogovali.", NotificationType.Error);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    textBoxIme.Text = "";
                    labelImeGreska.Content = "Pogresno ime za admina";
                    textBoxIme.BorderBrush = Brushes.Red;

                }
                else if (textBoxIme.Text.Trim().Equals(korisnik2) && !passwordBoxSifra.Password.Equals(lozinka2))
                {
                    var toast = new ToastNotification("Greska", "Niste se uspjesno ulogovali.", NotificationType.Error);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    passwordBoxSifra.Password = "";
                    labelSifraGreska.Content = "Pogresna sifra za posjetioca";
                    passwordBoxSifra.BorderBrush = Brushes.Red;
                }
                else if (!textBoxIme.Text.Trim().Equals(korisnik2) && passwordBoxSifra.Password.Equals(lozinka2))
                {
                    var toast = new ToastNotification("Greska", "Niste se uspjesno ulogovali.", NotificationType.Error);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    textBoxIme.Text = "";
                    labelImeGreska.Content = "Pogresno ime za posjetioca";
                    textBoxIme.BorderBrush = Brushes.Red;

                }
                else
                {
                    var toast = new ToastNotification("Greska", "Niste se uspjesno ulogovali.", NotificationType.Error);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
                    passwordBoxSifra.Password = "";
                    labelSifraGreska.Content = "Pogresna sifra";
                    passwordBoxSifra.BorderBrush = Brushes.Red;
                    textBoxIme.Text = "";
                    labelImeGreska.Content = "Pogresno ime";
                    textBoxIme.BorderBrush = Brushes.Red;
                }
            }
        }
        #endregion

        #region Validacija unosa
        private bool Validate()
        {
            bool result = true;

            //IME
            if (textBoxIme.Text.Trim().Equals("") || textBoxIme.Text.Trim().Equals("unesite ime"))
            {
                result = false;
                labelImeGreska.Content = "Popunite podatke!";
                textBoxIme.BorderBrush = Brushes.Red;
            }
            else
            {
                labelImeGreska.Content = "";
                textBoxIme.BorderBrush = Brushes.Black;
            }
            //SIFRA
            if (passwordBoxSifra.Password.Equals("") || passwordBoxSifra.Password.Equals("unesite šifru"))
            {
                result = false;
                labelSifraGreska.Content = "Popunite podatke!";
                passwordBoxSifra.BorderBrush = Brushes.Red;
            }
            else
            {
                labelSifraGreska.Content = "";
                passwordBoxSifra.BorderBrush = Brushes.Black;
            }
            if (textBoxIme.Text.Trim().Equals("") || textBoxIme.Text.Trim().Equals("unesite ime") && passwordBoxSifra.Password.Equals("") || passwordBoxSifra.Password.Equals("unesite šifru"))
            {
                var toast = new ToastNotification("Greska", "Niste se uspjesno ulogovali,unesite podatke.", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "WindowNotificationArea");
            }
            return result;
        }
        #endregion

        #region TextBoxIme
        private void textBoxIme_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxIme.Text.Trim().Equals("unesite ime"))
            {
                textBoxIme.Text = "";
                textBoxIme.Foreground = Brushes.Black;
            }
            labelImeGreska.Content = "";
            textBoxIme.Foreground = Brushes.Black;
        }

        private void textBoxIme_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxIme.Text.Trim().Equals(string.Empty))
            {
                textBoxIme.Text = "unesite ime";
                textBoxIme.Foreground = Brushes.Black;

            }
        }
        #endregion

        #region PasswordBoxSifra
        private void passwordBoxSifra_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBoxSifra.Password.Trim().Equals(String.Empty))
            {
                passwordBoxSifra.Password = "";
                passwordBoxSifra.Foreground = Brushes.Black;
            }
            labelSifraGreska.Content = "";
            passwordBoxSifra.Foreground = Brushes.Black;

        }

        private void passwordBoxSifra_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBoxSifra.Password.Trim().Equals(string.Empty))
            {
                passwordBoxSifra.Password = "";
                passwordBoxSifra.Foreground = Brushes.Black;
            }
            if (!passwordBoxSifra.Password.Trim().Equals(string.Empty))
            {
                labelSifraGreska.Content = "";
                passwordBoxSifra.Foreground = Brushes.Black;
            }
        }
        #endregion
        #region Izlazak iz prozora
        private void btnClose_Click(object sender, RoutedEventArgs e)//Ako je pritisnuto dugme Close
        {
            Application.Current.Shutdown(); //Zatvori prozor
        }
        #endregion

        #region Dugme za izlaz
        private void btnIzlaz_Click(object sender, RoutedEventArgs e) //Ako je pritisnuto dugme za izlaza
        {
            this.Close(); //Izadji
            Application.Current.Shutdown(); //Zatvori aplikaciju
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

