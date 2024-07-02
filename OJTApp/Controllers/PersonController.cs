using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OJTApp.Data;
using OJTApp.Models;

namespace OJTApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _memoryCache;
        public PersonController(ApplicationDbContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Person obj)
        {
            //Set STS value 
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
                TempData["STS"] = 1;
            }
            _memoryCache.Set("STS", 1);
            TempData["STS"] = 1;

            //Add Person to Database
            _db.Persons.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult CreateRandomPeople()
        {
            //Set STS value 
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
                TempData["STS"] = 1;
            }
            _memoryCache.Set("STS", 1);
            TempData["STS"] = 1;



            //Random Person Generator
            Random rand = new Random();
            string[] tempNames = { "asdasd", "asdsadasda" };
            string[] civilStatus = { "s", "m", "w" };
            int[] age = { 23, 21, 24, 25 };
            int nOfPersons = 1000; // number of person to generate
            for (int i = 0; i < nOfPersons; i++)
            {
                Person p = new Person();
                p.Name = tempNames[rand.Next(tempNames.Length)];
                p.Age = rand.Next(16, 100);
                p.CivilStatus = civilStatus[rand.Next(civilStatus.Length)];
                _db.Persons.Add(p);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAllRecords()
        {
            //Set STS 
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
                TempData["STS"] = 1;

            }
            _memoryCache.Set("STS", 1);
            TempData["STS"] = 1;



            //Get all persons, Find them and Remove them from the Persons table
            IEnumerable<Person> objPersonsList = _db.Persons;
            foreach (Person p in objPersonsList)
            {
                var obj = _db.Persons.Find(p.Id);
                _db.Persons.Remove(obj);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
                TempData["STS"] = 1;

            }
            TempData["STS"] = _memoryCache.Get("STS");
            return View();
        }
    }
}
