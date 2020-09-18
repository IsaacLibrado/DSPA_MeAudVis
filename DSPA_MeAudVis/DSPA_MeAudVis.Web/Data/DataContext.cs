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
      
        public DbSet<Borrowing> Borrowings { get; set; }

        public DbSet<BorrowingDetail> BorrowingDetails { get; set; }

        public DbSet<Handbook> Handbooks { get; set; }
        
        public DbSet<Material> Materials { set; get; }
        
        public DbSet<Petitioner> Petitioners { set; get; }
        
        public DbSet<Sizar> Sizars { set; get; }
        

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
