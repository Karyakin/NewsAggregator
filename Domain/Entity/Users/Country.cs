using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Users
{
    public class Country : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryCod { get; set; }
        public string CountryPhoneCod { get; set; }
    }
}
