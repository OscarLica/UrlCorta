using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.Models
{
    public class UrlDetail
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Fecha de click de la url corta
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        ///     url id al que pertecen el detalle
        /// </summary>
        public int UrlId { get; set; }

        /// <summary>
        ///     Sistema operativo
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        ///     Nombre del navegador
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        ///     Contador de clicks
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     Propiedad de navegación hacia Url
        /// </summary>
        public virtual Url Url { get; set; }

    }
}
