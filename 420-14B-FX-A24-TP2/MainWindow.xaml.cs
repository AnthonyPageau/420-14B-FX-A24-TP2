
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.Primitives;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const string CHEMIN_FICHIER_COUREURS = @"C:\data-420-14B-FX\TP2\coureurs.csv";
        public const string CHEMIN_FICHIER_COURSES = @"C:\data-420-14B-FX\TP2\courses.csv";

        GestionCourse _gestionCourse;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _gestionCourse = new GestionCourse();
            AfficherListeCourses();
        }

        private void AfficherListeCourses()
        {
           lstCourses.Items.Clear();
            foreach (Course course in _gestionCourse.Courses)
            {
                lstCourses.Items.Add(course);
            }
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            FormCourse frmCourse = new FormCourse();
            if(frmCourse.ShowDialog() == true)
            {
                _gestionCourse.AjouterCourse(frmCourse.Course);
                _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                AfficherListeCourses();
                MessageBox.Show("La course a bien été ajouté");
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (lstCourses.SelectedItem != null)
            {
                Course course = lstCourses.SelectedItem as Course;
                FormCourse frmCourse = new FormCourse(EtatFormulaire.Modifier, course);
                if (frmCourse.ShowDialog() == true)
                {
                    _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                    AfficherListeCourses();
                    MessageBox.Show("La course a bien été ajouté");
                }
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lstCourses.SelectedItem != null)
            {
                Course course = lstCourses.SelectedItem as Course;
                FormCourse frmCourse = new FormCourse(EtatFormulaire.Supprimer, course);
                if (frmCourse.ShowDialog() == true)
                {
                    _gestionCourse.SupprimerCourse(course);
                    _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                    AfficherListeCourses();
                    MessageBox.Show("La course a bien été supprimé");
                }
            }
        }
    }
}