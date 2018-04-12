using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext() : base("VS2015Angular2DbContext") { }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<UserStory> UserStories { get; set; }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public DbSet<ManagerComment> ManagerComments { get; set; }
    }
}