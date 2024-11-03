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
    /// <summary>
    /// Logique d'interaction pour Fiche_du_coureur.xaml
    /// </summary>
    public partial class FormCoureur : Window
    {

        private Coureur _coureur;

        public Coureur Coureur
        {
            get { return _coureur; }
            set { _coureur = value; }
        }

        private EtatFormulaire _etat;

        public EtatFormulaire Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }
        public FormCoureur(EtatFormulaire etat = EtatFormulaire.Ajouter, Coureur coureur = null)
        {
            Etat = etat;
            Coureur = coureur;
            InitializeComponent();
        }

        
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


        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            switch (Etat)
            {
                case EtatFormulaire.Ajouter:
                    Coureur = new Coureur(
                        ushort.Parse(txtDossard.Text),
                        txtNom.Text,
                        txtPrenom.Text,
                        (Categorie)Enum.Parse(typeof(Categorie),
                        cboCategorie.Text),
                        txtVille.Text,
                        (Province)Enum.Parse(typeof(Province),
                        cboProvince.Text),
                        TimeSpan.Parse(tspTemps.Text),
                        checkAbandon.IsChecked.Value);

                    DialogResult = true;
                    break;
                case EtatFormulaire.Modifier:
                    Coureur.Dossard = ushort.Parse(txtDossard.Text);
                    Coureur.Nom = txtNom.Text;
                    Coureur.Prenom = txtPrenom.Text;
                    Coureur.Ville = txtVille.Text;
                    Coureur.Province = (Province)Enum.Parse(typeof(Province), cboProvince.Text);
                    Coureur.Categorie = (Categorie)Enum.Parse(typeof(Categorie), cboCategorie.Text);
                    Coureur.Temps = TimeSpan.Parse(tspTemps.Text);
                    Coureur.Abandon = checkAbandon.IsChecked.Value;
                    DialogResult = true;
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

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
