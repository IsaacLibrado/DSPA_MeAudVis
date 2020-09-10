namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los prestamos que se realizan en el sistema
    /// </summary>
    /// Version 1.1
    /// Fecha de creacion 09/09/20
    /// Creador David Hernandez
    /// Fecha de Modificacion 10/09/20
    /// Modificador Isaac Librado
    public class CPrestamo : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Materiales prestado")]
        public ICollection<CMaterial> Materiales { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Becario que presta")]
        public CBecario becarioSalida { set; get; }

        [Display(Name = "Becario que recibe")]
        public CBecario becarioEntrega { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Solicitante")]
        public CSolicitante solicitante { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Fecha y hora del prestamo")]
        public DateTime FechaHoraSalida { set; get; }

        [Display(Name = "Fecha y hora de recibido")]
        public DateTime FechaHoraEntrega { set; get; }

        [Display(Name = "Estado del prestamo")]
        public bool Estado { set; get; }

    }
}
