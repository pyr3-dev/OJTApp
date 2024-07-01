using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OJTApp.Data;
using OJTApp.Models;

namespace OJTApp.Controllers
{
    public class SummariesController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _db;
        public SummariesController(ApplicationDbContext db, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _db = db;
        }
        // TODO: GetSummaries
        public IActionResult Summary()
        {
            if (!_memoryCache.TryGetValue("STS", out int stsValue))
            {
                stsValue = 1;
                _memoryCache.Set("STS", stsValue, TimeSpan.FromDays(1));
            }
            if ((int)_memoryCache.Get("STS") == 1)
            {
                CalculateSummaries();
                _memoryCache.Set("STS", 0);
            }
            Dictionary<string, int> statusCount = (Dictionary<string, int>)_memoryCache.Get("statusCount");
            int[] results = { statusCount["s"], statusCount["m"], statusCount["w"] };
            return View(results);
        }
        public void CalculateSummaries()
        {
            CalculateStatusCount();
            //CalculateAgeDistribution();
        }
        /*public void CalculateAgeDistribution()
        {
            if (!_memoryCache.TryGetValue("ageDistribution", out Dictionary<string, int> statusCountValues))
            {
                statusCountValues = new Dictionary<string, int>();
                statusCountValues["1-10"] = 0
                statusCountValues.Add("11-20", 0);
                statusCountValues.Add("21-30", 0);
                statusCountValues.Add("31-40", 0);
                statusCountValues.Add("41-50", 0);
                statusCountValues.Add("51-60", 0);
                statusCountValues.Add("61-70", 0);
                statusCountValues.Add("71-80", 0);
                statusCountValues.Add("81-90", 0);
                statusCountValues["91-100"] = 0;
                _memoryCache.Set("ageDistribution", statusCountValues, TimeSpan.FromDays(1));

            }
            IEnumerable<Person> objCategoryList = _db.Persons;
            Dictionary<string, int> statusCounts = (Dictionary<string, int>)_memoryCache.Get("ageDistribution");
            statusCountValues["s"] = 0;
            statusCountValues["m"] = 0;
            statusCountValues["w"] = 0;
            statusCountValues["1-10"] = 0;
            statusCountValues["11-20"] = 0;
            statusCountValues["21-30"] = 0;
            statusCountValues["31-40"] = 0;
            statusCountValues["41-50"] = 0;
            statusCountValues["51-60"] = 0;
            statusCountValues["61-70"] = 0;
            statusCountValues["71-80"] = 0;
            statusCountValues["81-90"] = 0;
            statusCountValues["91-100"] = 0;
            foreach (Person obj in objCategoryList)
            {
                statusCountValues[obj.CivilStatus] += 1;

            }
            _memoryCache.Set("ageDistribution", statusCountValues, TimeSpan.FromDays(1));
        }*/
        public void CalculateStatusCount()
        {
            if (!_memoryCache.TryGetValue("statusCount", out Dictionary<string, int> statusCountValues))
            {
                statusCountValues = new Dictionary<string, int>();
                statusCountValues.Add("s", 0);
                statusCountValues.Add("m", 0);
                statusCountValues.Add("w", 0);
                _memoryCache.Set("statusCount", statusCountValues, TimeSpan.FromDays(1));

            }
            IEnumerable<Person> objCategoryList = _db.Persons;
            Dictionary<string, int> statusCounts = (Dictionary<string, int>)_memoryCache.Get("statusCount");
            statusCounts["s"] = 0;
            statusCounts["m"] = 0;
            statusCounts["w"] = 0;
            foreach (Person obj in objCategoryList)
            {
                statusCounts[obj.CivilStatus] += 1;

            }
            _memoryCache.Set("statusCount", statusCounts, TimeSpan.FromDays(1));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
