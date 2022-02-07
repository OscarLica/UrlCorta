using Microsoft.EntityFrameworkCore;
using NETChallenge.Data;
using NETChallenge.DTO;
using NETChallenge.Helper;
using NETChallenge.Models;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.Service
{
    public interface IServiceUrl
    {
        /// <summary>
        ///     Consulta un listado de urls
        /// </summary>
        /// <returns></returns>
        Task<List<Url>> GetAll();

        /// <summary>
        ///     Crea una nueva url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<Url> Create(Url url);

        /// <summary>
        ///     Consulta una url por su token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Url> GetByToken(string token);

        /// <summary>
        ///     Registra el click a la url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<Url> CountClick(Url url);

        /// <summary>
        ///     Genera un token para la url corta
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<ShortUrlHelper> GenerateToken();

        /// <summary>
        ///     Validar que el token sea unico
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> IsUniqueToken(string token);

        Task<List<MetricasDTO>> GetDailyClick(int urlId);
        Task<List<MetricasDTO>> GetByPlatforms(int urlId);
        Task<List<MetricasDTO>> GetByBrowser(int urlId);

        Task<List<UrlAPIDTO>> GetAllAPI();

    }

    public class ServiceUrl : IServiceUrl
    {
        private readonly ApplicationDbContext _Context;
        private readonly IBrowserDetector browserDetector;
        public ServiceUrl(ApplicationDbContext _context, IBrowserDetector browserDetector)
        {
            _Context = _context;
            this.browserDetector = browserDetector;

        }

        /// <summary>
        ///     Crea una nueva url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Url> Create(Url url)
        {
            var urlHelper = await GenerateToken();
            url.ShortUrl = urlHelper.Token;
            await _Context.Url.AddAsync(url);
            await _Context.SaveChangesAsync();

            return url;
        }

        /// <summary>
        ///     Consylta una url por el token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Url> GetByToken(string token)
            => await _Context.Url.FirstOrDefaultAsync(x => x.ShortUrl == token);


        public async Task<List<Url>> GetAll()
        {
            return await (from url in _Context.Url
                          let click = (from detail in _Context.UrlDetails
                                       where detail.UrlId == url.Id
                                       select detail.Count).Sum()
                          select new Url
                          {
                              Fecha = url.Fecha,
                              OriginalUrl = url.OriginalUrl,
                              ShortUrl = url.ShortUrl,
                              Usuario = url.Usuario,
                              Count = click
                          }).ToListAsync();
        }

        public async Task<Url> CountClick(Url url)
        {
            var detail = new UrlDetail
            {
                Count = 1,
                UrlId = url.Id,
                Fecha = DateTime.Now,
                OS = browserDetector.Browser?.OS,
                Browser = browserDetector.Browser?.Name,
            };
            await _Context.UrlDetails.AddAsync(detail);
            await _Context.SaveChangesAsync();
            return url;

        }

        public async Task<ShortUrlHelper> GenerateToken()
        {
            var urlHelper = new ShortUrlHelper();
            var isUnique = await IsUniqueToken(urlHelper.Token);

            while (!isUnique)
            {
                urlHelper = new ShortUrlHelper();
                isUnique = await IsUniqueToken(urlHelper.Token);
            }

            return urlHelper;
        }

        public async Task<bool> IsUniqueToken(string token)
        {
            return !await _Context.Url.AnyAsync(x => x.ShortUrl == token);
        }

        public async Task<List<MetricasDTO>> GetDailyClick(int urlId)
        {
            var detail = await _Context.UrlDetails.Where(x => x.UrlId == urlId).ToListAsync();

            return detail.GroupBy(x => x.Fecha.Date.Day)
                          .Select(metrica => new MetricasDTO
                          {
                              Key = metrica.Key.ToString(),
                              Value = metrica == null ? default : metrica.Sum(x => x.Count)
                          }).ToList();
        }

        public async Task<List<MetricasDTO>> GetByPlatforms(int urlId)
        {
            var detail = await _Context.UrlDetails.Where(x => x.UrlId == urlId).ToListAsync();

            return detail.GroupBy(x => x.OS)
                          .Select(metrica => new MetricasDTO
                          {
                              Key = metrica.Key.ToString(),
                              Value = metrica == null ? default : metrica.Sum(x => x.Count)
                          }).ToList();
        }

        public async Task<List<MetricasDTO>> GetByBrowser(int urlId)
        {
            var detail = await _Context.UrlDetails.Where(x => x.UrlId == urlId).ToListAsync();

            return detail.GroupBy(x => x.Browser)
                          .Select(metrica => new MetricasDTO
                          {
                              Key = metrica.Key.ToString(),
                              Value = metrica == null ? default : metrica.Sum(x => x.Count)
                          }).ToList();
        }

        public async Task<List<UrlAPIDTO>> GetAllAPI()
        {
            var query = await (from url in _Context.Url
                               let click = _Context.UrlDetails.Where(x => x.UrlId == url.Id).Sum(s => s.Count)
                               select new UrlAPIDTO
                               {
                                   type = "url",
                                   id = url.Id,
                                   attributes = new attibute
                                   {
                                       createat = url.Fecha,
                                       originalUrl = url.OriginalUrl,
                                       url = $"https://domain/{url.ShortUrl}",
                                       clicks = click
                                   },
                                   relationships = new relationships
                                   {
                                       clicks = new clicks
                                       {
                                           data = (from detail in _Context.UrlDetails
                                                   where detail.UrlId == url.Id
                                                   select new clickData
                                                   {
                                                       id = detail.Id,
                                                       type = "clicks"
                                                   }).ToList()
                                       }
                                   }
                               }).ToListAsync();

            return query;
        }
    }
}
