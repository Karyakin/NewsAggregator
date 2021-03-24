using Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.Users
{
    public class ContactDetails
    {
        public Guid Id { get; set; }
        public string AdditionalInformation { get; set; }

        IEnumerable<Phone> Phones { get; set; }
        IEnumerable<EMail> EMails { get; set; }

        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }
    }
}
