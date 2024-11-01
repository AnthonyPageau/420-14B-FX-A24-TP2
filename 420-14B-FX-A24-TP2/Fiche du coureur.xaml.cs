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
    public partial class Fiche_du_coureur : Window
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
        public Fiche_du_coureur(EtatFormulaire etat = EtatFormulaire.Ajouter, Coureur coureur = null)
        {
            Coureur = coureur;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbTitre.Text = $"{Etat} un coureur";
            btnBouton.Content = Etat;

            if (_coureur != null)
            {
                txtDossard.Text = Coureur.Dossard.ToString();
                txtNom.Text = Coureur.Nom;
                txtPrenom.Text = Coureur.Prenom;
                txtVille.Text = Coureur.Ville;
                cboProvince.Text = Coureur.Province.ToString();
                cboCategorie.Text = Coureur.Categorie.ToString();
                tspTemps.Text = Coureur.Temps.ToString();
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
