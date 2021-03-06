﻿using Contracts.Interfaces;
using Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Users
{
    public class EMail : IBaseEntity
    {
        public Guid Id { get; set; } 
        public string UserEMail { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemovedDate { get; set; }

        public ContactDetails ContactDetails { get; set; }
        public Guid ContactDetailsId { get; set; }
    }
}
