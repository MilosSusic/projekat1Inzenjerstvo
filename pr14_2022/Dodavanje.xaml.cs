using Microsoft.Win32;
using Notification.Wpf;
using pr14_2022.MODELS;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Dodavanje.xaml
    /// </summary>
    public partial class Dodavanje : Window
    {

        private readonly NotificationManager notificationManager = new NotificationManager();
        private string slika = "";
        public Dodavanje()
        {
            #region Podešavanje početnih vrijednosti
            InitializeComponent();
            textBoxNaziv.Text = "Unesite naziv filma";
            textBoxNaziv.Foreground = Brushes.Gray;

            textBoxBroj.Text = "Unesite ocjenu filma";
            textBoxBroj.Foreground = Brushes.Gray;



            ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            ComboBoxFamily.SelectedIndex = 2;
            ComboBoxColor.ItemsSource = typeof(Colors).GetProperties();
            ComboBoxColor.SelectedItem = typeof(Colors).GetProperties()[5];
            ComboBoxColor.SelectedIndex = 7;
            #endregion
        }


        #region Dugme za izlaz
        private void btnIzađi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Dugme za dodavanje filma
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                if (btnDodaj.Content.Equals("Dodaj"))
                {
                    string naziv = "";
                    naziv = textBoxNaziv.Text + ".rtf";

                    var toast = new ToastNotification("Uspjesnost", "Uspjesno ste dodali film", NotificationType.Success);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");

                    TextRange textRange;
                    FileStream fileStream;
                    textRange = new TextRange(richTextBoxFilms.Document.ContentStart, richTextBoxFilms.Document.ContentEnd);
                    fileStream = new FileStream(naziv, FileMode.Create);
                    textRange.Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                   // this.Close();

                    Film film = new Film(Int32.Parse(textBoxBroj.Text), textBoxNaziv.Text, DateTime.Now, slika, naziv);
                  MainWindow.Films.Add(film);

                }

            }
            else
            {
                var toast = new ToastNotification("Greska", "Morate popuniti sva polja.", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");
            }

        }
        #endregion

        #region Validacija unosa
        private bool validate()
        {
            bool result = true;

            #region Naziv
            if (textBoxNaziv.Text.Trim().Equals("") || textBoxNaziv.Text.Trim().Equals("Unesite naziv filma"))
            {
                result = false;
                textBoxNaziv.Foreground = Brushes.Red;
                textBoxNaziv.BorderBrush = Brushes.Red;
                textBoxNaziv.BorderThickness = new Thickness(1);
                textBoxGreskaNaziv.Text = "Obavezno popuniti!";
                textBoxGreskaNaziv.Foreground = Brushes.Red;
                var toast = new ToastNotification("Greska", "Morate popuniti sva polja.", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");
            }
            else
            {
                textBoxNaziv.Foreground = Brushes.Black;
                textBoxNaziv.BorderBrush = Brushes.Green;
                textBoxNaziv.BorderThickness = new Thickness(1);
                textBoxGreskaNaziv.Text = "";
                textBoxGreskaNaziv.Foreground = Brushes.Gray;

            }
            #endregion


            #region Ocjena filma
            if (textBoxBroj.Text.Trim().Equals("") || textBoxBroj.Text.Trim().Equals("Unesite ocjenu filma"))
            {
                result = false;
                textBoxBroj.Foreground = Brushes.Red;
                textBoxBroj.BorderBrush = Brushes.Red;
                textBoxBroj.BorderThickness = new Thickness(1);
                textBoxGreskaBroj.Text = "Obavezno popuniti!";
                textBoxGreskaBroj.Foreground = Brushes.Red;
                var toast = new ToastNotification("Greska", "Morate popuniti sva polja.", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");

            }
            else
            {

                bool broj = int.TryParse(textBoxBroj.Text, out _);
                if (broj)
                {
                    if (Int32.Parse(textBoxBroj.Text) > 0 && Int32.Parse(textBoxBroj.Text) <=10)
                    {
                        textBoxBroj.Foreground = Brushes.Black;
                        textBoxBroj.BorderBrush = Brushes.Green;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "";
                        textBoxGreskaBroj.Foreground = Brushes.Gray;
                    }
                    else if (Int32.Parse(textBoxBroj.Text) <= 0)
                    {
                        result = false;
                        textBoxBroj.Foreground = Brushes.Red;
                        textBoxBroj.BorderBrush = Brushes.Red;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "Unesite ocenu u opsegu 1 do 10!";
                        textBoxGreskaBroj.Foreground = Brushes.Red;
                    }
                    else
                    {
                        result = false;
                        textBoxBroj.Foreground = Brushes.Red;
                        textBoxBroj.BorderBrush = Brushes.Red;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "Unesite ocjenu do 10!";
                        textBoxGreskaBroj.Foreground = Brushes.Red;
                    }
                }
                else
                {
                    result = false;
                    textBoxBroj.Foreground = Brushes.Red;
                    textBoxBroj.BorderBrush = Brushes.Red;
                    textBoxBroj.BorderThickness = new Thickness(1);
                    textBoxGreskaBroj.Text = "Niste unijeli ocjenu!";
                    textBoxGreskaBroj.Foreground = Brushes.Red;
                    var toast = new ToastNotification("Greska", "Morate popuniti sva polja.", NotificationType.Error);
                    notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");


                }
            }
            #endregion

            #region Slika

            if (textBoxSlika.Text.Trim().Equals("Slika"))
            {
                result = false;
                borderSlika.BorderBrush = Brushes.Red;
                borderSlika.BorderThickness = new Thickness(1);
                labelaGreskaSlika.Content = "Slika obavezna!";
                labelaGreskaSlika.Foreground = Brushes.Red;
                labelaGreskaSlika.BorderThickness = new Thickness(1);
                textBoxSlika.Text = "";
                var toast = new ToastNotification("Greska", "Morate popuniti sva polja.", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");
            }
            else
            {
                borderSlika.BorderBrush = Brushes.Green;
                borderSlika.BorderThickness = new Thickness(0);
                labelaGreskaSlika.BorderThickness = new Thickness(0);
                labelaGreskaSlika.Content = "";
                textBoxSlika.Text = "";
            }


            #endregion

            #region RichTextBox
            if (richTextBoxText.Text.Trim().Equals("Unesite opis vaseg filma") || richTextBoxText.Text.Trim().Equals(""))
            {
                result = false;
                richTextBoxText.Text = "Obavezno polje!";
                richTextBoxText.Foreground = Brushes.Red;
                richTextBoxFilms.BorderBrush = Brushes.Red;
                richTextBoxFilms.BorderThickness = new Thickness(1);
                var toast = new ToastNotification("Greska", "Morate popuniti sva polja.", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "DodavanjeNotificationArea");
            }
            else
            {
                richTextBoxText.Foreground = Brushes.Black;
                richTextBoxFilms.BorderBrush = Brushes.Gray;
            }
            #endregion


            return result;
        }
        #endregion

        #region TextBox Naziv

        private void textBoxNaziv_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNaziv.Text.Trim().Equals("Unesite naziv filma"))
            {
                textBoxNaziv.Text = "";
                textBoxNaziv.Foreground = Brushes.Black;
            }

        }

        private void textBoxNaziv_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNaziv.Text.Trim().Equals(string.Empty))
            {
                textBoxNaziv.Text = "Unesite naziv filma";
                textBoxNaziv.Foreground = Brushes.Gray;
            }

        }
        #endregion


        #region TextBox Ocjena filma

        private void textBoxBroj_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxBroj.Text.Trim().Equals("Unesite ocjenu filma"))
            {
                textBoxBroj.Text = "";
                textBoxBroj.Foreground = Brushes.Black;
            }

        }

        private void textBoxBroj_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxBroj.Text.Trim().Equals(string.Empty))
            {
                textBoxBroj.Text = "Unesite ocjenu filma";
                textBoxBroj.Foreground = Brushes.Gray;
            }

        }

        #endregion

        #region RichTextBox opis filma

        private void richTextBoxFilms_GotFocus(object sender, RoutedEventArgs e)
        {
            if (richTextBoxText.Text.Trim().Equals("Unesite opis filma") || richTextBoxText.Text.Trim().Equals("Obavezno polje!"))
            {
                richTextBoxText.Text = "";
                richTextBoxText.Foreground = Brushes.Black;
            }
        }

        private void richTextBoxFilms_LostFocus(object sender, RoutedEventArgs e)
        {
            if (richTextBoxText.Text.Trim().Equals(String.Empty))
            {
                richTextBoxText.Text = "Unesite opis filma";
                richTextBoxText.Foreground = Brushes.Gray;
            }
        }
        #endregion

        #region Datum
        private void trenutniDatum_MouseEnter(object sender, MouseEventArgs e)
        {
            trenutniDatum.Text = DateTime.Now.ToString();
            trenutniDatum.IsEnabled = false;
        }
        #endregion

        #region Dugme za dodavanje slike
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            borderSlika.Visibility = Visibility.Hidden;
            labelaGreskaSlika.Visibility = Visibility.Hidden;
            textBoxSlika.Visibility = Visibility.Hidden;
            textBoxSlika.Text = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                slika = openFileDialog.FileName;
                Uri fileUri = new Uri(slika);
                imgSlika.Source = new BitmapImage(fileUri);
            }
        }
        #endregion

        #region Promjena u RichTextBoxu

        private void richTextBoxFilms_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.FontStyleProperty);
            tglButtonItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.FontWeightProperty);
            tglButtonBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            tglButtonUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            ComboBoxFamily.SelectedItem = temp;

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.FontSizeProperty);
            ComboBoxSize.Text = temp.ToString();

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.ForegroundProperty);

        }
        #endregion

        #region Promjena tipa slova
        private void ComboBoxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (ComboBoxFamily.SelectedItem != null && !richTextBoxFilms.Selection.IsEmpty)
            {
                richTextBoxFilms.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
            }
        }

        #endregion

        #region Promjena veličine slova
        private void ComboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ComboBoxSize.SelectedValue != null && !richTextBoxFilms.Selection.IsEmpty)
            {
                richTextBoxFilms.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
            }
        }
        #endregion

        #region Promjena boje slova
        private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             if (ComboBoxColor.SelectedItem is System.Reflection.PropertyInfo selectedProperty)
             {

                 try
                 {
                     // Izvuci boju iz reflektovane osobine (npr. Colors.Red)
                     var color = (Color)selectedProperty.GetValue(null);
                     var brush = new SolidColorBrush(color);

                     var selection = richTextBoxFilms.Selection;

                     if (!selection.IsEmpty)
                     {
                         // Promeni boju selektovanog teksta
                         selection.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
                     }
                     else
                     {
                         // Promeni boju za buduće kucanje (na poziciji kursor-a)
                         var caret = richTextBoxFilms.CaretPosition;
                         var run = caret.Parent as Run;

                         if (run != null)
                         {
                             run.Foreground = brush;
                         }
                         else
                         {
                             // Ako ne postoji aktivni Run, napravi novi
                             var newRun = new Run() { Foreground = brush };
                             caret.InsertTextInRun("");
                             caret.Paragraph?.Inlines.Add(newRun);
                             richTextBoxFilms.CaretPosition = newRun.ContentStart;
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Greška pri izboru boje: " + ex.Message);
                 }
             }
        }
        #endregion



        #region Funkcija za prebrojavanje broja rijeci
        private void BrojRijeci()
        {
            int brojRijeci = 0;
            int index = 0;
            string richText = new TextRange(richTextBoxFilms.Document.ContentStart, richTextBoxFilms.Document.ContentEnd).Text;

            while (index < richText.Length && char.IsWhiteSpace(richText[index]))
            {
                index++;
            }

            while (index < richText.Length)
            {
                while (index < richText.Length && !char.IsWhiteSpace(richText[index]))
                {
                    index++;
                }

                brojRijeci++;

                while (index < richText.Length && char.IsWhiteSpace(richText[index]))
                {
                    index++;
                }
            }
            TextBlockBrojReci.Text = brojRijeci.ToString();
        }
        #endregion

        #region Prikaz broja izbrojanih rijeci
        private void richTextBoxBarselona_TextChanged(object sender, TextChangedEventArgs e)
        {
            BrojRijeci();

        }
        #endregion

        #region Pomjeranje prozora
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
    }
}

