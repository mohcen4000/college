using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjetHarold
{
    public class StagiaireItem
    {
        public string ID { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public DateTime Naissance { get; set; }
        public ProgrammeItem Programme { get; set; }
    }

    public partial class Stagiaire : Page
    {
        public List<StagiaireItem> stagiaires = new List<StagiaireItem>();
        public List<ProgrammeItem> programmes = new List<ProgrammeItem>();
        private void LoadStagiairesFromDatabase()
        {
            try
            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                MySqlCommand commandProgrammes = new MySqlCommand("SELECT * FROM programme", databaseConnection);
                MySqlCommand commandStagiaires = new MySqlCommand("SELECT * FROM stagiaire", databaseConnection);

                databaseConnection.Open();

                // Read programmes from the database
                MySqlDataReader readerProgrammes = commandProgrammes.ExecuteReader();
                while (readerProgrammes.Read())
                {
                    int programmeId = readerProgrammes.GetInt32("id_p");
                    string programmeNom = readerProgrammes.GetString("nom_p");

                    ProgrammeItem newProgramme = new ProgrammeItem
                    {
                        Id = programmeId,
                        Nom = programmeNom
                    };

                    programmes.Add(newProgramme);
                }
                readerProgrammes.Close();

                // Set the ItemsSource of ProgrammeComboBox to the list of programmes
                ProgrammeComboBox.ItemsSource = programmes;

                // Read stagiaires from the database
                MySqlDataReader myReader = commandStagiaires.ExecuteReader();

                while (myReader.Read())
                {
                    // Read data from the database
                    int stagiaireId = myReader.GetInt32("id_s");
                    string nom = myReader.GetString("nom_s");
                    string prenom = myReader.GetString("prenom_s");
                    DateTime naissance = myReader.GetDateTime("date_naissance");
                    string sexe = myReader.GetString("sexe");

                    int programmeId = myReader.GetInt32("programme_id");
                    ProgrammeItem programme = programmes.FirstOrDefault(p => p.Id == programmeId);
                    if (programme != null)
                    {
                        // Create a new StagiaireItem and add it to the list
                        StagiaireItem newStagiaire = new StagiaireItem
                        {
                            ID = stagiaireId.ToString(),
                            Nom = nom,
                            Prenom = prenom,
                            Naissance = naissance,
                            Sexe = sexe,
                            Programme = programme
                        };

                        stagiaires.Add(newStagiaire);
                    }
                    else
                    {
                        Debug.WriteLine($"Warning: No matching ProgrammeItem found for programme_id={programmeId}");
                    }
                }

                laliste.Items.Refresh(); // Refresh the UI with the updated data

                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des données depuis la base de données : " + ex.Message);
            }
            finally
            {
                // Close the database connection
            }
        }


        public Stagiaire()
        {
            InitializeComponent();

            // Instantiate the Programme class to access the list of programmes
            Programme programme = new Programme();

            // Use the GetProgrammesList method of the Programme class to get the list of programmes
            programmes = programme.GetProgrammesList();

            // Ensure that programmes is not null before calling LoadStagiairesFromDatabase
            if (programmes != null)
            {
                LoadStagiairesFromDatabase();
                laliste.ItemsSource = stagiaires;
                ProgrammeComboBox.ItemsSource = programmes;
            }
            else
            {
                MessageBox.Show("Error: Unable to retrieve the list of programmes.");
            }


        }



        public List<StagiaireItem> GetStagaireList()
        {
            return stagiaires;
        }



       



        private void ProgrammeBtn_Click(object sender, RoutedEventArgs e)
        {
            Programme programme = new Programme();
            this.NavigationService.Navigate(programme); // Utilisez la navigation pour passer à la page "Programme"
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Créez une nouvelle instance de la fenêtre MainWindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Fermez la page Programme actuelle
            Window.GetWindow(this).Close();
        }

        private void AddBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProgrammeComboBox.SelectedItem is ProgrammeItem selectedProgramme)
            {
                string nom = NomFamilleTextBox.Text;
                string prenom = PrenomTextbox.Text;
                DateTime? dateNaissance = DateNaissancePicker.SelectedDate;
                string sexe = (SexeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(); // Récupérez le sexe sélectionné

                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || dateNaissance == null || string.IsNullOrEmpty(sexe) || selectedProgramme == null)
                {
                    MessageBox.Show("Veuillez remplir tous les champs et sélectionner un programme.");
                }
                else
                {
                    // Database insertion logic
                    try
                    {
                        string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
                        MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                        MySqlCommand commandDatabase = new MySqlCommand("", databaseConnection);
                        commandDatabase.CommandTimeout = 60;

                        databaseConnection.Open();

                        string query = "INSERT INTO stagiaire (nom_s, prenom_s, date_naissance, sexe, programme_id) VALUES (@Nom, @Prenom, @DateNaissance, @Sexe, @ProgrammeId)";

                        commandDatabase.CommandText = query;
                        commandDatabase.Parameters.AddWithValue("@Nom", nom);
                        commandDatabase.Parameters.AddWithValue("@Prenom", prenom);
                        commandDatabase.Parameters.AddWithValue("@DateNaissance", dateNaissance.Value.ToString("yyyy-MM-dd")); // Format date as yyyy-MM-dd
                        commandDatabase.Parameters.AddWithValue("@Sexe", sexe);
                        commandDatabase.Parameters.AddWithValue("@ProgrammeId", selectedProgramme.Id);

                        commandDatabase.ExecuteNonQuery();

                        // If the insertion is successful, update the local list
                        StagiaireItem newStagiaire = new StagiaireItem
                        {
                            Nom = nom,
                            Prenom = prenom,
                            Naissance = dateNaissance.Value,
                            Programme = selectedProgramme,
                            Sexe = sexe
                        };

                        stagiaires.Add(newStagiaire);
                        laliste.Items.Refresh();

                        MessageBox.Show("Stagiaire ajouté avec succès à la base de données.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur : " + ex.Message);
                    }
                    finally
                    {
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs et sélectionner un programme.");
            }
        }

        private void ModifierBtn_Click(object sender, RoutedEventArgs e)
        {
            if (laliste.SelectedItem != null)
            {
                StagiaireItem stagiaireSelectionne = laliste.SelectedItem as StagiaireItem;

                if (stagiaireSelectionne != null)
                {
                    if (ProgrammeComboBox.SelectedItem is ProgrammeItem selectedProgramme)
                    {
                        string nom = NomFamilleTextBox.Text;
                        string prenom = PrenomTextbox.Text;
                        DateTime? dateNaissance = DateNaissancePicker.SelectedDate;
                        string sexe = (SexeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(); // Récupérez le sexe sélectionné

                        if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || dateNaissance == null || string.IsNullOrEmpty(sexe) || selectedProgramme == null)
                        {
                            MessageBox.Show("Veuillez remplir tous les champs et sélectionner un programme.");
                        }
                        else
                        {
                            // Print the data for debugging
                            Debug.WriteLine($"Data for modification:\nNom: {nom}\nPrénom: {prenom}\nDate de naissance: {dateNaissance:dd/MM/yyyy}\nSexe: {sexe}\nProgramme: {selectedProgramme.Id}");

                            string confirmationMessage = $"Confirmez la modification du stagiaire :\nNom : {nom}\nPrénom : {prenom}\nDate de naissance : {dateNaissance:dd/MM/yyyy}\nSexe : {sexe}\nProgramme : {selectedProgramme.Nom}";

                            if (MessageBox.Show(confirmationMessage, "Confirmation de modification", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                // Update the UI
                                stagiaireSelectionne.Nom = nom;
                                stagiaireSelectionne.Prenom = prenom;
                                stagiaireSelectionne.Naissance = dateNaissance.Value;
                                stagiaireSelectionne.Programme = selectedProgramme;
                                stagiaireSelectionne.Sexe = sexe;
                                laliste.Items.Refresh();

                                // Update the database
                                try
                                {
                                    string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
                                    using (MySqlConnection databaseConnection = new MySqlConnection(connectionString))
                                    {
                                        databaseConnection.Open();

                                        string query = "UPDATE stagiaire SET nom_s = @Nom, prenom_s = @Prenom, date_naissance = @DateNaissance, sexe = @Sexe, programme_id = @ProgrammeId WHERE id_s = @StagiaireId";
                                        using (MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection))
                                        {
                                            commandDatabase.Parameters.AddWithValue("@Nom", nom);
                                            commandDatabase.Parameters.AddWithValue("@Prenom", prenom);
                                            commandDatabase.Parameters.AddWithValue("@DateNaissance", dateNaissance.Value.ToString("yyyy-MM-dd"));
                                            commandDatabase.Parameters.AddWithValue("@Sexe", sexe);
                                            commandDatabase.Parameters.AddWithValue("@ProgrammeId", selectedProgramme.Id);
                                            commandDatabase.Parameters.AddWithValue("@StagiaireId", stagiaireSelectionne.ID);

                                            commandDatabase.ExecuteNonQuery();
                                        }
                                    }

                                    MessageBox.Show("Stagiaire modifié avec succès dans la base de données.");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Erreur lors de la modification du stagiaire dans la base de données : " + ex.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez remplir tous les champs et sélectionner un programme.");
                    }
                }
            }
        }



        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (laliste.SelectedItem != null)
            {
                StagiaireItem stagiaireSelectionne = laliste.SelectedItem as StagiaireItem;

                if (stagiaireSelectionne != null)
                {
                    // Affichez les informations dans une boîte de dialogue de confirmation
                    string confirmationMessage = $"Confirmez la suppression du stagiaire :\nNom : {stagiaireSelectionne.Nom}\nPrénom : {stagiaireSelectionne.Prenom}\nDate de naissance : {stagiaireSelectionne.Naissance:dd/MM/yyyy}\nSexe : {stagiaireSelectionne.Sexe}\nProgramme : {stagiaireSelectionne.Programme.Nom}";

                    if (MessageBox.Show(confirmationMessage, "Confirmation de suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // Remove from UI
                        stagiaires.Remove(stagiaireSelectionne);
                        laliste.Items.Refresh(); // Actualisez la liste

                        // Remove from database
                        try
                        {
                            string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
                            using (MySqlConnection databaseConnection = new MySqlConnection(connectionString))
                            {
                                databaseConnection.Open();

                                string query = "DELETE FROM stagiaire WHERE id_s = @StagiaireId"; // Adjusted the column name
                                using (MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection))
                                {
                                    commandDatabase.Parameters.AddWithValue("@StagiaireId", stagiaireSelectionne.ID);

                                    commandDatabase.ExecuteNonQuery();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de la suppression du stagiaire de la base de données : " + ex.Message);
                        }
                    }
                }
            }
        }


        private void ResetBTN_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialisez tous les TextBox et autres contrôles à leurs valeurs par défaut

            NomFamilleTextBox.Text = ""; // Réinitialisez le TextBox "Nom"
            PrenomTextbox.Text = ""; // Réinitialisez le TextBox "Prénom"
            DateNaissancePicker.SelectedDate = null; // Réinitialisez le DatePicker "Date de naissance"
            SexeComboBox.SelectedIndex = -1; // Réinitialisez le ComboBox "Sexe" en sélectionnant la première option
            ProgrammeComboBox.SelectedIndex = -1; // Réinitialisez le ComboBox "Programme" en sélectionnant la première option

           
        }

        private void ProgrammeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
