using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryCod { get; set; }
        public string CountryPhoneCod { get; set; }
    }
}
