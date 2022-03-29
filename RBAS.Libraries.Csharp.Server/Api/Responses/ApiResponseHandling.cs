using System.Net;
using Microsoft.AspNetCore.Http;

namespace RBAS.Libraries.Csharp.Server.Api.Responses;

public class ApiResponseHandling
{
    private readonly RequestDelegate _delegate;

    public ApiResponseHandling(RequestDelegate @delegate)
    {
        _delegate = @delegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var stream = new MemoryStream();
        
        var response = context.Response;

        ApiResponse? res = null;
        if (response.StatusCode >= 400)
        {
            res = Fail(response);
        }

        await _delegate(context);
    }

    private ApiResponse Fail(HttpResponse response)
    {
        return response.StatusCode switch
        {
            400 => ResponsesFactory.BadRequest(),
            403 => ResponsesFactory.Unauthorized(),
            _ => ResponsesFactory.NotFound()
        };
    }
}