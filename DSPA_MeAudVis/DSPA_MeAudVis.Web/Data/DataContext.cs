namespace DSPA_MeAudVis.Web.Data
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase Data Context para la migracion en la base de datos
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 09/09/20
    /// Creador Arturo Villegas
    public class DataContext : IdentityDbContext<CUsuario>
    {
        //Las entidades del sistema
        public DbSet<CAdministrador> Administradores { get; set; }
      
        public DbSet<CBecario> Becarios { get; set; }
        
        public DbSet<CManual> Manuales { get; set; }
        
        public DbSet<CMaterial> Materiales { set; get; }
        
        public DbSet<CPrestamo> Prestamos { set; get; }
        
        public DbSet<CSolicitante> Solicitantes { set; get; }
        

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
