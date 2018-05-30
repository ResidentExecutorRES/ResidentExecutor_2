using Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using System.Xml.Serialization;

namespace KorisnickiInterfejs
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Upit = "";
        public static string SelectUpit = "";
        public static List<PodaciIzBaze> podaciIzBaze;

        private string _geoPodrucje;
        private float _unesenaPotrosnja;
        private string _datum;
        private string _vreme;

        public string GeoPodrucje { get => _geoPodrucje; set => _geoPodrucje = value; }
        public float UnesenaPotrosnja { get => _unesenaPotrosnja; set => _unesenaPotrosnja = value; }
        public string Datum { get => _datum; set => _datum = value; }
        public string Vreme { get => _vreme; set => _vreme = value; }

        public MainWindow()
        {
            InitializeComponent();
            geoCombo.ItemsSource = GeoPodrucja.geoPodrucja.Values;
            //funkcijeCombo.ItemsSource = Funkcije.funkcije.Values;
            GeoPodrucja geo = new GeoPodrucja();
            if (!File.Exists("../../../geo_podrucja.xml"))
            {

                geo.NapraviXMLGeoPOdrucja();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                string putanja = @"../../../PodaciSaMainWindow.txt";
                string[] trimovano = _datum.Split('/');
                string datum = trimovano[2] + "-" + trimovano[0] + "-" + trimovano[1] + " " + _vreme;
                string idGeoPodrucja = "";

                foreach (var item in GeoPodrucja.geoPodrucja)
                {
                    if (item.Value == _geoPodrucje)
                        idGeoPodrucja = item.Key;
                }

                SelectUpit = "SELECT * FROM UneseneVrednosti WHERE " +
                    "VremeMerenja = '"+datum+"' AND IDGeoPodrucja = '"+idGeoPodrucja+"'";

                IConnect proxy = new ChannelFactory<IConnect>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10100/IConnect")).CreateChannel();

                 podaciIzBaze = proxy.VratiRedove(SelectUpit);

                
                if(podaciIzBaze.Count == 0)
                {

                    Upit = "INSERT INTO UneseneVrednosti " +
                       "(IDGeoPodrucja, VremeMerenja, Vrednost) " +
                       "VALUES ('" + idGeoPodrucja + "', '" + datum + "', " + _unesenaPotrosnja + ")";
                }

                if (!File.Exists(putanja))
                {
                    //string apendovan = _geoPodrucje + ";" + _unesenaPotrosnja.ToString() + ";" + _datum + ";" + _vreme;

                    

                    //2015-12-11 23:33:12.000
                    File.WriteAllText(putanja, Upit);

                    UneseniPodaci uneseni = new UneseniPodaci();
                    this.Close();
                    uneseni.ShowDialog();
                }
                else
                {
                    File.Delete(putanja);
                    //string apendovan = _geoPodrucje + ";" + _unesenaPotrosnja.ToString() + ";" + _datum + ";" + _vreme;
                    File.WriteAllText(putanja, Upit);

                    UneseniPodaci uneseni = new UneseniPodaci();
                    this.Close();
                    uneseni.ShowDialog();
                }

                

            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IzmijeniXML_Click(object sender, RoutedEventArgs e)
        {
            //NapraviXML();
            if (!File.Exists(putanja))
                NapraviXMLFunkcije();
            else
                IzmeniXML();
        }

        private bool Validate()
        {
            bool retVal = true;

            if (geoCombo.SelectedItem == null)
            {
                retVal = false;
                geoGreska.Content = "Izaberite jednu od ponudjenih vrednosti";
                geoGreska.BorderThickness = new Thickness(0);
                geoGreska.BorderBrush = Brushes.Red;
            }
            else
            {
                _geoPodrucje = geoCombo.Text.ToString();
                geoGreska.Content = "";
                geoLabel.BorderBrush = Brushes.White;
            }

            //
            if (potrosnjaTextBox.Text.Trim().Equals(""))
            {
                retVal = false;
                potrosnjaGreska.Content = "Polje ne sme biti prazno.";
                potrosnjaGreska.BorderThickness = new Thickness(0);
                potrosnjaGreska.BorderBrush = Brushes.Red;
            }
            else if (float.TryParse(potrosnjaTextBox.Text, out float potrosnja))
            {
                if (potrosnja < 0)
                {
                    retVal = false;
                    potrosnjaGreska.Content = "Potrosnja ne sme biti negativna vrednost.";
                    potrosnjaGreska.BorderThickness = new Thickness(0);
                    potrosnjaGreska.BorderBrush = Brushes.Red;

                }
                else
                {
                    _unesenaPotrosnja = potrosnja;
                    potrosnjaGreska.Content = "";
                    potrosnjaTextBox.BorderBrush = Brushes.White;
                }
            }
            else
            {
                retVal = false;
                potrosnjaGreska.Content = "Potrosnja mora biti broj";
                potrosnjaGreska.BorderThickness = new Thickness(0);
                potrosnjaGreska.BorderBrush = Brushes.Red;
            }

            if (vremeTextBox.Text.Trim().Equals("") || vremeTextBox2.Text.Trim().Equals("") || vremeTextBox3.Text.Trim().Equals(""))
            {
                retVal = false;
                vremeGreska.Content = "Sva polja za vreme morate uneti. [hh:mm:ss]";
                vremeGreska.BorderThickness = new Thickness(0);
                vremeGreska.BorderBrush = Brushes.Red;
            }
            else if (int.TryParse(vremeTextBox.Text, out int sati) &&
                    int.TryParse(vremeTextBox2.Text, out int minuti) &&
                    int.TryParse(vremeTextBox3.Text, out int sekunde))
            {
                if ((sati < 0 || sati > 24) || (minuti < 0 || minuti > 60) || (sekunde < 0 || sekunde > 60))
                {
                    retVal = false;
                    vremeGreska.Content = "Unesite validne vrednosti za vreme. [hh:mm:ss]";
                    vremeGreska.BorderThickness = new Thickness(0);
                    vremeGreska.BorderBrush = Brushes.Red;
                }
                else
                {
                    _vreme = sati.ToString() + ":" + minuti.ToString() + ":" + sekunde.ToString() + ".000";
                    vremeGreska.Content = "";
                    vremeTextBox.BorderBrush = Brushes.White;
                    vremeTextBox2.BorderBrush = Brushes.White;
                    vremeTextBox3.BorderBrush = Brushes.White;
                }

            }
            else
            {
                retVal = false;
                vremeGreska.Content = "Niste uneli validne vrednosti za vreme";
                vremeGreska.BorderThickness = new Thickness(0);
                vremeGreska.BorderBrush = Brushes.Red;
            }


            DateTime dateTime1 = datumPicker.DisplayDate;
            DateTime dateTime2 = DateTime.Now;
            DateTime dateTime3 = new DateTime();
            dateTime3.AddYears(dateTime2.Year - 5);

            if (datumPicker.Text.Equals(""))
            {
                retVal = false;
                datumGreska.Content = "Odaberite vreme";
                datumGreska.BorderThickness = new Thickness(0);
                datumGreska.BorderBrush = Brushes.Red;
            }
            else if (DateTime.Compare(dateTime1, dateTime2) > 0)
            {
                retVal = false;
                datumGreska.Content = "Nije moguc unos vremena u buducnosti";
                datumGreska.BorderThickness = new Thickness(0);
                datumGreska.BorderBrush = Brushes.Red;
            }
            else if (DateTime.Compare(dateTime1, new DateTime(2013, 1, 1)) < 0)
            {
                retVal = false;
                datumGreska.Content = "Ne postoje merenja za taj datum";
                datumGreska.BorderThickness = new Thickness(0);
                datumGreska.BorderBrush = Brushes.Red;
            }
            else
            {
                _datum = datumPicker.SelectedDate.Value.Date.ToShortDateString();
                datumGreska.Content = "";
                datumPicker.BorderBrush = Brushes.White;
            }

            return retVal;
        }

        private string putanja = @"../../../rezidentne_funkcije.xml";

        //public List<FUNKCIJA> CitajIzXML()
        //{
        //    XmlSerializer desrializer = new XmlSerializer(typeof(List<FUNKCIJA>));
        //    List<FUNKCIJA> retVal = new List<FUNKCIJA>();

        //    using (TextReader reader = new StreamReader(putanja))
        //    {
        //        object obj = desrializer.Deserialize(reader);
        //        retVal = (List<FUNKCIJA>)obj;
        //    }

        //    return retVal;
        //}

        public void IzmeniXML()
        {
            RadSaXML xmlFajl = new RadSaXML();
            List<FUNKCIJA> lista = xmlFajl.CitajIzXML();

            Random random = new Random();
            //string putanja = @"rezidentne_funkcije.xml";

            foreach (var item in lista)
            {
                item.UKLJUCENO = random.Next(2);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<FUNKCIJA>), new XmlRootAttribute("REZIDENTNE_FUNKCIJE"));
            using (TextWriter writer = new StreamWriter(putanja))
            {
                serializer.Serialize(writer, lista);
            }
        }

        public void NapraviXMLFunkcije()
        {
           
            XmlSerializer xml = new XmlSerializer(typeof(List<FUNKCIJA>), new XmlRootAttribute("REZIDENTNE_FUNKCIJE"));

            List<FUNKCIJA> lista = new List<FUNKCIJA>()
            {
                new FUNKCIJA() { ID = 1, UKLJUCENO = 1 },
                new FUNKCIJA() { ID = 2, UKLJUCENO = 1 },
                new FUNKCIJA() { ID = 3, UKLJUCENO = 0 }
            };

            using (TextWriter write = new StreamWriter(putanja))
            {
                xml.Serialize(write, lista);
            }
        }

        
    }
}
