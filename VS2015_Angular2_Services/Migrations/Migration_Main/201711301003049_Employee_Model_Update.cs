namespace VS2015_Angular2_Services.Migrations.Migration_Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_Model_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Employees", "CreatedBy", c => c.String());
            AddColumn("dbo.Employees", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Employees", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "UpdatedBy");
            DropColumn("dbo.Employees", "UpdatedOn");
            DropColumn("dbo.Employees", "CreatedBy");
            DropColumn("dbo.Employees", "CreatedOn");
        }
    }
}
