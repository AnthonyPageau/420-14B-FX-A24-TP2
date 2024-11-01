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
        //Est-ce qu'il faut faire ça?
        GestionCourse _gestionCourse;


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

        private void AfficherListeCoureur()
        {
            lstCoureurs.Items.Clear();
            foreach (Course course in _gestionCourse.Courses)
            {
                lstCoureurs.Items.Add(course);
            }
        }

    }
}
