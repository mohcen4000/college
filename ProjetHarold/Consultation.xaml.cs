using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetHarold
{





    public class ConsultationItem
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime Naissance { get; set; }
        public string Sexe { get; set; }

        public ProgrammeItem Programme { get; set; }
    }
    /// <summary>
    /// Logique d'interaction pour Consultation.xaml
    /// </summary>
    public partial class Consultation : Page
    {
        public List<ConsultationItem> consultation;
        private Stagiaire stagiairePage;
        private IEnumerable<ConsultationItem> filteredStudents;


        public Consultation()
        {
            InitializeComponent();

            // Initialize the consultation list
            consultation = new List<ConsultationItem>();

            // Initialize the list of programs
            ProgramComboBox.ItemsSource = GetProgrammesList();

            try
            {
                // Establish a database connection
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
                using (MySqlConnection databaseConnection = new MySqlConnection(connectionString))
                {
                    databaseConnection.Open();

                    // Fetch data from the database using a JOIN query
                    string query = "SELECT s.nom_s, s.prenom_s, s.date_naissance, s.sexe, s.Programme_id, p.nom_p AS ProgrammeNom " +
                                   "FROM stagiaire s " +
                                   "JOIN programme p ON s.Programme_id = p.id_p";
                    using (MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection))
                    {
                        using (MySqlDataReader reader = commandDatabase.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Map database columns to ConsultationItem properties
                                ConsultationItem item = new ConsultationItem
                                {
                                    Nom = reader["nom_s"].ToString(),
                                    Prenom = reader["prenom_s"].ToString(),
                                    Naissance = Convert.ToDateTime(reader["date_naissance"]),
                                    Sexe = reader["sexe"].ToString(),
                                    Programme = new ProgrammeItem { Nom = reader["ProgrammeNom"].ToString() }
                                };

                                // Add the item to the consultation list
                                consultation.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données depuis la base de données : " + ex.Message);
            }

            // Set the item source for lalisteConsultation
            lalisteConsultation.ItemsSource = consultation;
        }




        private List<ProgrammeItem> GetProgrammesList()
        {
            Programme programmePage = new Programme();
            return programmePage.GetProgrammesList();
        }

        private void ProgramComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProgrammeItem selectedProgram = ProgramComboBox.SelectedItem as ProgrammeItem;

            if (selectedProgram == null)
            {
                // Si aucun programme n'est sélectionné, affichez la liste complète des étudiants
                lalisteConsultation.ItemsSource = consultation;
                // Mettez à jour la collection des étudiants filtrés
                filteredStudents = consultation;
            }
            else
            {
                // Filtrer la liste des étudiants en fonction du programme sélectionné
                filteredStudents = consultation.Where(student => student.Programme.Nom == selectedProgram.Nom).ToList();
                lalisteConsultation.ItemsSource = filteredStudents;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                // Si la zone de recherche est vide, affichez les étudiants filtrés (après le filtre par programme)
                lalisteConsultation.ItemsSource = filteredStudents;
            }
            else
            {
                // Effectuez la recherche parmi les étudiants filtrés
                var searchResults = filteredStudents.Where(student => student.Nom.ToLower().StartsWith(searchTerm)).ToList();
                lalisteConsultation.ItemsSource = searchResults;
            }

            // Réinitialisez le champ de recherche
            SearchTextBox.Text = string.Empty;
        }






        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Créez une nouvelle instance de la fenêtre MainWindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Fermez la page Programme actuelle
            Window.GetWindow(this).Close();
        }

        private void ResetBtn3_Click(object sender, RoutedEventArgs e)
        {
            // Efface le texte de la TextBox
            SearchTextBox.Text = "";

            // Réinitialise la sélection de la ComboBox en la définissant sur l'élément par défaut (index 0)
            ProgramComboBox.SelectedIndex = -1;
        }
    }
}

