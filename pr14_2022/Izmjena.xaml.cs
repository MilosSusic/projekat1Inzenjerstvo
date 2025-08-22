using Microsoft.Win32;
using Notification.Wpf;
using pr14_2022.MODELS;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace pr14_2022
{
    /// <summary>
    /// Interaction logic for Izmjena.xaml
    /// </summary>
    public partial class Izmjena : Window
    {
        #region Pomocna polja
        private int index = 0;
        private string pomoc = "";
        private string fajl_pomocni = "";
        private string slika_pomocna = "";
        private readonly NotificationManager notificationManager = new NotificationManager();
        #endregion

        public Izmjena(int idx)
        {
            InitializeComponent();

            #region Podešavanje početnih vrijednosti

            Film films = MainWindow.Films[idx];
            index = idx;

            ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            ComboBoxColor.ItemsSource = typeof(Colors).GetProperties();
            ComboBoxColor.SelectedItem = typeof(Colors).GetProperties()[5];
            ComboBoxColor.SelectedIndex = 7;
            ComboBoxFamily.SelectedIndex = 2;
            textBoxSlika.Text = "";
            textBoxSlika.Visibility = Visibility.Hidden;


           slika_pomocna = films.Slika;
            Uri uri = new Uri(films.Slika);
            imgSlika.Source = new BitmapImage(uri);

           fajl_pomocni = films.Fajl;

            textBoxNaziv.Text = films.nazivFilma;
           textBoxBroj.Text = Convert.ToString(films.ocjenaFilma);

            TextRange textRange;
            System.IO.FileStream fileStream;

            if (System.IO.File.Exists(fajl_pomocni))
            {
                textRange = new TextRange(richTextBoxFilms.Document.ContentStart, richTextBoxFilms.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(fajl_pomocni, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }

            }

            #endregion

        }
        #region	 Dugme za izlaz
        private void btnIzađi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region	Promjena u RichTextBoxu

        private void richTextBoxFilms_SelectionChanged(object sender, RoutedEventArgs e)
        {

            object temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.FontStyleProperty);
            tglButtonItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));    //italic

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.FontWeightProperty);
            tglButtonBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));      //bold

            temp = richTextBoxFilms.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            tglButtonUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));   //underline

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

        #region Promjena velicina slova
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


        #region Funckija za prebrojavanje rijeci
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


        #region Kod promjene teksta, poziva se funkcija za prebrojavanje
        private void richTextBoxFilms_TextChanged(object sender, TextChangedEventArgs e)
        {
            BrojRijeci();

        }
        #endregion

        #region Dugme za izmjenu slike
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            textBoxSlika.Text = "";
            if (openFileDialog.ShowDialog() == true)
            {
                pomoc = openFileDialog.FileName;
                Uri uri = new Uri(pomoc);
                imgSlika.Source = new BitmapImage(uri);
                textBoxSlika.Text = "";
            }
        }
        #endregion


        #region Dugme za izmjenu filma
        private void btnIzmijeni_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                File.Delete(fajl_pomocni);

                if (pomoc == "") //provjera da li je slika uopste promijenjena, ako nije ostaje nam ista slika
                {
                    pomoc = slika_pomocna;
                }

               MainWindow.Films[index] = new Film(Int32.Parse(textBoxBroj.Text), textBoxNaziv.Text, DateTime.Now, pomoc, fajl_pomocni);

                TextRange textRange;
                FileStream fileStream;
                textRange = new TextRange(richTextBoxFilms.Document.ContentStart, richTextBoxFilms.Document.ContentEnd);
                fileStream = new FileStream(fajl_pomocni, FileMode.Create);
                textRange.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
                var toast = new ToastNotification("Uspjesnost", "Uspjesno ste izmjenili film", NotificationType.Success);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "IzmjenaNotificationArea");
               // this.Close();
            }
            else
            {
                var toast = new ToastNotification("Greska", "Niste uspjesno izmjenili film", NotificationType.Error);
                notificationManager.Show(toast.Title, toast.Message, toast.Type, "IzmjenaNotificationArea");
                textBoxNaziv.Foreground = Brushes.Black;
                textBoxBroj.Foreground = Brushes.Black;
            }
        }

        #endregion

        #region Validacija unosa
        private bool validate()
        {
            bool result = true;

            if (textBoxNaziv.Text.Trim().Equals("") || textBoxNaziv.Text.Trim().Equals("Unesite naziv filma"))
            {
                result = false;
                textBoxNaziv.Foreground = Brushes.Red;
                textBoxNaziv.BorderBrush = Brushes.Red;
                textBoxNaziv.BorderThickness = new Thickness(1);
                textBoxGreskaNaziv.Text = "Obavezno popuniti!";
                textBoxGreskaNaziv.Foreground = Brushes.Red;
            }
            else
            {
                textBoxNaziv.Foreground = Brushes.Black;
                textBoxNaziv.BorderBrush = Brushes.Green;
                textBoxNaziv.BorderThickness = new Thickness(1);
                textBoxGreskaNaziv.Text = "";
                textBoxGreskaNaziv.Foreground = Brushes.Gray;

            }

            if (textBoxBroj.Text.Trim().Equals("") || textBoxBroj.Text.Trim().Equals("Unesite ocjenu filma"))
            {
                result = false;
                textBoxBroj.Foreground = Brushes.Red;
                textBoxBroj.BorderBrush = Brushes.Red;
                textBoxBroj.BorderThickness = new Thickness(1);
                textBoxGreskaBroj.Text = "Obavezno popuniti!";
                textBoxGreskaBroj.Foreground = Brushes.Red;
            }
            else
            {

                bool broj = int.TryParse(textBoxBroj.Text, out _);
                if (broj)
                {
                    if (Int32.Parse(textBoxBroj.Text) > 0 && Int32.Parse(textBoxBroj.Text) <= 10)
                    {
                        textBoxBroj.Foreground = Brushes.Black;
                        textBoxBroj.BorderBrush = Brushes.Green;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.BorderThickness = new Thickness(0);
                        textBoxGreskaBroj.Text = "";
                    }
                    else if (Int32.Parse(textBoxBroj.Text) <= 0)
                    {
                        result = false;
                        textBoxBroj.Foreground = Brushes.Red;
                        textBoxBroj.BorderBrush = Brushes.Red;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "Unesite ocjenu vecu od 1!";
                        textBoxGreskaBroj.Foreground = Brushes.Red;
                    }
                    else
                    {
                        result = false;
                        textBoxBroj.Foreground = Brushes.Red;
                        textBoxBroj.BorderBrush = Brushes.Red;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "Unesite ocjenu od 0 do 10";
                        textBoxGreskaBroj.Foreground = Brushes.Red;
                    }

                }
                else
                {
                    result = false;
                    textBoxBroj.Foreground = Brushes.Red;
                    textBoxBroj.BorderBrush = Brushes.Red;
                    textBoxBroj.BorderThickness = new Thickness(1);
                    textBoxGreskaBroj.Text = "Unesite ocjenu!";
                    textBoxGreskaBroj.BorderThickness = new Thickness(1);
                    textBoxGreskaBroj.Foreground = Brushes.Red;

                }

            }
            if (richTextBoxText.Text.Trim().Equals(""))
            {
                result = false;
                richTextBoxText.Text = "Obavezno polje!";
                richTextBoxText.Foreground = Brushes.Red;
                richTextBoxFilms.BorderBrush = Brushes.Red;
                richTextBoxFilms.BorderThickness = new Thickness(1);

            }
            else
            {
                richTextBoxText.Foreground = Brushes.Black;
                richTextBoxFilms.BorderBrush = Brushes.Green;
            }


            return result;
        }

        #endregion

        #region Datum
        private void trenutniDatum_MouseEnter(object sender, MouseEventArgs e)
        {
            trenutniDatum.Text = DateTime.Now.ToString();
            trenutniDatum.IsEnabled = false;
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
