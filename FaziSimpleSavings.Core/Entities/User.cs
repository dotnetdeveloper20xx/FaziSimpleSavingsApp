using System;
using System.Collections.Generic;

namespace FaziSimpleSavings.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }  // Unique identifier for the user
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructor with defensive checks
        public User(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be empty.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.");

            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        // Business method to update user details
        public void UpdateDetails(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be empty.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
