using Microsoft.AspNetCore.Mvc;
using StudentRegistrationForm.Models;
using StudentRegistrationForm.RepositoryLayer;
using System.Diagnostics;
using System.Reflection;

namespace StudentRegistrationForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRL _studentrl;

        public HomeController(ILogger<HomeController> logger, IStudentRL studentRL)
        {
            _logger = logger;
            _studentrl = studentRL;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public IActionResult GetStateandCity()
        {
            try
            {
                var result = _studentrl.GetStateandCity();
                if (result != null && result.Result.code == 200)
                {
                    return Json(result.Result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult InsertStudentData([FromBody] StudentInsertRequet requestData)
        {
            try
            {
                if (XSSControl.ContainsScript(requestData.Name) ||
                    XSSControl.ContainsScript(requestData.Email) ||
                    XSSControl.ContainsScript(requestData.Mobile) ||
                    XSSControl.ContainsScript(requestData.State) ||
                    XSSControl.ContainsScript(requestData.City) ||
                    XSSControl.ContainsScript(requestData.AboutYourself) 
                     )
                {
                    return Json("N|Invalid Inputs");
                }  
                if (string.IsNullOrEmpty(requestData.Name))
                {
                    return Json("N|Name field is empty.");
                }
                if (string.IsNullOrEmpty(requestData.Email))
                {
                    return Json("N|Email field is empty.");
                }
                if (string.IsNullOrEmpty(requestData.Mobile))
                {
                    return Json("N|Mobile field is empty.");
                }
                if (string.IsNullOrEmpty(requestData.State))
                {
                    return Json("N|State field is not selected..");
                }
                if (string.IsNullOrEmpty(requestData.City))
                {
                    return Json("N|City field is not selected.");
                }
                if (string.IsNullOrEmpty(requestData.AboutYourself))
                {
                    return Json("N|AboutYourself field is empty.");
                }

                 
                var result = _studentrl.InsertStudentData(requestData);
                if (result != null && result.Result.code == 200)
                {
                    return Json("Y|" + result.Result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult GetStudentData()
        {
            try
            {
                var result = _studentrl.GetStudentData();
                if (result != null && result.Result.code == 200)
                {
                    return Json(result.Result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult updatestudentdata([FromBody] StudentRequest requestData)
        {
            try
            {
                var result = _studentrl.GetStudentDatabyId(requestData);
                if (result != null && result.Result.code == 200)
                {
                    return Json(result.Result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        public IActionResult StudentDataUpdate([FromBody] UpdateRequet requestData)
        {
            try
            {
                if (XSSControl.ContainsScript(requestData.StudentId) ||
                    XSSControl.ContainsScript(requestData.Name) ||
                    XSSControl.ContainsScript(requestData.Email) ||
                    XSSControl.ContainsScript(requestData.Mobile) ||
                    XSSControl.ContainsScript(requestData.State) ||
                    XSSControl.ContainsScript(requestData.City) ||
                    XSSControl.ContainsScript(requestData.AboutYourself)
                     )
                {
                    return Json("N|Invalid Inputs");
                }
                if (string.IsNullOrEmpty(requestData.Name))
                {
                    return Json("N|Name field is empty.");
                }
                if (string.IsNullOrEmpty(requestData.Email))
                {
                    return Json("N|Email field is empty.");
                }
                if (string.IsNullOrEmpty(requestData.Mobile))
                {
                    return Json("N|Mobile field is empty.");
                }
                if (string.IsNullOrEmpty(requestData.State))
                {
                    return Json("N|State field is not selected..");
                }
                if (string.IsNullOrEmpty(requestData.City))
                {
                    return Json("N|City field is not selected.");
                }
                if (string.IsNullOrEmpty(requestData.AboutYourself))
                {
                    return Json("N|AboutYourself field is empty.");
                }


                var result = _studentrl.UpdateStudentData(requestData);
                if (result != null && result.Result.code == 200)
                {
                    return Json("Y|"+result.Result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Deletestudentdata([FromBody] StudentRequest requestData)
        {
            try
            {
                var result = _studentrl.Deletestudentdata(requestData);
                if (result != null && result.Result.code == 200)
                {
                    return Json(result.Result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ocuurred in Geting State and city  {ex.Message}");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
