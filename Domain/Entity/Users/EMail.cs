using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
    public class EMail
    {
        public long Id { get; set; }
        public string UserEMail { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime RemovedDate { get; set; }

        public ContactDetails ContactDetails { get; set; }
        public long ContactDetailsId { get; set; }
    }
}
