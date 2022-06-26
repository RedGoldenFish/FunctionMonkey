using Flurl;
using Flurl.Http;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Http
{
    public class HttpFunctionWithNoRouteShould : AbstractHttpFunctionTest
    {
        [Fact]
        public async Task Return200()
        {
            var response = await Settings.Host
                .AppendPathSegment("HttpHttpCommandWithNoRoute")
                .GetAsync();

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }
    }
}
