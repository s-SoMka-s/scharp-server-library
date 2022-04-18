using System.Text;
using System.Text.Json;
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
        await using var stream = new MemoryStream();
        using var reader = new StreamReader(stream, Encoding.Default);
        
        var defaultBody = context.Response.Body;

        try
        {
            context.Response.Body = stream;
            SetupContentType(context);

            await _delegate(context);
            
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var content = await reader.ReadToEndAsync();


            if (context.Response.StatusCode >= 400)
            {
                //Problem with request
                var response = Fail(context, content);
                await CompleteApiResponseAsync(context, response, defaultBody);
            }
            else
            {
                //Api requests
                var response = Success(content);
                await CompleteApiResponseAsync(context, response, defaultBody);
            }
        }
        catch (ApiResponse response)
        {
            await CompleteApiResponseAsync(context, response, defaultBody);
        }
        catch (Exception exception)
        {
            var response = ResponsesFactory.InternalServerException(exception.ToString());
            
            await CompleteApiResponseAsync(context, response, defaultBody);
        }
    }

    private static void SetupContentType(HttpContext context)
    {
        var headers = context.Request.Headers;

        const string contentType = "Content-Type";
        if (headers.ContainsKey(contentType))
        {
            headers.Remove(contentType);
        }

        headers[contentType] = "application/json";
    }
    
    private static ApiResponse Success(string? content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return ResponsesFactory.Ok(content);
        }

        try
        {
            var obj = JsonSerializer.Deserialize<object>(content);

            return ResponsesFactory.Ok(obj);
        }
        catch
        {
            return ResponsesFactory.Ok(content);
        }
    }
    
    private static ApiResponse Fail(HttpContext context, string? content)
    {
        var code = context.Response.StatusCode;

        return code switch
        {
            400 => ResponsesFactory.BadRequest(content),
            401 => ResponsesFactory.Unauthorized(),
            403 => ResponsesFactory.Forbidden(),
            404 => ResponsesFactory.NotFound(content),
            _ => ResponsesFactory.InternalServerError()
        };
    }
    
    private static async Task CompleteApiResponseAsync(HttpContext context, ApiResponse response, Stream defaultBody)
    {
        context.Response.Body = defaultBody;
        context.Response.StatusCode = (int)response.StatusCode;
        context.Response.ContentType = "application/json";
        context.Response.Headers.Remove("Content-Length");

        await context.Response.WriteAsync(response.ToString());
    }
}