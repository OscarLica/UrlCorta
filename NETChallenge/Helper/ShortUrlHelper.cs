using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.Helper
{
    public class ShortUrlHelper
    {
        public string Token { get; set; }
        public ShortUrlHelper()
        {
            string caracteres = string.Empty;

            Enumerable.Range(48, 75).Where(i => i < 58 || i > 64 && i < 91 || i > 96).OrderBy(o => new Random().Next()).ToList().ForEach(c => caracteres += Convert.ToChar(c)) ;

            var start = 62;
            var endIndex = 5;

            Token = caracteres.Substring((start + endIndex) > caracteres.Length ? (start - endIndex) : start, endIndex);

            Token = Token.ToUpper();
        }
    }
}
