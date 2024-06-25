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
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
            }
            _memoryCache.Set("STS", 1);
            _db.Persons.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult CreateRandomPeople()
        {
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
            }
            _memoryCache.Set("STS", 1);
            Random rand = new Random();
            string[] tempNames = { "asdasd", "asdsadasda" };
            string[] civilStatus = { "s", "m", "w" };
            int[] age = { 23, 21, 24, 25 };
            for (int i = 0; i < 1000; i++)
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
