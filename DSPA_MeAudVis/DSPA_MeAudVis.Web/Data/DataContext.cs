namespace DSPA_MeAudVis.Web.Data
{
    using DSPA_MeAudVis.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {
        DbSet<CUsuario> Usuarios { get; set; }
        //DbSet<CAdministrador> Administradores { get; set; }
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
    }
}
