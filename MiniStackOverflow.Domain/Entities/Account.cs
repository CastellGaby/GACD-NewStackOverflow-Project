using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace MiniStackOverflow.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }

        public Account()
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;
            isAuthenticated = false;
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isAuthenticated { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Views { get; set; }
        
    }
}