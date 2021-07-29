namespace Spectacular.UnitTest.Models
{
    public class Person
    {
        public Person(string name, Genders? gender = null)
        {
            Name = name;
            Gender = gender;
        }

        public string Name { get; }
        public Genders? Gender { get; }
    }

    public enum Genders
    {
        Female = 1,
        Male = 2
    }
}