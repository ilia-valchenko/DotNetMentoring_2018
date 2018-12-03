using System.Web.Mvc;

namespace TestMvcDatabaseFirstApproach.Controllers
{
    public class DefaultController : Controller
    {
        //private readonly DataAccess.Repository.PersonRepository _personRepository;

        public DefaultController()
        {
            //_personRepository = new DataAccess.Repository.PersonRepository();
        }

        // GET: Default
        public string GetFakeString()
        {
            return "This is a random string.";
        }

        //public DataAccess.Person GetPerson(int id)
        //{
        //    //var person = _personRepository.GetPersonById(id);
        //    //return person;
        //}
    }
}