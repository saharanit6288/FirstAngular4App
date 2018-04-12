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
    public class UserStoriesController : ApiController
    {
        UserStoriesRepository repository;

        public UserStoriesController()
        {
            repository = new UserStoriesRepository();
        }
		
		[Route("api/UserStories/GetUserStoriesByPaging")]
        public IEnumerable<UserStoryViewModel> GetUserStoriesByPaging(int page, int pagesize)
        {
            return repository.GetUserStoriesByPaging(page, pagesize);
        }

        [Route("api/UserStories")]
        public IEnumerable<UserStoryViewModel> Get()
        {
            return repository.GetAllUserStories();
        }

        [Route("api/UserStories/GetAllForDdl")]
        public IEnumerable<UserStoryDDLViewModel> GetAllForDdl()
        {
            return repository.GetAllForDdl();
        }

        [Route("api/UserStories/{id?}")]
        public UserStory Get(int id)
        {
            return repository.GetUserStory(id);
        }

        [Route("api/UserStories")]
        public IEnumerable<UserStoryViewModel> Get(string q)
        {
            return repository.SearchUserStories(q);
        }

        [Route("api/UserStories/GetAllByProject/{id}")]
        public IEnumerable<UserStoryViewModel> GetAllByProject(int id)
        {
            return repository.GetAllByProject(id);
        }

        [Route("api/UserStories")]
        [HttpPost]
        public IEnumerable<UserStoryViewModel> Post(UserStory e)
        {
			e.CreatedOn = DateTime.Now;
            e.CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.InsertUserStory(e);
        }

        [Route("api/UserStories/{id}")]
        [HttpPut]
        public IEnumerable<UserStoryViewModel> Put(UserStory e)
        {
			e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.UpdateUserStory(e);

        }

        [Route("api/UserStories/{id}")]
        [HttpDelete]
        public IEnumerable<UserStoryViewModel> Delete(int id)
        {
            return repository.DeleteUserStory(id);
        }
    }
}
