using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestMvcDatabaseFirstApproach.ViewModels;

namespace TestMvcDatabaseFirstApproach.Mappings
{
    public static class PersonMapper
    {
        public static PersonViewModel ToPersonViewModel(this DataAccess.Person personEntity)
        {
            return new PersonViewModel
            {
                PersonId = personEntity.PersonId,
                Username = personEntity.Username,
                Password = personEntity.Password
            };
        }
    }
}