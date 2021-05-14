using Entities.Entity.NewsEnt;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsMember { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemovedDate { get; set; }

    }
}
