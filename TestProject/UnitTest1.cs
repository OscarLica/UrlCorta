using Moq;
using NETChallenge.Data;
using NETChallenge.Models;
using NETChallenge.Service;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using System.Threading.Tasks;
using TestProject.Config;

namespace TestProject
{
    public class Tests
    {
        private IBrowserDetector browserDetector;
        private ApplicationDbContext _Context;
        [SetUp]
        public void Setup()
        {
            _Context = ApplicationDbContextInMemory.Get();
            browserDetector = new Mock<IBrowserDetector>().Object;
        }


        [Test]
        public async Task CreateShortUrl()
        {
            // arrange
            var Service = new ServiceUrl(_Context, browserDetector);
            var url = new Url
            {
                OriginalUrl = "https://google.com",
                Fecha = System.DateTime.Now,
                Usuario = "oscar"
            };
            // act
            var result = await Service.Create(url);

            Assert.IsTrue(result.Id > 0 && !string.IsNullOrEmpty(result.ShortUrl));
        }


        [Test]
        public async Task GenerateToken()
        {
            // arrange
            var Service = new ServiceUrl(_Context, browserDetector);
            // act
            var result = await Service.GenerateToken();

            Assert.IsTrue(!string.IsNullOrEmpty(result.Token) && result.Token.Length == 5);
        }
    }
}