
using _420_14B_FX_A24_TP2.enums;
using System.Globalization;
using System.Windows.Controls;

namespace _420_14B_FX_A24_TP2.classes
{
    /// <summary>
    /// Classe représentant un coureur
    /// </summary>
    public class Coureur : IComparable<Coureur>
    {
        public const int DOSSARD_VAL_MIN = 1;
        public const int NOM_NB_CARC_MIN = 3;
        public const int PRENOM_NB_CARC_MIN = 3;
        public const int VILLE_NB_CARC_MIN = 4;

        /// <summary>
        /// Numéro du dossard
        /// </summary>
        private ushort _dossard;

        /// <summary>
        /// Nom du coureur
        /// </summary>
        private string _nom;

        /// <summary>
        /// Prénom du coureur
        /// </summary>
        private string _prenom;


        /// <summary>
        /// Catégorie d'âge du coureur
        /// </summary>
        private Categorie _categorie;

        /// <summary>
        /// Ville d'origine du courreur
        /// </summary>
        private string _ville;

        /// <summary>
        /// Province d'origine du coureur.
        /// </summary>
        private Province _province;


        /// <summary>
        /// Temps de course
        /// </summary>
        private TimeSpan _temps;

        /// <summary>
        /// Rang du coureur
        /// </summary>
        private ushort _rang;


        /// <summary>
        /// Indicateur d'abandon de la course
        /// </summary>
        private bool _abandon;




        /// <summary>
        ///Obtien ou modifie le numéro du dossard.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _dossard.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que le numéro du dossard est inférieur à 1.</exception>
        public ushort Dossard
        {
            get { return _dossard; }
            set
            {
                if (value < DOSSARD_VAL_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Nom), $"Le dossard doit avoir une valeur superieur à {DOSSARD_VAL_MIN}"); ;

                _dossard = value;

            }
        }

        /// <summary>
        ///Obtien ou modifie le nom du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _nom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le nom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que le nom a moins de caractères que NOM_COUREUR_NB_CARC_MIN .</exception>
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Nom), "Le nom ne doit pas être vide");
                if (value.Trim().Length < NOM_NB_CARC_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Nom), $"Le nom doit contenir {NOM_NB_CARC_MIN} caractères"); ;
                _nom = value.Trim();
            }
        }



        /// <summary>
        ///Obtien ou modifie le prénom du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _prenom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le prénom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que le prénom a moins de caractères que PRENON_NB_CARC_MIN .</exception>

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Prenom), "Le prénom ne doit pas être vide");
                if (value.Trim().Length < PRENOM_NB_CARC_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Prenom), $"Le prénom doit contenir {PRENOM_NB_CARC_MIN} caractères"); ;


                _prenom = value.Trim();
            }
        }


        /// <summary>
        ///Obtien ou modifie la catégorie du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _categorie.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la catégorie n'existe pas dans les plages de valeurs possibles.</exception>
        public Categorie Categorie
        {
            get { return _categorie; }
            set
            {
                if (!Enum.IsDefined(typeof(Categorie), value))
                    throw new ArgumentOutOfRangeException(nameof(Province), "La catégorie ne fait pas partie de celles disponible");

                _categorie = value;
            }
        }

        /// <summary>
        ///Obtien ou modifie la ville d'origine du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _ville.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que la ville est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que la ville a moins de caractères que VILLE_NB_CARC_MIN .</exception>
        public string Ville
        {
            get { return _ville; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Ville), "Le nom de la ville ne doit pas être vide");
                if (value.Trim().Length < VILLE_NB_CARC_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Ville), $"Le nom de la ville doit contenir {VILLE_NB_CARC_MIN} caractères");

                _ville = value.Trim();
            }
        }

        /// <summary>
        ///Obtien ou modifie la province d'origine du coureur
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _province.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la province n'est pas entre PROVINCE_MIN_VAL et PROVINCE_MAX_VAL.</exception>
        public Province Province
        {
            get { return _province; }
            set
            {
                if(!Enum.IsDefined(typeof(Province), value))
                    throw new ArgumentOutOfRangeException(nameof(Province), "La province ne fait pas partie de celles disponible");
                _province = value;
            }
        }


        /// <summary>
        ///Obtien ou modifie la temps de course du coureur
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _temps.</value>
        public TimeSpan Temps
        {
            get { return _temps; }
            set { _temps = value; }
        }
        /// <summary>
        /// Obtient ou défini le rang du coureur
        /// </summary>
        public ushort Rang
        {
            get { return _rang; }
            set { _rang = value; }
        }


        /// <summary>
        ///Obtien ou modifie l'indicateur d'abandon du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _abandon.</value>
        public bool Abandon
        {
            get { return _abandon; }
            set { _abandon = value; }
        }



        /// <summary>
        /// Permet de construire un objet Coureur
        /// </summary>
        /// <param name="dossard">No. de dossard du coureur</param>
        /// <param name="nom">Nom du coureur</param>
        /// <param name="prenom">Prénom du coureur</param>
        /// <param name="categorie">Catégorie à laquelle appartient le coureur</param>
        /// <param name="ville">Ville du coureur</param>
        /// <param name="province">Province du coureur</param>
        /// <param name="temps">Temps de course du coureur</param>
        /// <param name="abandon">Indicateur d'abandon de la course. Faux par défaut</param>

        public Coureur(ushort dossard, string nom, string prenom, Categorie categorie, string ville, Province province, TimeSpan temps, bool abandon = false)
        {
            Dossard = dossard;
            Nom = nom;
            Prenom = prenom;
            Categorie = categorie;
            Ville = ville;
            Province = province;
            Temps = temps;
            Abandon = abandon;

        }

        /// <summary>
        /// permet l'affichage des informations du coureur dans la liste des coureurs
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string dossard = Dossard.ToString().PadRight(10);
            string nomprenom = (Nom + ',' + Prenom).PadRight(25);
            string categorie = Categorie.ToString().PadRight(19);
            string temps = Temps.ToString().PadRight(12);
            string rang = Rang.ToString();

            return $"{dossard}{nomprenom}{categorie}{temps}{rang}";
        }

        /// <summary>
        /// surcharge d'operateur compareTo
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Retourne 1 si le coureur a abandonner ou est a 0 comme temps</returns>
        public int CompareTo(Coureur? other)
        {
            if (other is null || Abandon || Temps == TimeSpan.Zero)
                return 1;

            if (other.Abandon || other.Temps == TimeSpan.Zero)
                return -1;

            int resComp = Temps.CompareTo(other.Temps);

            if (resComp != 0)
                return resComp;

            return 0;
        }


        /// <summary>
        /// surcharge d'operateur ==
        /// </summary>
        /// <param name="coureur"></param>
        /// <param name="other"></param>
        /// <returns> Retourne si deux coureur sont identique</returns>
        public static bool operator ==(Coureur coureur, Coureur other)
        {
            if (other is null)
                return false;

            return (coureur.Nom == other.Nom && coureur.Prenom == other.Prenom && coureur.Ville == other.Ville && coureur.Province == other.Province && coureur.Temps == other.Temps && coureur.Abandon == other.Abandon);
            
        }

        /// <summary>
        /// permet de comparer deux coureurs sans comparer le dossard
        /// </summary>
        /// <param name="coureur"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool operator !=(Coureur coureur, Coureur other)
        {
            return !(coureur == other);
        }
    }
}
