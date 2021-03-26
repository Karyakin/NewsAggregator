using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.Users
{
   public class Phone
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemovedDate { get; set; }

        public ContactDetails ContactDetails { get; set; }
        public Guid ContactDetailsId { get; set; }
    }
}
