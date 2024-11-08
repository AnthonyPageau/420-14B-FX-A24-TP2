using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using Microsoft.VisualBasic;
using System;
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
using System.Windows.Shapes;

namespace _420_14B_FX_A24_TP2
{
    public partial class FormCoureur : Window
    {

        /// <summary>
        ///coureur selectionner
        /// </summary>
        private Coureur _coureur;

        /// <summary>
        /// obtient ou définit le coureur
        /// </summary>
        public Coureur Coureur
        {
            get { return _coureur; }
            set { _coureur = value; }
        }

        /// <summary>
        /// Action désirée du formulaire (Ajouter, Modifier, Supprimer)
        /// </summary>
        private EtatFormulaire _etat;

        /// <summary>
        /// obtient ou définit l'état du formulaire
        /// </summary>
        public EtatFormulaire Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        /// <summary>
        /// Permet de constuire le formulalaire formCoureur
        /// </summary>
        /// <param name="etat"></param>
        /// <param name="coureur"></param>
        public FormCoureur(EtatFormulaire etat = EtatFormulaire.Ajouter, Coureur coureur = null)
        {
            Etat = etat;
            Coureur = coureur;
            InitializeComponent();
        }

        /// <summary>
        /// permet de charge les information du formulaire et les affiche a son ouverture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbTitre.Text = $"{Etat} un coureur";
            btnAjouter.Content = Etat;

            if (Etat != EtatFormulaire.Ajouter && Coureur != null)
            {
                txtDossard.Text = Coureur.Dossard.ToString();
                txtNom.Text = Coureur.Nom;
                txtPrenom.Text = Coureur.Prenom;
                txtVille.Text = Coureur.Ville;
                cboProvince.Text = Coureur.Province.ToString();
                cboCategorie.Text = Coureur.Categorie.ToString();
                tspTemps.Text = Coureur.Temps.ToString();
                //faire le check box si il a abbandonner
                if (Etat == EtatFormulaire.Supprimer) 
                {
                    txtDossard.IsEnabled = false;
                    txtNom.IsEnabled = false;
                    txtPrenom.IsEnabled = false;
                    txtVille.IsEnabled = false;
                    cboProvince.IsEnabled = false;
                    cboCategorie.IsEnabled = false;
                    tspTemps.IsEnabled = false;
                    checkAbandon.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Permet d'ajouter, supprimer ou modifier un course dépendamment de l'état du formulare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            switch (Etat)
            {
                case EtatFormulaire.Ajouter:
                    if (ValiderFormulaire()) {

                        Coureur = new Coureur(
                        ushort.Parse(txtDossard.Text),
                        txtNom.Text,
                        txtPrenom.Text,
                        (Categorie)Enum.Parse(typeof(Categorie),
                        cboCategorie.Text),
                        txtVille.Text,
                        (Province)Enum.Parse(typeof(Province),
                        cboProvince.Text),
                        TimeSpan.Zero,
                        false
                        );

                        DialogResult = true;
                    }
                    break;
                case EtatFormulaire.Modifier:
                    if (ValiderFormulaire())
                    {
                        Coureur.Dossard = ushort.Parse(txtDossard.Text);
                        Coureur.Nom = txtNom.Text;
                        Coureur.Prenom = txtPrenom.Text;
                        Coureur.Ville = txtVille.Text;
                        Coureur.Province = (Province)Enum.Parse(typeof(Province), cboProvince.Text);
                        Coureur.Categorie = (Categorie)Enum.Parse(typeof(Categorie), cboCategorie.Text);
                        Coureur.Temps = TimeSpan.Parse(tspTemps.Text);
                        Coureur.Abandon = checkAbandon.IsChecked.Value;
                        DialogResult = true;
                    }
                    break;
                case EtatFormulaire.Supprimer:
                    MessageBoxResult messageResult = MessageBox.Show("Desirez-vous supprimer le coureur?", "Suppression d'un coureur", MessageBoxButton.YesNo, MessageBoxImage.Question,MessageBoxResult.No);
                    if (messageResult == MessageBoxResult.Yes)
                        DialogResult = true;
                    else
                        DialogResult = false;
                    break;
            }
        }

        /// <summary>
        /// Affiche une message de validation si un des champs ne respecte pas leur validation
        /// </summary>
        /// <returns></returns>
        private bool ValiderFormulaire()
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(txtDossard.Text) || ushort.Parse(txtDossard.Text) < Coureur.DOSSARD_VAL_MIN)
            {
                message += $"Le dossard doit être un nombre supérieur ou égal à {Coureur.DOSSARD_VAL_MIN}\n";
            }
            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Trim().Length < Coureur.NOM_NB_CARC_MIN)
            {
                message += $"Vous devez saisir le nom ({Coureur.NOM_NB_CARC_MIN} caractères minimum)\n";
            }
            if (string.IsNullOrWhiteSpace(txtPrenom.Text) || txtPrenom.Text.Trim().Length < Coureur.PRENOM_NB_CARC_MIN)
            {
                message += $"Vous devez saisir le prénom ({Coureur.PRENOM_NB_CARC_MIN} caractères minimum)\n";
            }
            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Trim().Length < Coureur.VILLE_NB_CARC_MIN)
            {
                message += $"Vous devez saisir la ville ({Coureur.VILLE_NB_CARC_MIN} caractères minimum)\n";
            }
            if (string.IsNullOrWhiteSpace(cboProvince.Text))
            {
                message += "Vous devez sélectionner une province\n";
            }
            if (string.IsNullOrWhiteSpace(cboCategorie.Text))
            {
                message += "Vous devez sélectionner une catégorie\n";
            }

            if (message.Length > 0)
            {
                MessageBox.Show(message, "Validation");
                return false;
            }
            return true;
        }

        /// <summary>
        /// permet de fermer le form si le bouton annuler est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
