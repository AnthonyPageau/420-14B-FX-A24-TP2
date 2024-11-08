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
        /// <summary>
        /// Course sélectionnée
        /// </summary>
        private Course _course;

        /// <summary>
        /// Obtient ou modife la course
        /// </summary>
        public Course Course
        {
            get { return _course; }
            set { _course = value; }
        }

        /// <summary>
        /// Action désirée du formulaire (Ajouter, Modifier, Supprimer)
        /// </summary>
        private EtatFormulaire _etat;

        /// <summary>
        /// Obtient ou modifie l'état du formulaire
        /// </summary>
        public EtatFormulaire Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        /// <summary>
        /// Permet de construre le formulaire FormCourse
        /// </summary>
        /// <param name="etat"></param>
        /// <param name="course"></param>
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
                txtNbrParticipant.Text = Course.NbParticipants.ToString();
                txtTempsCourse.Text = Course.TempCourseMoyen.ToString();

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
                    btnAjouterCoureur.IsEnabled = false;
                    btnModifier.IsEnabled = false;
                    btnSupprimer.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Affiche la liste des coureurs de la course
        /// </summary>
        private void AfficherListeCoureur()
        {
            lstCoureurs.Items.Clear();
            foreach (Coureur coureur in Course.Coureurs)
            {
                lstCoureurs.Items.Add(coureur);
            }
         
        }      

        /// <summary>
        /// Affiche une message de validation si un des champs ne respecte pas leur validation
        /// </summary>
        /// <returns>Retourne true s'il y a aucune erreur, faux s'il y a des erreurs</returns>
        private bool ValiderFormulaire()
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Trim().Length < Course.NOM_NB_CAR_MIN)
                message += $"Le nom de la course doit contenir au minimum {Course.NOM_NB_CAR_MIN} caractères\n";
            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Trim().Length < Course.VILLE_NB_CAR_MIN)
                message += $"Le nom de la ville doit contenir au minimum {Course.VILLE_NB_CAR_MIN} caractères\n";
            if (string.IsNullOrWhiteSpace(cBoxProvince.Text))
                message += "Vous devez sélectionner une province\n";
            if (dtpDate.SelectedDate == null)
                message += "Vous devez sélectionner une date pour la course\n";
            if (string.IsNullOrWhiteSpace(cBoxType.Text))
                message += "Vous devez sélectionner le type de course\n";
            if (!(uint.TryParse(txtDistance.Text, out uint a) && a >= Course.DISTANCE_VAL_MIN))
                message += $"La distance doit être plus grande ou égale que {Course.DISTANCE_VAL_MIN}";

            if (message.Length > 0)
            {
                MessageBox.Show(message, "Validation");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permet d'ajouter, supprimer ou modifier un course dépendamment de l'état du formulare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        

        /// <summary>
        /// Ferme le formulaire FormCourse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Permet d'ajouter un coureur à la course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterCoureur_Click(object sender, RoutedEventArgs e)
        {
            FormCoureur frmCoureur = new FormCoureur();
            if (frmCoureur.ShowDialog() == true)
            {
                Course.AjouterCoureur(frmCoureur.Coureur);
                AfficherListeCoureur();
                MessageBox.Show("Le coureur a bien été ajouté");
            }
        }

        /// <summary>
        /// Permet de modifier un coureur de la course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoureurs.SelectedItem != null)
            {
                Coureur coureur = lstCoureurs.SelectedItem as Coureur;

                FormCoureur frmCoureur = new FormCoureur(EtatFormulaire.Modifier, coureur);

                if (frmCoureur.ShowDialog() == true)
                {
                    AfficherListeCoureur();
                    MessageBox.Show("Le coureur a bien été modifié");
                }

            }
            else
            {
                MessageBox.Show("Vous devez selectionner un coureur");
            }
        }

        /// <summary>
        /// Permet de supprimer un coureur de la course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoureurs.SelectedItem != null)
            {
                Coureur coureur = lstCoureurs.SelectedItem as Coureur;
                FormCoureur frmCoureur = new FormCoureur(EtatFormulaire.Supprimer, coureur);
                if (frmCoureur.ShowDialog() == true)
                {
                    Course.SupprimerCoureur(frmCoureur.Coureur);
                    AfficherListeCoureur();
                    MessageBox.Show("La course a bien été supprimé");
                }
            }
        }
    }
}
