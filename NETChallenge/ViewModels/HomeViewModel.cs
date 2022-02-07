using NETChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.ViewModels
{
    public class HomeViewModel
    {
        /// <summary>
        ///     Objeto de la nueva url corta a crear
        /// </summary>
        public Url NewUrl{ get; set; }

        /// <summary>
        ///     Listado de urls, se utilizaremos en la vista index del urlcontrller
        /// </summary>
        public List<Url> Urls { get; set; }
    }
}
