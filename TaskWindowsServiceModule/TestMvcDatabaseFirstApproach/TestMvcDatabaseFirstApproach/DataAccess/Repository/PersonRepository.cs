using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMvcDatabaseFirstApproach.DataAccess.Repository
{
    public class PersonRepository
    {
        private FakePersonDatabaseEntities _dbContext;

        public PersonRepository()
        {
            _dbContext = new FakePersonDatabaseEntities();
        }

        public Person GetPersonById(int id)
        {
            return _dbContext.People.FirstOrDefault(p => p.PersonId == id);
        }
    }
}