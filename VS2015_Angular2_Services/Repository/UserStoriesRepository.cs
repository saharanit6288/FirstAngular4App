using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS2015_Angular2_Services.Models;

namespace VS2015_Angular2_Services.Repository
{
    public class UserStoriesRepository
    {
        private ProjectDBContext _context;
        const int pageSize = 10;

        public UserStoriesRepository()
        {
            _context = new ProjectDBContext();
        }

        public List<UserStoryDDLViewModel> GetAllForDdl()
        {
            return _context.UserStories.Select(s => new UserStoryDDLViewModel { ID = s.ID, Title = s.Title }).ToList();
        }
		
		public List<UserStoryViewModel> GetUserStoriesByPaging(int page = 1, int pagesize = pageSize)
        {
            return _context.UserStories
                    .Select(s => new UserStoryViewModel
                    {
                        ID = s.ID,
                        Title = s.Title,
                        Story = s.Story,
                        ProjectID = s.ProjectID,
                        ProjectName = s.Project.ProjectName
                    })
                    .OrderBy(o => o.ProjectName)
                    .ThenBy(t => t.Title)
					.Skip((page - 1) * pagesize)
					.Take(pagesize)
                    .ToList();
        }

        public List<UserStoryViewModel> GetAllUserStories()
        {
            return _context.UserStories
                    .Select(s => new UserStoryViewModel
                    {
                        ID = s.ID,
                        Title = s.Title,
                        Story = s.Story,
                        ProjectID = s.ProjectID,
                        ProjectName = s.Project.ProjectName
                    })
                    .OrderBy(o => o.ProjectName)
                    .ThenBy(t => t.Title)
                    .ToList();
        }

        public List<UserStoryViewModel> GetAllByProject(int id)
        {
            var result = _context.UserStories
                    .Select(s => new UserStoryViewModel
                    {
                        ID = s.ID,
                        Title = s.Title,
                        Story = s.Story,
                        ProjectID = s.ProjectID,
                        ProjectName = s.Project.ProjectName
                    })
                    .Where(w => w.ProjectID == id)
                    .OrderBy(o => o.ProjectName)
                    .ThenBy(t => t.Title)
                    .ToList();

            return result;
        }

        public List<UserStoryViewModel> SearchUserStories(string srch)
        {
            IQueryable<UserStoryViewModel> result = _context.UserStories
                                                        .Select(s => new UserStoryViewModel
                                                        {
                                                            ID = s.ID,
                                                            Title = s.Title,
                                                            Story = s.Story,
                                                            ProjectID = s.ProjectID,
                                                            ProjectName = s.Project.ProjectName
                                                        });

            if (!string.IsNullOrEmpty(srch))
            {
                result= result
                        .Where(w => w.Title.ToLower().Contains(srch.ToLower())
                                || w.Story.ToLower().Contains(srch.ToLower())
                                || w.ProjectName.ToLower().Contains(srch.ToLower()))
                        
                        .OrderBy(o => o.ProjectName)
                        .ThenBy(t => t.Title);
            }

            return result.ToList();
        }

        public UserStory GetUserStory(int userStoryID)
        {
            var result = _context.UserStories
                .Where(w => w.ID == userStoryID)
                .FirstOrDefault();

            return result;
        }

        public List<UserStoryViewModel> InsertUserStory(UserStory usInput)
        {
            _context.UserStories.Add(usInput);
            _context.SaveChanges();
            return GetUserStoriesByPaging(1, pageSize);
        }

        public List<UserStoryViewModel> UpdateUserStory(UserStory usrStry)
        {
            _context.Entry(usrStry).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return GetUserStoriesByPaging(1, pageSize);
        }

        public List<UserStoryViewModel> DeleteUserStory(int id)
        {
            var usrStry = _context.UserStories.Where(w => w.ID == id).FirstOrDefault();
            _context.UserStories.Remove(usrStry);
            _context.SaveChanges();
            return GetUserStoriesByPaging(1, pageSize);
        }
    }
}