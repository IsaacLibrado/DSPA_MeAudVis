namespace DSPA_MeAudVis.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    /// <summary>
    /// Clase User para el inicio de sesión de los usuarios
    /// </summary>
    /// Version 1.0
    /// Fecha de creacion 7/09/20
    /// Creador Isaac Librado
    public class CUsuario : IdentityUser
    {
        //Propiedades
        public string Contrasena { set; get; }

        public int Matricula { set; get; }
        
        public string Nombre { set; get; }
        
        public string Apellido { set; get; }
        
        public string NombreCompleto { get { return $"{Nombre} {Apellido}"; } }
        
        public string TipoDeUsuario { set; get; }
        
        public ICollection<CManual> Manuales { set; get; }

        //Metodos
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
