using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
    public class ContactDetails
    {
        public long Id { get; set; }
        public string AdditionalInformation { get; set; }

        IEnumerable<Phone> Phones { get; set; }
        IEnumerable<EMail> EMails { get; set; }

        public Country Country { get; set; }
        public long CountryId { get; set; }
        public City City { get; set; }
        public long CityId { get; set; }
    }
}
