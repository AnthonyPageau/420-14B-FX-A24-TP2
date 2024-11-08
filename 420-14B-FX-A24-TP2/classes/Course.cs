
using _420_14B_FX_A24_TP2.enums;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace _420_14B_FX_A24_TP2.classes
{
    /// <summary>
    /// Classe représentant une course à pied
    /// </summary>
    public class Course : IComparable<Course>
    {

        public const byte NOM_NB_CAR_MIN = 3;
        public const byte VILLE_NB_CAR_MIN = 4;
        public const ushort DISTANCE_VAL_MIN = 1;


        /// <summary>
        /// Identifiant unique de la course
        /// </summary>
        private Guid _id;


        /// <summary>
        /// Nom de la course
        /// </summary>
        private string _nom;

        /// <summary>
        /// Date de la course
        /// </summary>
        private DateOnly _date;

        /// <summary>
        /// Ville où a lieu la course
        /// </summary>
        private string _ville;

        /// <summary>
        /// Province où a lieu la course
        /// </summary>
        private Province _province;

        /// <summary>
        /// Type de course
        /// </summary>
        private TypeCourse _typeCourse;


        /// <summary>
        /// Distance de la course
        /// </summary>
        private ushort _distance;


        /// <summary>
        /// Liste des coureurs 
        /// </summary>
        private List<Coureur> _coureurs;




        /// <summary>
        /// Obtient ou définit l'identifiant unique d'une course
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set {
                if (value == Guid.Empty)
                    throw new ArgumentException(nameof(Id), "L'identifiant ne peut pas être vide");
                _id = value;
            }
        }


        /// <summary>
        /// Obtient ou modifie le nom de la course.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _nom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le nom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentException">Lancé lors que le nom a moins de NOM_NB_CAR_MIN caractères.</exception>

        public string Nom
        {
            get { return _nom; }

            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Nom), "Le nom ne doit pas être vide");
                if (value.Trim().Length < NOM_NB_CAR_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Nom), $"Le nom doit contenir {NOM_NB_CAR_MIN} caractères");
                _nom = value.Trim().ToUpper(); 
            }
        }


        /// <summary>
        ///Obtien ou modifie la date de la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _date.</value>
        public DateOnly Date
        {
            get { return _date; }
            set { _date = value; }
        }


        /// <summary>
        ///Obtien ou modifie la ville où a lieu la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _ville.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que la ville est nulle ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentException">Lancé lors que la ville a moins de NOM_COURSE_NB_CAR_MIN caractères.</exception>
        public string Ville
        {
            get { return _ville; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Nom), "La ville ne doit pas être vide");
                if (value.Trim().Length < VILLE_NB_CAR_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Nom), $"La ville doit contenir {VILLE_NB_CAR_MIN} caractères");
                _ville = value.Trim(); 
            }
        }




        /// <summary>
        ///Obtien ou modifie la province où a lieu la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _province.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la province n'est pas entre PROVINCE_MIN_VAL et PROVINCE_MAX_VAL.</exception>
        public Province Province
        {
            get { return _province; }
            set 
            {
                if (!Enum.IsDefined(typeof(Province), value))
                    throw new ArgumentOutOfRangeException(nameof(Province), "La province ne fait pas partie de celles disponibles");
                _province = value;
            }
        }


        /// <summary>
        ///Obtien ou modifie le type de course.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _type.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que le type de course n'est pas entre TYPE_COURSE_MIN_VAL et TYPE_COURSE_MAX_VAL.</exception>
        public TypeCourse TypeCourse
        {
            get { return _typeCourse; }
            set 
            {
                if (!Enum.IsDefined(typeof(TypeCourse), value))
                    throw new ArgumentOutOfRangeException(nameof(TypeCourse), "Le type de course ne fait pas partie de ceux disponibles");
                _typeCourse = value;
            }
        }

        /// <summary>
        ///Obtien ou modifie la distance de la course en km
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _distance.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que la distance est inférieure à DISTANCE_VAL_MIN.</exception>
        public ushort Distance
        {
            get { return _distance; }
            set 
            {
                if (value < DISTANCE_VAL_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Distance), $"La distance doit être d'au moins {DISTANCE_VAL_MIN}");
                _distance = value; 
            }
        }

        /// <summary>
        ///Obtien ou modifie la liste des coureurs
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _coureurs.</value>
        public List<Coureur> Coureurs
        {
            get { return _coureurs; }
            set { _coureurs = value; }
        }


     

        /// <summary>
        ///Obtien le nombre de coureurs participants à la course
        /// </summary>
        /// <value>Obtien la valeur de l'attribut :  _coureurs.Count.</value>
        public int NbParticipants
        {
            get {
                return Coureurs.Count;
            }
      
        }

        /// <summary>
        ///Obtien le temps de course moyen
        /// </summary>
        /// <value>Obtien la valeur retourné par la méthode : CalculerTempsCourseMoyen() </value>
        public TimeSpan TempCourseMoyen
        {
            get {
                return CalculerTempsCourseMoyen();
            }
          
        }

        /// <summary>
        /// Permet de constuire un objet de type Course
        /// </summary>
        /// <param name="nom">Nom de la course</param>
        /// <param name="date">Date à laquelle a lieu la course</param>
        /// <param name="ville">Ville où a lieu la course</param>
        /// <param name="province">Province ou a lieu la course</param>
        /// <param name="typeCourse">Type de course</param>
        /// <param name="distance">Distance de la course</param>
        /// <remarks>Initialise une liste de coureurs vide</remarks>
        public Course(Guid id, string nom, DateOnly date, string ville, Province province, TypeCourse typeCourse, ushort distance )
        {
            Id = id;
            Nom = nom;
            Date = date;
            Ville = ville;
            Province = province;
            TypeCourse = typeCourse;
            Distance = distance;
            Coureurs = new List<Coureur>();
        }

        /// <summary>
        /// Permet de trier les coureur du plus petit temps au plus grand, les coureurs qui ont abandonné sont mis à la fin
        /// </summary>
        public void TrierCoureurs()
        {
            Coureurs.Sort();
        }

        /// <summary>
        /// Permet de vérifier si un coureur dans la course existe avec le numéro de dossard
        /// </summary>
        /// <param name="noDossard"></param>
        /// <returns>Retourne un coureur si le numéro de dossard et déjà utilisé, retourne null s'il n'est pas encore utilisé</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Coureur ObtenirCoureurParNoDossard(ushort noDossard)
        {
            if (noDossard < Coureur.DOSSARD_VAL_MIN)
                throw new ArgumentOutOfRangeException("Dossard", $"Le numéro du dossard doit être supérieur à {Coureur.DOSSARD_VAL_MIN}");

            foreach (Coureur c in Coureurs)
            {
                if (c.Dossard == noDossard)
                    return c;
            }

            return null;
        }

        /// <summary>
        /// Permet de calculer la moyenne du temps de course des coureurs
        /// </summary>
        /// <returns>Le temps moyen de course des coureurs</returns>
        private TimeSpan CalculerTempsCourseMoyen()
        {
            int compteur = 0;
            TimeSpan tempsTotale = TimeSpan.Zero;
            foreach(Coureur c in Coureurs)
            {
                if (!c.Abandon && c.Temps != TimeSpan.Zero)
                {
                    compteur++;
                    tempsTotale += c.Temps;
                }
            }

            return compteur == 0 ? TimeSpan.Zero : tempsTotale / compteur;    
        }

        /// <summary>
        /// Surcharge de l'opérateur ToString()
        /// </summary>
        /// <returns>Retounre le nom, la ville, la province et la date de la course</returns>
        public override string ToString()
        {
            string nom = Nom.PadRight(40);
            string ville = Ville.PadRight(25);
            string province = Province.GetDescription().ToString().PadRight(26);
            string date = Date.ToString();

            return $"{nom}{ville}{province}{date}";
        }

        /// <summary>
        /// Surcharge de l'opérateur ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Vérifie le Nom, Date, Ville, Province, TypeCourse et Distance</returns>
        public static bool operator == (Course a, Course b)
        {
            if (Object.ReferenceEquals(a, b)) return true;

            if (a is null || b is null) return false;

            return (a.Nom == b.Nom && a.Date == b.Date && a.Ville.ToUpper() == b.Ville.ToUpper() && a.Province == b.Province && a.TypeCourse == b.TypeCourse && a.Distance == b.Distance);
        }

        /// <summary>
        /// Surcharge de l'opérateur !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Vérifie le Nom, Date, Ville, Province, TypeCourse et Distance</returns>
        public static bool operator != (Course a, Course b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Surcharge de l'opérateur Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Course) return false;

            return this == (Course)obj;
        }

        /// <summary>
        /// Compare deux courses par la date décroissant et le nom de la course
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Retourne 1 si la course est moins récente</returns>
        public int CompareTo(Course? other)
        {
            if (other is null)
                return 1;

            int resComp = Date.CompareTo(other.Date) * -1;

            if (resComp != 0)
                return resComp;

            return string.Compare(Nom, other.Nom, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        }


        /// <summary>
        /// Vérifie que le coureur existe déjà pas avant de l'ajouter
        /// </summary>
        /// <param name="coureur"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void AjouterCoureur(Coureur coureur)
        {
            if (coureur is null)
                throw new ArgumentNullException(nameof(coureur), "Le coureur ne peut être nul");
            foreach (Coureur c in Coureurs)
            {
                if (c.Dossard == coureur.Dossard)
                throw new InvalidOperationException("Il existe déjà un coureur avec ce numero de dossard");
            }
            foreach (Coureur c in Coureurs)
            {
                if (coureur == c)
                    throw new InvalidOperationException("Ce coureur existe déjà avec un autre numéro de dossard");
            }
            Coureurs.Add(coureur);
            TrierCoureurs();
        }

        /// <summary>
        /// Vérifie si le coureur existe avant de le supprimer
        /// </summary>
        /// <param name="coureur"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void SupprimerCoureur(Coureur coureur)
        {
            if (coureur is null)
                throw new ArgumentNullException(nameof(coureur), "Le coureur ne peut pas être nul");
            if (!Coureurs.Contains(coureur))
                throw new InvalidOperationException("Le coureur n'existe pas dans la liste des coureurs");
            Coureurs.Remove(coureur);
            TrierCoureurs();
        }
    }
}
