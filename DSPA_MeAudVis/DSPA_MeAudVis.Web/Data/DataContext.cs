namespace DSPA_MeAudVis.Web.Data
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {

        public DbSet<CAdministrador> Administradores { get; set; }
        public DbSet<CBecario> Becarios { get; set; }
        public DbSet<CManual> Manuales { get; set; }
        public DbSet<CMaterial> Materiales { set; get; }
        public DbSet<CPrestamo> Prestamos { set; get; }
        public DbSet<CSolicitante> Solicitantes { set; get; }
        public DbSet<CUsuario> Usuarios { set; get; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
