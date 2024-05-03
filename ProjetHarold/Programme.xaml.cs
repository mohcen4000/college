using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace ProjetHarold
{
    public class ProgrammeItem
    {
        public int Id { get; set; }  
        public string Nom { get; set; }
        public string Cote { get; set; }
        public string Duree { get; set; }
        public string Cout { get; set; }
    }


    public partial class Programme : Page
    {
        public ProgrammeItem programmeSelectionne;
        public List<ProgrammeItem> programmes;

        public Programme()
        {
            InitializeComponent();
            programmes = new List<ProgrammeItem>();
            laliste.ItemsSource = programmes;

            // Load data from the database when the page is loaded
            LoadDataFromDatabase();
        }
        public void LoadDataFromDatabase()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM programme", databaseConnection);

            try
            {
                databaseConnection.Open();

                using (MySqlDataReader reader = commandDatabase.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProgrammeItem item = new ProgrammeItem
                        {
                            Id = Convert.ToInt32(reader["id_p"]),
                            Nom = reader["nom_p"].ToString(),
                            Cote = reader["cote"].ToString(),
                            Duree = reader["duree"].ToString(),
                            Cout = "$" + reader["cout"].ToString()
                        };

                        programmes.Add(item);
                    }

                    // Refresh the list to reflect the changes
                    laliste.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseConnection.Close();
            }
        }


        public List<ProgrammeItem> GetProgrammesList()
        {
            List<ProgrammeItem> programmes = new List<ProgrammeItem>();

            try
            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand("SELECT * FROM programme", databaseConnection);

                databaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int programmeId = reader.GetInt32("id_p");
                    string programmeNom = reader.GetString("nom_p");

                    ProgrammeItem newProgramme = new ProgrammeItem
                    {
                        Id = programmeId,
                        Nom = programmeNom
                    };

                    programmes.Add(newProgramme);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving programmes from the database: " + ex.Message);
            }

            return programmes;
        }

        private void ModifierBtn_Click(object sender, RoutedEventArgs e)
        {
            if (laliste.SelectedItem != null)
            {
                ProgrammeItem programmeSelectionne = laliste.SelectedItem as ProgrammeItem;

                if (programmeSelectionne != null)
                {
                    // Add data verification
                    if (string.IsNullOrWhiteSpace(NomTextBox.Text) || string.IsNullOrWhiteSpace(CoteTextbox.Text) || string.IsNullOrWhiteSpace(DureeTextbox.Text) || string.IsNullOrWhiteSpace(CoutTextbox.Text))
                    {
                        MessageBox.Show("Veuillez remplir tous les champs.");
                    }
                    else
                    {
                        // Validation du numéro de programme (Cote)
                        if (!CoteTextbox.Text.All(char.IsDigit) || CoteTextbox.Text.Length != 7)
                        {
                            MessageBox.Show("Veuillez entrer un numéro de programme valide (7 chiffres).");
                            return;
                        }

                        // Validation du nom du programme
                        if (!NomTextBox.Text.All(char.IsLetter))
                        {
                            MessageBox.Show("Veuillez entrer un nom de programme valide (lettres uniquement).");
                            return;
                        }

                        // Validation de la durée du programme
                        if (!int.TryParse(DureeTextbox.Text, out int duree) || duree < 0 || duree > 5)
                        {
                            MessageBox.Show("Veuillez entrer une durée de programme valide (entre 0 et 5 ans).");
                            return;
                        }

                        try
                        {
                            double cout = double.Parse(CoutTextbox.Text);

                            // Display a confirmation message
                            string confirmationMessage = $"Confirmez la modification du programme :\nNom : {NomTextBox.Text}\nCote : {CoteTextbox.Text}\nDuree : {DureeTextbox.Text}\nCout : {CoutTextbox.Text}";

                            if (MessageBox.Show(confirmationMessage, "Confirmation de modification", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            {
                                // User confirmed the modification, update the properties
                                programmeSelectionne.Nom = NomTextBox.Text;
                                programmeSelectionne.Cote = CoteTextbox.Text;
                                programmeSelectionne.Duree = DureeTextbox.Text;
                                programmeSelectionne.Cout = cout.ToString();

                                // Update the corresponding record in the database
                                UpdateDatabase(programmeSelectionne);

                                // Refresh the list to reflect the changes
                                laliste.Items.Refresh();
                            }
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Veuillez entrer une valeur numérique pour le champ Cout.");
                        }
                    }
                }
            }
        }

        private void UpdateDatabase(ProgrammeItem programme)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("", databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                string query = "UPDATE Programme SET nom_p = @Nom, duree = @Duree, cote = @Cote, cout = @Cout WHERE id_p = @Id";

                commandDatabase.CommandText = query;
                commandDatabase.Parameters.AddWithValue("@Nom", programme.Nom);
                commandDatabase.Parameters.AddWithValue("@Duree", programme.Duree);
                commandDatabase.Parameters.AddWithValue("@Cote", programme.Cote);
                commandDatabase.Parameters.AddWithValue("@Cout", programme.Cout);
                commandDatabase.Parameters.AddWithValue("@Id", programme.Id);

                commandDatabase.ExecuteNonQuery();

                MessageBox.Show("Program updated successfully in the database.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseConnection.Close();
            }
        }






        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string nom = NomTextBox.Text;
            string cote = CoteTextbox.Text;
            string dureeText = DureeTextbox.Text;
            string coutText = CoutTextbox.Text;

            // Validation des champs non vides
            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(cote) || string.IsNullOrWhiteSpace(dureeText) || string.IsNullOrWhiteSpace(coutText))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Validation du numéro de programme (Cote)
            if (!CoteTextbox.Text.All(char.IsDigit) || CoteTextbox.Text.Length != 7)
            {
                MessageBox.Show("Veuillez entrer un numéro de programme valide (7 chiffres).");
                return;
            }

            // Validation du nom du programme
            if (!nom.All(char.IsLetter))
            {
                MessageBox.Show("Veuillez entrer un nom de programme valide (lettres uniquement).");
                return;
            }

            // Validation de la durée du programme
            if (!int.TryParse(dureeText, out int duree) || duree < 0 || duree > 5)
            {
                MessageBox.Show("Veuillez entrer une durée de programme valide (entre 0 et 5 ans).");
                return;
            }

            // Validation du champ cout
            if (!double.TryParse(coutText, out double cout))
            {
                MessageBox.Show("Veuillez entrer une valeur numérique pour le champ Cout.");
                return;
            }

            // Check if the cote already exists in the database
            if (IsCoteAlreadyExists(cote))
            {
                MessageBox.Show("Le numéro de programme (Cote) doit être unique.");
                return;
            }


            // Affichage d'un message de confirmation
            string confirmationMessage = $"Confirmez l'ajout du programme :\nNom : {nom}\nCote : {cote}\nDuree : {duree}\nCout : {cout}";

            if (MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Ajout du nouveau programme
                ProgrammeItem newProgramme = new ProgrammeItem
                {
                    Nom = nom,
                    Cote = cote,
                    Duree = duree.ToString() + " ans", // Store as string
                    Cout = "$" + cout.ToString() // Store as string
                };

                programmes.Add(newProgramme);
                // Actualisation de la liste pour refléter les changements
                InsertIntoDatabase(newProgramme);

                laliste.Items.Refresh();
            }
        }

        private bool IsCoteAlreadyExists(string cote)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("", databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                string query = "SELECT COUNT(*) FROM Programme WHERE cote = @Cote";
                commandDatabase.CommandText = query;
                commandDatabase.Parameters.AddWithValue("@Cote", cote);

                int count = Convert.ToInt32(commandDatabase.ExecuteScalar());

                return count > 0; // If count is greater than 0, cote already exists
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                databaseConnection.Close();
            }
        }

        private void InsertIntoDatabase(ProgrammeItem programme)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("", databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                string query = "INSERT INTO Programme (nom_p, duree, cote, cout) VALUES (@Nom, @Duree, @Cote, @Cout)";

                commandDatabase.CommandText = query;
                commandDatabase.Parameters.AddWithValue("@Nom", programme.Nom);
                commandDatabase.Parameters.AddWithValue("@Duree", programme.Duree);
                commandDatabase.Parameters.AddWithValue("@Cote", programme.Cote);
                commandDatabase.Parameters.AddWithValue("@Cout", programme.Cout);

                commandDatabase.ExecuteNonQuery();

                MessageBox.Show("Program added successfully to the database. Id: " + commandDatabase.LastInsertedId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseConnection.Close();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (laliste.SelectedItem != null)
            {
                ProgrammeItem programmeSelectionne = laliste.SelectedItem as ProgrammeItem;

                if (programmeSelectionne != null)
                {
                    // Display a confirmation message for deletion
                    string confirmationMessage = $"Confirmez la suppression du programme :\nNom : {programmeSelectionne.Nom}\nCote : {programmeSelectionne.Cote}\nDuree : {programmeSelectionne.Duree}\nCout : {programmeSelectionne.Cout}";

                    if (MessageBox.Show(confirmationMessage, "Confirmation de suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // Remove the item from the list
                        programmes.Remove(programmeSelectionne);

                        // Delete the corresponding record from the database
                        DeleteFromDatabase(programmeSelectionne.Cote);

                        // Refresh the list to reflect the changes
                        laliste.Items.Refresh();
                    }
                }
            }
        }

        private void DeleteFromDatabase(string cote)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;Password=;database=desktop";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("", databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                string query = "DELETE FROM Programme WHERE cote = @Cote";

                commandDatabase.CommandText = query;
                commandDatabase.Parameters.AddWithValue("@Cote", cote);

                commandDatabase.ExecuteNonQuery();

                MessageBox.Show("Program deleted successfully from the database.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseConnection.Close();
            }
        }








        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void StagiaireBTN1_Click(object sender, RoutedEventArgs e)
        {
            Stagiaire stagiaire = new Stagiaire();
            this.NavigationService.Navigate(stagiaire);
        }

        private void laliste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            programmeSelectionne = laliste.SelectedItem as ProgrammeItem;
        }

        private void NomTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ResetBTN1_Click(object sender, RoutedEventArgs e)
        {
            NomTextBox.Text = "";
            CoteTextbox.Text = "";
            DureeTextbox.Text = "";
            CoutTextbox.Text = "";
        }

    }
}
