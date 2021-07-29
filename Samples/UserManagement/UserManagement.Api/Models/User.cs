using System;

namespace UserManagement.Api.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public Genders Gender { get; private set; }
        public DateTime Birthdate { get; private set; }

        private User(Guid id, string username, Genders gender, DateTime birthdate)
        {
            Id = id;
            Username = username;
            Gender = gender;
            Birthdate = birthdate;
        }

        public static User Create(string username, Genders gender, DateTime birthDate)
        {
            User user = new User(Guid.NewGuid(), username, gender, birthDate);
            return user;
        }
    }

    public enum Genders
    {
        Female = 1,
        Male = 2
    }
}