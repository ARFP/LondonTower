using System;

namespace LondonTowerLibrary
{
    [Serializable]
    public class Personn
    {
        private string lastName;
        private string firstName;
        private Genre genre;
        private DateTime dayOfBirth;
        private int age;

        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public Genre Genre { get => genre; set => genre = value; }
        public DateTime DayofBirth { get => dayOfBirth; set => dayOfBirth = value; }
        public int Age { get => age; set => age = value; }

        public Personn()
        {
        }

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
