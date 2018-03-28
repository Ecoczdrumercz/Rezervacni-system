namespace WebApplication1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {
            var pl = context.Playgrounds.Add(new Playground {Name = "Dasicka hala",Owner = "Daniel Pichnarcik",Price=100 });
        context.Reservations.AddRange(new List<Reservation>
            {
                new Reservation { Hour = DateTime.Now, Place = pl },
                new Reservation { Hour = DateTime.Now.AddHours(1), Place = pl },
                new Reservation { Hour = DateTime.Now.AddHours(2), Place = pl },
                new Reservation { Hour = DateTime.Now.AddHours(3), Place = pl }
            });
            var a = context.Users.SingleOrDefault(c => c.Email == "pichda@seznam.cz");
            var store = new UserStore<Customer>(context);
            var manager = new UserManager<Customer>(store);
            manager.AddToRole(a.Id, "Admin");

            
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
