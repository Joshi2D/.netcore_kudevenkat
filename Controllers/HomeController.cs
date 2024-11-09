using Kudvenkat_.NET_Core.Models;
using Kudvenkat_.NET_Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;

namespace Kudvenkat_.NET_Core.Controllers
{
     [Route("Home")] //---> so instead of applying home attribute on all controllers we can simply put it here for less repeatition
    //[Route("controller")] // ---> instead of  hardcoding controller name each time for each controlller we can directly put controller and it will take controller name
    //[Route("[controller]/[action]")] // ---> now we can remove action at controller level 
    public class HomeController : Controller
    {
        private readonly IEmployeeRespository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IEmployeeRespository employeeRespository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRespository;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("")]
        [Route("Index")]
        public string Index()
        {
            return _employeeRepository.GetEmployee(2).Name;
        }

        //public Employee Details()
        //{
        //    return _employeeRepository.GetEmployee(1);
        //}

        //public ObjectResult Details()
        //{
        //    Employee employee =  _employeeRepository.GetEmployee(1);

        //    return new ObjectResult(employee);
        //}

        [Route("Details/{Id}")]
        public ViewResult Details(int Id)
        {
            Employee emp = _employeeRepository.GetEmployee(Id);

            if(emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", Id);
            }

            EmployeeLocationViewModel employeeLocationViewModel = new EmployeeLocationViewModel()
            {
                Employee = emp,
                PageTitle = "Employee Details"
            };
            return View(employeeLocationViewModel);
        }

        [Route("action")] //----> action name will be taken
        public ViewResult randomView()
        {
            
            ViewData["PageTitle"] = " Hello from NY";
            ViewBag.Name = "Ravish Kumar";
            ViewBag.Email = "ravish_kumar@ndtv.com";
            return View("../../Views/Test/Test");
        }

        //[Route("Details/{Id?}")]
        //public ViewResult GetEmployee(int? Id)
        //{
        //    EmployeeLocationViewModel employeeLocationViewModel = new EmployeeLocationViewModel()
        //    {
        //        Employee = _employeeRepository.GetEmployee(Id??1),
        //        PageTitle = "Employee Details"
        //    };
        //    return View("Index", employeeLocationViewModel);

        //}

        [Route("List")] //---> this method will be called using above route path
        public ViewResult GetEmployees()
        {
            return View("EmployeeList", _employeeRepository.GetEmployees()); //--->now we have path name changed, we need to provide view file path.

        }

        [HttpGet]
        [Route("Create")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(EmployeeCreateViewModel employee_vm)
        {
            
            if (ModelState.IsValid) {//---> checks if the model passed is correct and if not conditions fails


                Employee employee = new Employee
                {
                    Name = employee_vm.Name,
                    Email = employee_vm.Email,
                    Department = employee_vm.Department,
                    Photopath = ProcessUploadedFile(employee_vm)
                }; 


                _employeeRepository.Add(employee);
                return RedirectToAction("details", new { Id = employee.Id });
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        [Route("Edit")]
        public ViewResult edit(int Id)
        {

            Employee old_employee = _employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel();

            employeeEditViewModel.Id = old_employee.Id;
            employeeEditViewModel.Name = old_employee.Name;
            employeeEditViewModel.Department = old_employee.Department;
            employeeEditViewModel.Email = old_employee.Email;
            employeeEditViewModel.ExistingPhotoPath = old_employee.Photopath;

            return View(employeeEditViewModel);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                // Update the employee object with the data in the model object
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    employee.Photopath = ProcessUploadedFile(model);
                }

                // Call update method on the repository service passing it the
                // employee object to update the data in the database table
                Employee updatedEmployee = _employeeRepository.Update(employee);

                return RedirectToAction("GetEmployees");
            }

            return View(model);
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))//using block is to dispose of values in filestream
                                                                                  //so that previous session does not interfere current session
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }

}
