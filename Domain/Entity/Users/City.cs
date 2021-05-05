using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.Users
{
   public class City : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
