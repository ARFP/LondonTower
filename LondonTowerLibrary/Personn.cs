using System;

namespace LondonTowerLibrary
{
    /// <summary>
    /// Classe Metier contenant les informations de l'utilisateur actuel
    /// </summary>
    [Serializable]
    public class Personn
    {
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        private string lastName;

        /// <summary>
        /// Assesseur de lastName correspondant au Nom de l'utilisateur
        /// </summary>
        public string LastName { get => lastName; set => lastName = value; }


        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        private string firstName;

        /// <summary>
        /// Assesseur de firstName correspondant au Prénom de l'utilisateur
        /// </summary>
        public string FirstName { get => firstName; set => firstName = value; }

        /// <summary>
        /// Sexe de l'utilisateur
        /// </summary>
        private Genre genre;

        /// <summary>
        /// Assesseur de genre correspondant au Sexe de l'utilisateur
        /// </summary>
        public Genre Genre { get => genre; set => genre = value; }

        /// <summary>
        /// Date de naissance de l'utilisateur
        /// </summary>
        private DateTime dayOfBirth;

        /// <summary>
        /// Assesseur de dayOfBirth correspondant a la Date de naissance de l'utilisateur
        /// </summary>
        public DateTime DayofBirth { get => dayOfBirth; set => dayOfBirth = value; }

        /// <summary>
        /// Age de l'utilisateur
        /// </summary>
        private int age;

        /// <summary>
        /// Assesseur de Age correspondant a l'Age de l'utilisateur
        /// </summary>
        public int Age { get => age; set => age = value; }
                
        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Personn()
        {
        }

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="_lastName">Nom de l'utilisateur</param>
        /// <param name="_firstName">Prénom de l'utilisateur</param>
        /// <param name="_genre">Sexe de l'utilisateur</param>
        /// <param name="_dayOfBirth">Date de naissance de l'utilisateur</param>
        /// <param name="_age">Age de l'utilisateur</param>
        public Personn(string _lastName, string _firstName, Genre _genre, DateTime _dayOfBirth, int _age)
        {
            LastName = _lastName;
            FirstName = _firstName;
            Genre = _genre;
            DayofBirth = _dayOfBirth;
            Age = _age;
        }
    }
}
