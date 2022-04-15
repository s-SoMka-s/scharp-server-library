using Microsoft.AspNetCore.Builder;
using RBAS.Libraries.Csharp.Server.Api.Responses;

namespace RBAS.Libraries.Csharp.Server.Api;

public static class Injections
{
    public static IApplicationBuilder UseRBASApiProtocol(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ApiResponseHandling>();

        return builder;
    }
}