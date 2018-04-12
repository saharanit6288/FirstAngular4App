using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS2015_Angular2_Services.Models;

namespace VS2015_Angular2_Services.Repository
{
    public class ManagerCommentsRepository
    {
        private ProjectDBContext _context;
        const int pageSize = 10;

        public ManagerCommentsRepository()
        {
            _context = new ProjectDBContext();
        }

        public List<CommentViewModel> GetTaskComments(int id)
        {
            return _context.ManagerComments
                    .Select(s => new CommentViewModel
                    {
                        ID = s.ID,
                        Comments = s.Comments,
                        CommentDate = s.CommentDate,
                        ProjectTaskID = s.ProjectTaskID,
                        EmployeeName = s.ProjectTask.Employee.EmployeeName,
                        ProjectName = s.ProjectTask.UserStory.Project.ProjectName,
                        UserStoryTitle = s.ProjectTask.UserStory.Title
                    })
                    .Where(w => w.ProjectTaskID == id)
                    .OrderBy(o => o.ProjectName)
                    .ThenBy(t => t.UserStoryTitle)
                    .ThenBy(h => h.EmployeeName)
                    .ToList();
        }
		
		public List<CommentViewModel> GetManagerCommentsByPaging(int page = 1, int pagesize = pageSize)
        {
            return _context.ManagerComments
                    .Select(s => new CommentViewModel
                    {
                        ID = s.ID,
                        Comments = s.Comments,
                        CommentDate = s.CommentDate,
                        ProjectTaskID = s.ProjectTaskID,
                        EmployeeName = s.ProjectTask.Employee.EmployeeName,
                        ProjectName = s.ProjectTask.UserStory.Project.ProjectName,
                        UserStoryTitle = s.ProjectTask.UserStory.Title
                    })
                    .OrderBy(o => o.ProjectName)
                    .ThenBy(t => t.UserStoryTitle)
                    .ThenBy(h => h.EmployeeName)
					.Skip((page - 1) * pagesize)
					.Take(pagesize)
                    .ToList();
        }

        public List<CommentViewModel> GetAllManagerComments()
        {
            return _context.ManagerComments
                    .Select(s => new CommentViewModel
                    {
                        ID = s.ID,
                        Comments = s.Comments,
                        CommentDate = s.CommentDate,
                        ProjectTaskID = s.ProjectTaskID,
                        EmployeeName = s.ProjectTask.Employee.EmployeeName,
                        ProjectName = s.ProjectTask.UserStory.Project.ProjectName,
                        UserStoryTitle = s.ProjectTask.UserStory.Title
                    })
                    .OrderBy(o => o.ProjectName)
                    .ThenBy(t => t.UserStoryTitle)
                    .ThenBy(h => h.EmployeeName)
                    .ToList();
        }

        public List<CommentViewModel> SearchComments(string srch)
        {
            IQueryable<CommentViewModel> result = _context.ManagerComments
                                                    .Select(s => new CommentViewModel
                                                    {
                                                        ID = s.ID,
                                                        Comments = s.Comments,
                                                        CommentDate = s.CommentDate,
                                                        ProjectTaskID = s.ProjectTaskID,
                                                        EmployeeName = s.ProjectTask.Employee.EmployeeName,
                                                        ProjectName = s.ProjectTask.UserStory.Project.ProjectName,
                                                        UserStoryTitle = s.ProjectTask.UserStory.Title
                                                    })
                                                    .OrderBy(o => o.ProjectName)
                                                    .ThenBy(t => t.UserStoryTitle)
                                                    .ThenBy(h => h.EmployeeName);

            if (!string.IsNullOrEmpty(srch))
            {
                result = result
                        .Where(w => w.EmployeeName.ToLower().Contains(srch.ToLower())
                                || w.Comments.ToLower().Contains(srch.ToLower())
                                || w.ProjectName.ToLower().Contains(srch.ToLower())
                                || w.UserStoryTitle.ToLower().Contains(srch.ToLower()));
            }

            return result.ToList();
        }

        public List<ManagerComment> GetCommentsByTask(int TaskID)
        {
            return _context.ManagerComments.Where(w => w.ProjectTaskID == TaskID).ToList();
        }

        public ManagerComment GetManagerComment(int ManagerCommentID)
        {
            return _context.ManagerComments.Where(w => w.ID == ManagerCommentID).FirstOrDefault();
        }

        public List<CommentViewModel> InsertManagerComment(ManagerComment mc)
        {
            _context.ManagerComments.Add(mc);
            _context.SaveChanges();
            return GetTaskComments(mc.ProjectTaskID);
        }

        public List<CommentViewModel> UpdateManagerComment(ManagerComment mc)
        {
            _context.Entry(mc).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return GetTaskComments(mc.ProjectTaskID);
        }

        public List<CommentViewModel> DeleteManagerComment(int id)
        {
            var mc = _context.ManagerComments.Where(w => w.ID == id).FirstOrDefault();
            _context.ManagerComments.Remove(mc);
            _context.SaveChanges();
            return GetTaskComments(mc.ProjectTaskID);
        }
    }
}