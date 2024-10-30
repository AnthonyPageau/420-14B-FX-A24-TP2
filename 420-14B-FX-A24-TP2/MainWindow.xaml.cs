
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using System.Windows;
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
            frmCourse.ShowDialog();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (lstCourses.SelectedItem != null)
            {
                Course course = lstCourses.SelectedItem as Course;
                FormCourse frmCourse = new FormCourse(EtatFormulaire.Modifier, course);
                frmCourse.ShowDialog();
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lstCourses.SelectedItem != null)
            {
                Course course = lstCourses.SelectedItem as Course;
                FormCourse frmCourse = new FormCourse(EtatFormulaire.Supprimer, course);
                frmCourse.ShowDialog();
            }
        }
    }
}