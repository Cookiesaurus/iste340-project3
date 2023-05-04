using iSchoolWebApp.Models;
using iSchoolWebApp.utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Dynamic;

namespace iSchoolWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            // first, go get data
            DataRetrieval dataR = new DataRetrieval();
            
            // load data
            var loadedAbout = await dataR.GetData("about/");
            // loadedAbout is a string, we need it to be JSON
            // another issue: VS does not have a good JSON converter
            // so what do D:
            // you use Newtonsoft.JSON! Industry standard JSON converter
            // install Newtonsoft by hitting "install nuGet packages" after
            // right clicking project title in solution explorer!

            var jsonResult = JsonConvert.DeserializeObject<AboutRootModel>(loadedAbout);
            // jsonResult is an object of type AboutRootModel which is now populated with data

            // add the pageTitle

            jsonResult.pageTitle = "About the iSchool";
            // then, load data into model

            return View(jsonResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Degrees()
        {
            // first, go get data
            DataRetrieval dataR = new DataRetrieval();

            // load data
            var loadedDegrees = await dataR.GetData("degrees/");

            var jsonResult = JsonConvert.DeserializeObject<DegreesRootModel>(loadedDegrees);
            // jsonResult is an object of type DegreeRootModel which is now populated with data

            // then, load data into model

            return View(jsonResult);
        }

        public async Task<IActionResult> People()
        {
            DataRetrieval dataR = new DataRetrieval();

            var loadedPeople = await dataR.GetData("people/");

            var jsonResult = JsonConvert.DeserializeObject<PeopleRootModel>(loadedPeople);

            return View(jsonResult);
        }


        public async Task<IActionResult> Minors()
        {
            DataRetrieval dataR = new DataRetrieval();

            var loadedMinors= await dataR.GetData("minors/");

            var jsonResult = JsonConvert.DeserializeObject<MinorsRootModel>(loadedMinors);

            return View(jsonResult);
        }

        public async Task<IActionResult> Course()
        {
            DataRetrieval dataR = new DataRetrieval();

            var loadedCourse = await dataR.GetData("course/");

            var jsonResult = JsonConvert.DeserializeObject<CourseModel[]>(loadedCourse);

            return View(new CourseRootModel
            {
                Course = jsonResult
            });
        }

        public async Task<IActionResult> Employment()
        {
            DataRetrieval dataR = new DataRetrieval();

            var loadedEmployment = await dataR.GetData("employment/");

            var jsonResult = JsonConvert.DeserializeObject<EmploymentRootModel>(loadedEmployment);

            return View(jsonResult);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}