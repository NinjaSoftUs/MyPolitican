using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NinjaSoft.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Politician> Politicians { get; set; }
        public DbSet<Summary> Summarys { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Blurb> Blurbs { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

       
        public static ApplicationDbContext Create()
        {
            var context= new ApplicationDbContext();
         
            context.Database.Initialize(false);
            return context;
        }
    }
}