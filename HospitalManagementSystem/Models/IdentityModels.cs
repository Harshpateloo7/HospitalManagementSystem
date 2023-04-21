using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalManagementSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FavoriteColor { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //Add  an Patient entity into our system
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }


        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Blog> Blog { get; set; }

        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Specialist> Specialists { get; set; }



        public System.Data.Entity.DbSet<HospitalManagementSystem.Models.Medicine> Medicines { get; set; }


        // Add an Branch entity into our system

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Applicant> Applicants { get; set; }


        public DbSet<Career> Careers { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}