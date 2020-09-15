namespace DSPA_MeAudVis.Web.Data
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase Data Context para la migracion en la base de datos
    /// </summary>
    /// Version 1.1
    /// Fecha de creacion 09/09/20
    /// Fecha de Modificacion 14/09/20
    /// Creador Arturo Villegas
    public class DataContext : IdentityDbContext<User>
    {
        //Las entidades del sistema
        public DbSet<Administrator> Administrators { get; set; }
      
        public DbSet<Scholar> Scholars { get; set; }
        
        public DbSet<Handbook> Handbooks { get; set; }
        
        public DbSet<Material> Materials { set; get; }
        
        public DbSet<Loan> Loans { set; get; }
        
        public DbSet<Applicant> Applicants { set; get; }
        

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
