using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS2015_Angular2_Services.Models;

namespace VS2015_Angular2_Services.Repository
{
    public class ProjectTasksRepository
    {
        private ProjectDBContext _context;
        const int pageSize = 10;

        public ProjectTasksRepository()
        {
            _context = new ProjectDBContext();
        }

        public List<ProjectTaskViewModel> GetProjectTasksByPaging(int page = 1, int pagesize = pageSize)
        {
            return _context.ProjectTasks
                    .Select(s => new ProjectTaskViewModel
                    {
                        ID = s.ID,
                        EmployeeID = s.EmployeeID,
                        EmployeeName = s.Employee.EmployeeName,
                        TaskCompletion = s.TaskCompletion,
                        TaskEndDate = s.TaskEndDate,
                        TaskStartDate = s.TaskStartDate,
                        UserStoryID = s.UserStoryID,
                        UserStoryTitle = s.UserStory.Title
                    })
                    .OrderBy(o => o.UserStoryTitle)
                    .ThenBy(t => t.EmployeeName)
					.Skip((page - 1) * pagesize)
					.Take(pagesize)
                    .ToList();
        }
		
		public List<ProjectTaskViewModel> GetAllProjectTasks()
        {
            return _context.ProjectTasks
                    .Select(s => new ProjectTaskViewModel
                    {
                        ID = s.ID,
                        EmployeeID = s.EmployeeID,
                        EmployeeName = s.Employee.EmployeeName,
                        TaskCompletion = s.TaskCompletion,
                        TaskEndDate = s.TaskEndDate,
                        TaskStartDate = s.TaskStartDate,
                        UserStoryID = s.UserStoryID,
                        UserStoryTitle = s.UserStory.Title
                    })
                    .OrderBy(o => o.UserStoryTitle)
                    .ThenBy(t => t.EmployeeName)
                    .ToList();
        }

        public List<ProjectTaskViewModel> SearchProjectTasks(string srch)
        {
            IQueryable<ProjectTaskViewModel> result = _context.ProjectTasks
                                                        .Select(s => new ProjectTaskViewModel
                                                        {
                                                            ID = s.ID,
                                                            EmployeeID = s.EmployeeID,
                                                            EmployeeName = s.Employee.EmployeeName,
                                                            TaskCompletion = s.TaskCompletion,
                                                            TaskEndDate = s.TaskEndDate,
                                                            TaskStartDate = s.TaskStartDate,
                                                            UserStoryID = s.UserStoryID,
                                                            UserStoryTitle = s.UserStory.Title
                                                        });

            if (!string.IsNullOrEmpty(srch))
            {
                result = result
                        .Where(w => w.EmployeeName.ToLower().Contains(srch.ToLower())
                                || w.UserStoryTitle.ToLower().Contains(srch.ToLower()))
                        .OrderBy(o => o.UserStoryTitle)
                        .ThenBy(t => t.EmployeeName);
            }

            return result.ToList();
        }
        
        public ProjectTaskViewModel GetProjectTask(int ProjectTaskID)
        {
            return _context.ProjectTasks
                    .Where(w => w.ID == ProjectTaskID)
                    .Select(s => new ProjectTaskViewModel
                    {
                        ID = s.ID,
                        EmployeeID = s.EmployeeID,
                        EmployeeName = s.Employee.EmployeeName,
                        TaskCompletion = s.TaskCompletion,
                        TaskEndDate = s.TaskEndDate,
                        TaskStartDate = s.TaskStartDate,
                        UserStoryID = s.UserStoryID,
                        UserStoryTitle = s.UserStory.Title
                    })
                    .FirstOrDefault();
        }

        public List<ProjectTaskViewModel> InsertProjectTask(ProjectTask ptInput)
        {
            _context.ProjectTasks.Add(ptInput);
            _context.SaveChanges();
            return GetProjectTasksByPaging(1, pageSize);
        }

        public List<ProjectTaskViewModel> UpdateProjectTask(ProjectTask projTask)
        {
            _context.Entry(projTask).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return GetProjectTasksByPaging(1, pageSize);
        }

        public List<ProjectTaskViewModel> DeleteProjectTask(int id)
        {
            var projTask = _context.ProjectTasks.Where(w => w.ID == id).FirstOrDefault();
            _context.ProjectTasks.Remove(projTask);
            _context.SaveChanges();
            return GetProjectTasksByPaging(1, pageSize);
        }
    }
}