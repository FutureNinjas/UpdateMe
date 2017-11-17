using System.Data.Entity.Migrations;

namespace UpdateMe.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<UpdateMe.Data.UpdateMeDbContext>
    {
        public Configuration()
        {

        }

        protected override void Seed(UpdateMe.Data.UpdateMeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }        
    }
}
