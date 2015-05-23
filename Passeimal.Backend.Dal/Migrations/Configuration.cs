namespace Passeimal.Backend.Dal.Migrations
{
    using Passeimal.Backend.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Passeimal.Backend.Dal.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Passeimal.Backend.Dal.DataContext";
        }

        protected override void Seed(Passeimal.Backend.Dal.DataContext context){
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Steps.AddOrUpdate(
              p => p.Description,
              new Step("Almoço no Eskimó"),
              new Step("Lanche no Rei do mate")
            );
            //
        }
    }// class
}
