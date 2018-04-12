namespace VS2015_Angular2_Services.Migrations.Migration_Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Designation = c.String(),
                        ManagerID = c.Int(),
                        ContactNo = c.String(),
                        EMailId = c.String(),
                        SkillSets = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
