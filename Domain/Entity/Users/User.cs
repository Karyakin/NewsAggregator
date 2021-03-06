﻿using Contracts.Interfaces;
using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.Users
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string AdditionalInformation { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemovedDate { get; set; }
        public DateTime LastActiv { get; set; } = DateTime.Now;

        public IEnumerable<Photo> Photos { get; set; }
        //public IEnumerable<Role> Roles { get; set; }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }


        public ContactDetails ContactDetails { get; set; }
        public Guid ContactDetailsId { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
