using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VS2015_Angular2_Services.Repository;
using VS2015_Angular2_Services.Models;
using Microsoft.AspNet.Identity;

namespace VS2015_Angular2_Services.Controllers
{
	[Authorize]
    public class ProjectsController : ApiController
    {
        ProjectsRepository repository;

        public ProjectsController()
        {
            repository = new ProjectsRepository();
        }

		[Route("api/Projects/GetProjectsByPaging")]
        public IEnumerable<Project> GetProjectsByPaging(int page, int pagesize)
        {
            return repository.GetProjectsByPaging(page, pagesize);
        }
		
        [Route("api/Projects")]
        public IEnumerable<Project> Get()
        {
            return repository.GetAllProjects();
        }

        [Route("api/Projects/GetAllForDdl")]
        public IEnumerable<ProjectDDLViewModel> GetAllForDdl()
        {
            return repository.GetAllForDdl();
        }

        [Route("api/Projects/{id?}")]
        public Project Get(int id)
        {
            return repository.GetProject(id);
        }

        [Route("api/Projects")]
        public IEnumerable<Project> Get(string q)
        {
            return repository.SearchProjects(q);
        }

        [Route("api/Projects")]
        [HttpPost]
        public IEnumerable<Project> Post(Project e)
        {
			e.CreatedOn = DateTime.Now;
            e.CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.InsertProject(e);
        }

        [Route("api/Projects/{id}")]
        [HttpPut]
        public IEnumerable<Project> Put(Project e)
        {
			e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.UpdateProject(e);

        }

        [Route("api/Projects/{id}")]
        [HttpDelete]
        public IEnumerable<Project> Delete(int id)
        {
            return repository.DeleteProject(id);
        }
    }
}
