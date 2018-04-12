namespace VS2015_Angular2_Services.Migrations.Migration_Identity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration_Identity : DbMigrationsConfiguration<VS2015_Angular2_Services.ApplicationDbContext>
    {
        public Configuration_Identity()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Migration_Identity";
        }

        protected override void Seed(VS2015_Angular2_Services.ApplicationDbContext context)
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
