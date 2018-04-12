namespace VS2015_Angular2_Services.Migrations.Migration_Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManagerComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        ProjectTaskID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectTasks", t => t.ProjectTaskID, cascadeDelete: true)
                .Index(t => t.ProjectTaskID);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        TaskStartDate = c.DateTime(nullable: false),
                        TaskEndDate = c.DateTime(nullable: false),
                        TaskCompletion = c.Int(nullable: false),
                        UserStoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.UserStories", t => t.UserStoryID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.UserStoryID);
            
            CreateTable(
                "dbo.UserStories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Story = c.String(),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ClientName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManagerComments", "ProjectTaskID", "dbo.ProjectTasks");
            DropForeignKey("dbo.ProjectTasks", "UserStoryID", "dbo.UserStories");
            DropForeignKey("dbo.UserStories", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectTasks", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.UserStories", new[] { "ProjectID" });
            DropIndex("dbo.ProjectTasks", new[] { "UserStoryID" });
            DropIndex("dbo.ProjectTasks", new[] { "EmployeeID" });
            DropIndex("dbo.ManagerComments", new[] { "ProjectTaskID" });
            DropTable("dbo.Projects");
            DropTable("dbo.UserStories");
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.ManagerComments");
        }
    }
}
