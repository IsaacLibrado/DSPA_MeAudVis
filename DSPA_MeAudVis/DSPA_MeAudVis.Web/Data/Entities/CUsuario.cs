namespace DSPA_MeAudVis.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class CUsuario : IdentityUser
    {
        public string Contrasena { set; get; }
        public int Matricula { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string NombreCompleto { get { return $"{Nombre} {Apellido}"; } }
        public string TipoUsuario { set; get; }
        public ICollection<CManual> Manuales { set; get; }

        public void InicioSesion()
        {

        }

        public void CierreSesion()
        {

        }

        public void ConsultaInformacion()
        {

        }

        public void CambiarContrasena()
        {

        }

        public void RevisionManuales()
        {

        }
    }
}
