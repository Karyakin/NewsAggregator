using Contracts.Interfaces;
using Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.Users
{
    public class ContactDetails : IBaseEntity
    {
        public Guid Id { get; set; }
        public string AdditionalInformation { get; set; }

        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<EMail> EMails { get; set; }

        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }
    }
}
