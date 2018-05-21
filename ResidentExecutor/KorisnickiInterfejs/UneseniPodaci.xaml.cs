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

namespace KorisnickiInterfejs
{
    /// <summary>
    /// Interaction logic for UneseniPodaci.xaml
    /// </summary>
    public partial class UneseniPodaci : Window
    {
        public UneseniPodaci()
        {
            InitializeComponent();
            //PristigliPodaci();
        }

        public void PristigliPodaci()
        {
            //MainWindow mainWindow = new MainWindow();

            string[] trimovano = File.ReadAllText(@"../../../PodaciSaMainWindow.txt").Split(';');

            //Banjaluka;12;5/2/2018;12:13:14.000;False;AVG
            geoBox.Content = trimovano[0];
            potrosnjaBox.Content = trimovano[1];
            datumBox.Content = trimovano[2];
            vremeBox.Content = trimovano[3];

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow vratiSeNazad = new MainWindow();
            this.Close();
            vratiSeNazad.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
