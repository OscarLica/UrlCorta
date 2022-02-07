using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.Models
{
    public class Url
    {
        /// <summary>
        ///     Primary key de la entidad
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Url original
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        ///     Url corta
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        ///     Fecha de creación
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        ///     username del usuario en sessión
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        ///     Contador
        /// </summary>
        [NotMapped]
        public int Count { get; set; }

        /// <summary>
        ///     Propiedad de navegación hacia el detalle de la url
        /// </summary>
        public virtual List<UrlDetail> UrlDetails { get; set; }
    }
}
