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
    public class ProjectTasksController : ApiController
    {

        ProjectTasksRepository repository;

        public ProjectTasksController()
        {
            repository = new ProjectTasksRepository();
        }
		
		[Route("api/ProjectTasks/GetProjectTasksByPaging")]
        public IEnumerable<ProjectTaskViewModel> GetProjectTasksByPaging(int page, int pagesize)
        {
            return repository.GetProjectTasksByPaging(page, pagesize);
        }

        [Route("api/ProjectTasks")]
        public IEnumerable<ProjectTaskViewModel> Get()
        {
            return repository.GetAllProjectTasks();
        }

        [Route("api/ProjectTasks/{id?}")]
        public ProjectTaskViewModel Get(int id)
        {
            return repository.GetProjectTask(id);
        }

        [Route("api/ProjectTasks")]
        public IEnumerable<ProjectTaskViewModel> Get(string q)
        {
            return repository.SearchProjectTasks(q);
        }

        [Route("api/ProjectTasks")]
        [HttpPost]
        public IEnumerable<ProjectTaskViewModel> Post(ProjectTask e)
        {
			e.CreatedOn = DateTime.Now;
            e.CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.InsertProjectTask(e);
        }

        [Route("api/ProjectTasks/{id}")]
        [HttpPut]
        public IEnumerable<ProjectTaskViewModel> Put(ProjectTask e)
        {
			e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.UpdateProjectTask(e);

        }

        [Route("api/ProjectTasks/{id}")]
        [HttpDelete]
        public IEnumerable<ProjectTaskViewModel> Delete(int id)
        {
            return repository.DeleteProjectTask(id);
        }
    }
}
