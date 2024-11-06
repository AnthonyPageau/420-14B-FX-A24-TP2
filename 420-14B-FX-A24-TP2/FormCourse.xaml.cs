using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
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
    /// Interaction logic for FormCourse.xaml
    /// </summary>
    public partial class FormCourse : Window
    {
        private Course _course;

        public Course Course
        {
            get { return _course; }
            set { _course = value; }
        }

        private EtatFormulaire _etat;

        public EtatFormulaire Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }


        public FormCourse(EtatFormulaire etat = EtatFormulaire.Ajouter, Course course = null)
        {
            Etat = etat;
            Course = course;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTitre.Text = $"{Etat} d'une course";
            btnAjouter.Content = Etat;

            if (Course != null)
            {
                txtNom.Text = Course.Nom;
                txtVille.Text = Course.Ville;
                dtpDate.Text = Course.Date.ToString();
                cBoxProvince.Text = Course.Province.ToString();
                cBoxType.Text = Course.TypeCourse.ToString();
                txtDistance.Text = Course.Distance.ToString();
                txtNbrParticipant.Text = Course.Coureurs.Count.ToString();

                foreach (Coureur coureur in Course.Coureurs)
                {
                    lstCoureurs.Items.Add(coureur);
                }

                if (Etat == EtatFormulaire.Supprimer)
                {
                    txtNom.IsEnabled = false;
                    txtVille.IsEnabled = false;
                    dtpDate.IsEnabled = false;
                    txtDistance.IsEnabled = false;
                    txtNbrParticipant.IsEnabled = false;
                    cBoxProvince.IsEnabled = false;
                    cBoxType.IsEnabled = false;
                }
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            switch(Etat)
            {
                case EtatFormulaire.Ajouter:
                    if (ValiderFormulaire())
                    {
                        Course = new Course(
                            Guid.NewGuid(),
                            txtNom.Text,
                            DateOnly.FromDateTime(dtpDate.SelectedDate.Value),
                            txtVille.Text,
                            (Province)Enum.Parse(typeof(Province), cBoxProvince.Text),
                            (TypeCourse)Enum.Parse(typeof(TypeCourse), cBoxType.Text),
                            ushort.Parse(txtDistance.Text)
                         );
                        DialogResult = true;
                    }
                    break;
                case EtatFormulaire.Modifier:
                    if (ValiderFormulaire())
                    {
                        Course.Nom = txtNom.Text;
                        Course.Date = DateOnly.FromDateTime(dtpDate.SelectedDate.Value);
                        Course.Ville = txtVille.Text;
                        Course.Province = (Province)Enum.Parse(typeof(Province), cBoxProvince.Text);
                        Course.TypeCourse = (TypeCourse)Enum.Parse(typeof(TypeCourse), cBoxType.Text);
                        Course.Distance = ushort.Parse(txtDistance.Text);

                        DialogResult = true;
                    }
                    break;
                case EtatFormulaire.Supprimer:
                    MessageBoxResult messageBoxResult = MessageBox.Show("Désirez-vous supprimer la course",
                        "Suppression d'une course", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
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

        private void btnAjouterCoureur_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoureurs.SelectedItem != null)
            {
                Coureur coureur = lstCoureurs.SelectedItem as Coureur;
                Fiche_du_coureur frmCoureur = new Fiche_du_coureur(EtatFormulaire.Modifier, coureur);
                if (frmCoureur.ShowDialog() == true)
                {
                    
                }
            }
        }

        private bool ValiderFormulaire()
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Trim().Length < 3)
                message += $"Le nom de la course doit contenir au moins {Course.NOM_NB_CAR_MIN} caractères\n";
            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Trim().Length < 4)
                message += $"La ville de la course doit contenir au moins {Course.VILLE_NB_CAR_MIN} caractères\n";
            if (string.IsNullOrWhiteSpace(cBoxProvince.Text))
                message += "Vous devez choisir une province\n";
            if (dtpDate.SelectedDate == null)
                message += "Vous devez choisir une date pour la course\n";
            if (string.IsNullOrWhiteSpace(cBoxType.Text))
                message += "Vous devez choisir un type de course\n";
            if (!(uint.TryParse(txtDistance.Text, out uint a) && a > 0))
                message += $"La distance doit être plus grande que {Course.DISTANCE_VAL_MIN}";

            if (message.Length > 0)
            {
                MessageBox.Show(message, "Validation");
                return false;
            }

            return true;
        }
    }
}
