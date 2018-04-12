using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VS2015_Angular2_Services.Models;
using VS2015_Angular2_Services.Repository;
using Microsoft.AspNet.Identity;
using System.Web;
using System.IO;
using System.Drawing;

namespace VS2015_Angular2_Services.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        EmployeeRepository repository;
        string baseUrl;
        string fileSavePath;
        string filePath;
        string fileName;

        public EmployeeController()
        {
            repository = new EmployeeRepository();
        }

        [Route("api/employees/GetEmployeesByPaging")]
        public IEnumerable<EmployeeViewModel> GetEmployeesByPaging(int page, int pagesize)
        {
            return repository.GetEmployeesByPaging(page, pagesize);
        }

        [Route("api/employees")]
        public IEnumerable<EmployeeViewModel> Get(string q, int page, int pagesize)
        {
            return repository.GetAllEmployees(q, page, pagesize);
        }

        [Route("api/employees/GetAllForDdl")]
        public IEnumerable<EmployeeDDLViewModel> GetAllForDdl()
        {
            return repository.GetAllForDdl();
        }
		
        [Route("api/employees/{id?}")]
        public Employee Get(int id)
        {
            return repository.GetEmployee(id);
        }

        [Route("api/employees")]
        public IEnumerable<Employee> Get(string q)
        {
            return repository.SearchEmployees(q);
        }

        [Route("api/employees/GetManagers")]
        public IEnumerable<ManagerViewModel> GetManagers()
        {
            return repository.GetManagers();
        }

        [Route("api/employees/")]
        [HttpPost]
        public IEnumerable<EmployeeViewModel> Post(EmployeeInputViewModel e)
        {
            baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

            //Upload New File
            if (!string.IsNullOrEmpty(e.ImgFile))
            {
                fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + e.ImageName;
                fileSavePath = HttpContext.Current.Server.MapPath("~/Uploads/Employee/") + fileName;
                Image img = Base64ToImageConvertion(e.ImgFile);
                img.Save(fileSavePath);
                filePath = baseUrl + "/Uploads/Employee/" + fileName;
            }

            Employee emp = new Employee
            {
                ContactNo = e.ContactNo,
                CreatedOn = DateTime.Now,
                CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                UpdatedOn = DateTime.Now,
                UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                Designation = e.Designation,
                EMailId = e.EMailId,
                EmployeeName = e.EmployeeName,
                ImagePath = filePath,
                ManagerID = e.ManagerID,
                SkillSets = e.SkillSets
            };

            return repository.InsertEmployee(emp);
        }

        public System.Drawing.Image Base64ToImageConvertion(string base64encode)
        {
            byte[] imageBytes = Convert.FromBase64String(base64encode);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        [Route("api/employees/{id}")]
        [HttpPut]
        public IEnumerable<EmployeeViewModel> Put(EmployeeInputViewModel e)
        {
            Employee _existEmp = Get(e.ID);
            baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);


            //If New File Uploaded
            if (!string.IsNullOrEmpty(e.ImgFile))
            {
                if (!string.IsNullOrEmpty(_existEmp.ImagePath))
                {
                    string imagePath = _existEmp.ImagePath.Replace(baseUrl, "");

                    //Delete existing file
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(imagePath)))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(imagePath));
                    }
                }

                fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + e.ImageName;
                fileSavePath = HttpContext.Current.Server.MapPath("~/Uploads/Employee/") + fileName;
                Image img = Base64ToImageConvertion(e.ImgFile);
                img.Save(fileSavePath);
                filePath = baseUrl + "/Uploads/Employee/" + fileName;
            }
            else
            {
                filePath = _existEmp.ImagePath;
            }

            Employee emp = new Employee
            {
                ID = e.ID,
                ContactNo = e.ContactNo,
                CreatedOn = _existEmp.CreatedOn,
                CreatedBy = _existEmp.CreatedBy,
                UpdatedOn = DateTime.Now,
                UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                Designation = e.Designation,
                EMailId = e.EMailId,
                EmployeeName = e.EmployeeName,
                ImagePath = filePath,
                ManagerID = e.ManagerID,
                SkillSets = e.SkillSets
            };

            repository.Detach(_existEmp);

            return repository.UpdateEmployee(emp);

        }

        [Route("api/employees/{id}")]
        [HttpDelete]
        public IEnumerable<EmployeeViewModel> Delete(int id)
        {
            Employee emp = Get(id);
            baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

            if (!string.IsNullOrEmpty(emp.ImagePath))
            {
                string imagePath = emp.ImagePath.Replace(baseUrl, "");

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(imagePath)))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(imagePath));
                }
            }

            return repository.DeleteEmployee(id);
        }
    }
}
