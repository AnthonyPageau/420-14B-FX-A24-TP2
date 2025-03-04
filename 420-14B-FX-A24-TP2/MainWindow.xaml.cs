﻿
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
            try
            {
                _gestionCourse = new GestionCourse(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                AfficherListeCourses();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lancement application", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Affiche la liste des courses
        /// </summary>
        private void AfficherListeCourses()
        {
           lstCourses.Items.Clear();
            foreach (Course course in _gestionCourse.Courses)
            {
                lstCourses.Items.Add(course);
            }
        }

        /// <summary>
        /// Permet d'ajouter une course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormCourse frmCourse = new FormCourse();
                if(frmCourse.ShowDialog() == true)
                {
                    _gestionCourse.AjouterCourse(frmCourse.Course);
                    _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                    AfficherListeCourses();
                    MessageBox.Show("La course a été ajoutée avec succès");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ajout d'une course", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Permet de modifier une course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    MessageBox.Show("La course a bien été modifié");
                }
            }
        }

        /// <summary>
        /// Permet de supprimer une course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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