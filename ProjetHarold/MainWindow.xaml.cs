using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetHarold
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void ProgrammeBtn_Click(object sender, RoutedEventArgs e)
        {
            Programme programme = new Programme();
            this.Content = programme;
        }

        private void ProgrammeBtn1_Click(object sender, RoutedEventArgs e)
        {
            Programme programme = new Programme();
            this.Content = programme;
        }

        

        private void HomeBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void StagaireBTN_Click(object sender, RoutedEventArgs e)
        {
            Stagiaire stagiaire = new Stagiaire();
            this.Content = stagiaire;
        }

        private void StagaireBTN2_Click_1(object sender, RoutedEventArgs e)
        {
            Stagiaire stagiaire = new Stagiaire();
            this.Content = stagiaire;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Consultation consultation = new Consultation();
            this.Content = consultation;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Consultation consultation = new Consultation();
            this.Content = consultation;
        }
    }
}
