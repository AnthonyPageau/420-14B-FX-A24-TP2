﻿
using _420_14B_FX_A24_TP2.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _420_14B_FX_A24_TP2.classes
{
    public class GestionCourse
    {

		/// <summary>
		/// Liste des courses
		/// </summary>
		private List<Course> _courses;


		/// <summary>
		/// Obtient ou définit la liste des courses
		/// </summary>
		public List<Course> Courses
		{
			get { return _courses; }
			set { _courses = value; }
		}


		/// <summary>
		/// Permet de construire un objet de type Course
		/// </summary>
		/// <param name="cheminFichierCourses"></param>
		/// <param name="cheminFichierCoureurs"></param>
		/// <remarks>Initialise une liste de courses vide</remarks>
		public GestionCourse(string cheminFichierCourses, string cheminFichierCoureurs)
		{
			Courses = new List<Course>();
			ChargerCourse(cheminFichierCourses, cheminFichierCoureurs);
		}


		/// <summary>
		/// Permet de d'ajouter les courses dans la liste de course à partir du fichier csv
		/// </summary>
		/// <param name="cheminFichierCourses"></param>
		/// <param name="cheminFichierCoureurs"></param>
		/// <exception cref="ArgumentNullException"></exception>
        private void ChargerCourse(string cheminFichierCourses, string cheminFichierCoureurs)
		{
            if (string.IsNullOrWhiteSpace(cheminFichierCourses))
                throw new ArgumentNullException("Impossible de lire le fichier des courses");
            if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
                throw new ArgumentNullException("Impossible de lire le fichier des coureurs");

            string[] vectCourses = Utilitaire.ChargerDonnees(cheminFichierCourses);

			for (int i = 1; i < vectCourses.Length; i++)
			{
				string[] vectParametres = vectCourses[i].ToString().Split(';');
				Guid guid = Guid.Parse(vectParametres[0]);
				string nom = vectParametres[1];
				string ville = vectParametres[2];
				Province province = (Province)(Convert.ToInt32(vectParametres[3]));
                DateOnly date = DateOnly.Parse(vectParametres[4]);
				TypeCourse type = (TypeCourse)(Convert.ToInt32(vectParametres[5]));
				ushort distance = ushort.Parse(vectParametres[6]);

				Course course = new Course(guid, nom, date, ville, province, type, distance);
				Courses.Add(course);

				ChargerCoureurs(course, cheminFichierCoureurs);
            }
			Courses.Sort();
        }


		/// <summary>
		/// Permet d'ajouter les coureurs dans chaque courses de la liste de courses  à partir du fichier csv
		/// </summary>
		/// <param name="course"></param>
		/// <param name="cheminFichierCoureurs"></param>
		/// <exception cref="ArgumentNullException"></exception>
        private void ChargerCoureurs(Course course, string cheminFichierCoureurs)
		{
            if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
                throw new ArgumentNullException("Impossible de lire le fichier des coureurs");

            string[] vectCoureurs = Utilitaire.ChargerDonnees(cheminFichierCoureurs);

			for (int i = 1; i < vectCoureurs.Length; i++)
			{
				string[] vectParametres = vectCoureurs[i].ToString().Split(';');
				Guid guid = Guid.Parse(vectParametres[0]);
				if (guid == course.Id)
				{
					ushort dossard = ushort.Parse(vectParametres[1]);
					string nom = vectParametres[2];
					string prenom = vectParametres[3];
					string ville = vectParametres[4];
					Province province = (Province)Enum.Parse(typeof(Province), vectParametres[5]);
					Categorie categorie = (Categorie)Enum.Parse(typeof(Categorie), vectParametres[6]);
                    TimeSpan temps = TimeSpan.Parse(vectParametres[7]);
					bool abandon = Convert.ToBoolean(vectParametres[8]);

					Coureur coureur = new Coureur(dossard, nom, prenom, categorie, ville, province, temps, abandon);

					course.Coureurs.Add(coureur);
				}
			}
			course.TrierCoureurs();
        }

		/// <summary>
		/// Permet d'ajouter une course dans la liste de course
		/// </summary>
		/// <param name="course"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
        public void AjouterCourse(Course course)
		{
			if (course is null)
				throw new ArgumentNullException(nameof(course), "La course ne peut être vide");
            if (Existe(course))
                throw new InvalidOperationException("Impossible d'ajouter la course, car elle existe déjà");

            Courses.Add(course);
			Courses.Sort();
		}


		/// <summary>
		/// Permet de supprimer une couse de la liste de course
		/// </summary>
		/// <param name="course"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public void SupprimerCourse(Course course)
		{
            if (course is null)
                throw new ArgumentNullException(nameof(course), "La course ne peut être vide");

			if (!Existe(course))
				throw new InvalidOperationException("Impossible de supprimer la course, car elle n'exite pas dans la liste");

			Courses.Remove(course);
			Courses.Sort();
		}


		/// <summary>
		/// Permet de vérifier si une course existe dans la liste de course
		/// </summary>
		/// <param name="course"></param>
		/// <returns>Retourne vrai si la course existe et faut si elle n'existe pas</returns>
		public bool Existe(Course course)
		{
			foreach (Course c in Courses)
			{
				if (course == c) return true;
			}

			return false;
		}

		/// <summary>
		/// Permet d'enregistrer les courses et les coureurs dans leur fichier csv respectif
		/// </summary>
		/// <param name="cheminFichierCourses"></param>
		/// <param name="cheminFichierCoureurs"></param>
		/// <exception cref="ArgumentNullException"></exception>
        public void EnregistrerCourses(string cheminFichierCourses, string cheminFichierCoureurs)
		{
			if (string.IsNullOrWhiteSpace(cheminFichierCourses))
				throw new ArgumentNullException("Impossible de lire le fichier des courses");
			if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
				throw new ArgumentNullException("Impossible de lire le fichier des coureurs");

            string courses = "";
			string coureurs = "";

			courses+=("Id;nom;ville;province;date;type;distance\n");
			coureurs+=("IdCourse;dossard;nom;prenom;ville;province;categorie;temps;abandon\n");

			foreach(Course course in Courses)
			{
				courses+=($"{course.Id};{course.Nom};{course.Ville};{(byte) course.Province};{course.Date};{(byte) course.TypeCourse};{course.Distance}\n");
				foreach(Coureur coureur in course.Coureurs)
				{
					coureurs+=($"{course.Id};{coureur.Dossard};{coureur.Nom};{coureur.Prenom};{coureur.Ville};{(byte) coureur.Province};{(byte) coureur.Categorie};{coureur.Temps};{coureur.Abandon}\n");
				}
			}

			Utilitaire.EnregistrerDonnees(cheminFichierCourses, courses);
			Utilitaire.EnregistrerDonnees(cheminFichierCoureurs, coureurs);
        }
    }
}
