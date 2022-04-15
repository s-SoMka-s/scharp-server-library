using System.Net;
using RBAS.Libraries.Csharp.Server.Api.Responses;

using Xunit;

namespace RBAS.Libraries.Csharp.Server.Tests.Api.Responses;

public class TestApiResponsesFactory
{
    public class TestApiResponseFactory
    {
        [Fact]
        public void CreateApiResponse()
        {
            CheckResponse(HttpStatusCode.Unauthorized, ResponsesFactory.Unauthorized("Bad"));
            CheckResponse(HttpStatusCode.InternalServerError, ResponsesFactory.InternalServerException("Bad"));
            CheckResponse(HttpStatusCode.OK, ResponsesFactory.Ok("OK"));
            CheckResponse(HttpStatusCode.OK, ResponsesFactory.Ok());
            CheckResponse(HttpStatusCode.Forbidden, ResponsesFactory.Forbidden("Bad"));
            CheckResponse(HttpStatusCode.BadRequest, ResponsesFactory.BadRequest("Bad"));
        }
        private static void CheckResponse(HttpStatusCode code, ApiResponse response)
        {
            try
            {
                throw response;
            }
            catch (ApiResponse catched)
            {
                Assert.Equal(code, catched.StatusCode);
                Assert.NotEqual(string.Empty, catched.ToString());
            }
        }
    }
}