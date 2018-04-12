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
    public class ManagerCommentsController : ApiController
    {
        ManagerCommentsRepository repository;

        public ManagerCommentsController()
        {
            repository = new ManagerCommentsRepository();
        }

        [Route("api/ManagerComments/GetManagerCommentsByPaging")]
        public IEnumerable<CommentViewModel> GetManagerCommentsByPaging(int page, int pagesize)
        {
            return repository.GetManagerCommentsByPaging(page, pagesize);
        }

        [Route("api/ManagerComments")]
        public IEnumerable<CommentViewModel> Get()
        {
            return repository.GetAllManagerComments();
        }

        [Route("api/ManagerComments/{id?}")]
        public ManagerComment Get(int id)
        {
            return repository.GetManagerComment(id);
        }

        [Route("api/ManagerComments")]
        public IEnumerable<CommentViewModel> Get(string q)
        {
            return repository.SearchComments(q);
        }

        [Route("api/ManagerComments/GetCommentsByTask/{id}")]
        public IEnumerable<ManagerComment> GetCommentsByTask(int id)
        {
            return repository.GetCommentsByTask(id);
        }

        [Route("api/ManagerComments/GetTaskComments/{id}")]
        public IEnumerable<CommentViewModel> GetTaskComments(int id)
        {
            return repository.GetTaskComments(id);
        }

        [Route("api/ManagerComments")]
        [HttpPost]
        public IEnumerable<CommentViewModel> Post(ManagerComment e)
        {
            e.CommentDate = System.DateTime.Now;
			e.CreatedOn = DateTime.Now;
            e.CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.InsertManagerComment(e);
        }

        [Route("api/ManagerComments/{id}")]
        [HttpPut]
        public IEnumerable<CommentViewModel> Put(ManagerComment e)
        {
			e.UpdatedOn = DateTime.Now;
            e.UpdatedBy = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return repository.UpdateManagerComment(e);

        }

        [Route("api/ManagerComments/{id}")]
        [HttpDelete]
        public IEnumerable<CommentViewModel> Delete(int id)
        {
            return repository.DeleteManagerComment(id);
        }
    }
}
