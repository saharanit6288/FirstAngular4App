namespace VS2015_Angular2_Services.Migrations.Migration_Main
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration_Main : DbMigrationsConfiguration<VS2015_Angular2_Services.Models.ProjectDBContext>
    {
        public Configuration_Main()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Migration_Main";
        }

        protected override void Seed(VS2015_Angular2_Services.Models.ProjectDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
