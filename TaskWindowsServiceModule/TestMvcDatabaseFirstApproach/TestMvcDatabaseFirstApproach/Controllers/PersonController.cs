using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMvcDatabaseFirstApproach.DataAccess.Repository;
using TestMvcDatabaseFirstApproach.Mappings;
using TestMvcDatabaseFirstApproach.ViewModels;

namespace TestMvcDatabaseFirstApproach.Controllers
{
    public class PersonController : Controller
    {
        private Repository<DataAccess.Person> _personRepository;

        public PersonController()
        {
            _personRepository = new Repository<DataAccess.Person>();
        }

        public PersonViewModel GetPerson(int id)
        {
            var personEntity = _personRepository.GetById(id);
            var personViewModel = personEntity.ToPersonViewModel();
            return personViewModel;
        }

        public ActionResult GetPersonJson(int id)
        {
            var personEntity = _personRepository.GetById(id);
            var personViewModel = personEntity.ToPersonViewModel();
            return Json(personViewModel);
        }
    }
}