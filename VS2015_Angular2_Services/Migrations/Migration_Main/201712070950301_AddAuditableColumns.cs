namespace VS2015_Angular2_Services.Migrations.Migration_Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditableColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ManagerComments", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.ManagerComments", "CreatedBy", c => c.String());
            AddColumn("dbo.ManagerComments", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.ManagerComments", "UpdatedBy", c => c.String());
            AddColumn("dbo.ProjectTasks", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.ProjectTasks", "CreatedBy", c => c.String());
            AddColumn("dbo.ProjectTasks", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.ProjectTasks", "UpdatedBy", c => c.String());
            AddColumn("dbo.UserStories", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.UserStories", "CreatedBy", c => c.String());
            AddColumn("dbo.UserStories", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.UserStories", "UpdatedBy", c => c.String());
            AddColumn("dbo.Projects", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Projects", "CreatedBy", c => c.String());
            AddColumn("dbo.Projects", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Projects", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "UpdatedBy");
            DropColumn("dbo.Projects", "UpdatedOn");
            DropColumn("dbo.Projects", "CreatedBy");
            DropColumn("dbo.Projects", "CreatedOn");
            DropColumn("dbo.UserStories", "UpdatedBy");
            DropColumn("dbo.UserStories", "UpdatedOn");
            DropColumn("dbo.UserStories", "CreatedBy");
            DropColumn("dbo.UserStories", "CreatedOn");
            DropColumn("dbo.ProjectTasks", "UpdatedBy");
            DropColumn("dbo.ProjectTasks", "UpdatedOn");
            DropColumn("dbo.ProjectTasks", "CreatedBy");
            DropColumn("dbo.ProjectTasks", "CreatedOn");
            DropColumn("dbo.ManagerComments", "UpdatedBy");
            DropColumn("dbo.ManagerComments", "UpdatedOn");
            DropColumn("dbo.ManagerComments", "CreatedBy");
            DropColumn("dbo.ManagerComments", "CreatedOn");
        }
    }
}
