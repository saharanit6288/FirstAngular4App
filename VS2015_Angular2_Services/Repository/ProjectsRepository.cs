using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS2015_Angular2_Services.Models;

namespace VS2015_Angular2_Services.Repository
{
    public class ProjectsRepository
    {
        private ProjectDBContext _context;
        const int pageSize = 10;

        public ProjectsRepository()
        {
            _context = new ProjectDBContext();
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public List<Project> GetProjectsByPaging(int page = 1, int pagesize = pageSize)
        {
            return _context.Projects
                        .OrderBy(o => o.ProjectName)
                        .Skip((page - 1) * pagesize)
                        .Take(pagesize)
                        .ToList();
        }

        public List<ProjectDDLViewModel> GetAllForDdl()
        {
            return _context.Projects.Select(s => new ProjectDDLViewModel { ID = s.ID, ProjectName = s.ProjectName }).ToList();
        }

        public List<Project> SearchProjects(string srch)
        {
            IQueryable<Project> result = _context.Projects;

            if (!string.IsNullOrEmpty(srch))
            {
                result = result
                        .Where(w => w.ProjectName.ToLower().Contains(srch.ToLower())
                                || w.ClientName.ToLower().Contains(srch.ToLower()));
            }

            return result.ToList();
        }

        public Project GetProject(int projectID)
        {
            return _context.Projects.Where(w => w.ID == projectID).FirstOrDefault();
        }

        public List<Project> InsertProject(Project p)
        {
            _context.Projects.Add(p);
            _context.SaveChanges();
            return GetProjectsByPaging(1, pageSize);
        }

        public List<Project> UpdateProject(Project proj)
        {
            _context.Entry(proj).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return GetProjectsByPaging(1, pageSize);
        }

        public List<Project> DeleteProject(int id)
        {
            var proj = _context.Projects.Where(w => w.ID == id).FirstOrDefault();
            _context.Projects.Remove(proj);
            _context.SaveChanges();
            return GetProjectsByPaging(1, pageSize);
        }
    }
}