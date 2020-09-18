namespace DSPA_MeAudVis.Web.Data.Entities
{
    using DSPA_MeAudVis.Web.Controllers.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a los prestamos que se realizan en el sistema
    /// </summary>
    /// Version 2.1
    /// Fecha de creacion 09/09/20
    /// Creador David Hernandez
    /// Fecha de Modificacion 14/09/20
    /// Modificador Isaac Librado
    public class Borrowing : IEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Borrowing Detail")]
        public BorrowingDetail BorrowingDetail { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Borrower")]
        public Sizar SizerOut { set; get; }

        [Display(Name = "Receiver")]
        public Sizar SizerIn { set; get; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Petitioner")]
        public Petitioner Petitioner { set; get; }
       
    }
}
