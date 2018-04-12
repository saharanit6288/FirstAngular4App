namespace VS2015_Angular2_Services.Migrations.Migration_Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_add_image_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ImagePath");
        }
    }
}
