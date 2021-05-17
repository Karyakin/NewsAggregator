using Entities.Entity.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string LoginNew { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string AdditionalInformation { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemovedDate { get; set; }
        public DateTime LastActiv { get; set; } = DateTime.Now;

        public Guid RoleId { get; set; }
        public Role Role { get; set; }


        public SelectList SelectListSourseCountry { get; set; }
        public SelectList SelectListSourseCity { get; set; }
       // public SelectList SelectListSourseRole { get; set; }

        public Guid? CountrySourseId { get; set; }
        public Guid? CitySourseId { get; set; }
       // public Guid? RoleSourseId { get; set; }


      //  public string RoleUser { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Email { get; set; }
        public string Phones { get; set; }
    }
}
