using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
   public class Phone
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime RemovedDate { get; set; }

        public ContactDetails ContactDetails { get; set; }
        public long ContactDetailsId { get; set; }
    }
}
