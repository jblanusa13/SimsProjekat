using ProjectSims.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Interop;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for DetailsTourWindow.xaml
    /// </summary>
    public partial class DetailsTourWindow : Window
    {
        public DetailsTourWindow(Tour tourSelected)
        {
            InitializeComponent();
            DataContext = this;

            NameTextBox.Text = tourSelected.Name;
            LocationTextBox.Text = tourSelected.Location;
            DescriptionTextBox.Text = tourSelected.Descrption;
            LanguageTextBox.Text = tourSelected.Language;
            MaxGuestsTextBox.Text = tourSelected.MaxNumberGuests.ToString();
            KeyPointTextBox.Text = tourSelected.KeyPoints;
            DateStartTextBox.Text = tourSelected.StartOfTheTour.ToString();
            DurationTextBox.Text = tourSelected.Duration.ToString();


            //dodavanje slike uz pomoc url-a
            /*
            WebClient w = new WebClient();
            byte[] imageByte = w.DownloadData(tourSelected.Images);
            MemoryStream stream = new MemoryStream(imageByte);

            Image im = Image.FromStream(stream);
            */
            
            var image = new Image();
            var fullFilePath = tourSelected.Images;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            image.Source = bitmap;

            image.Width = 100;
            image.Height = 80;

            Image.Source = bitmap;
            

            
        }
    }
}
