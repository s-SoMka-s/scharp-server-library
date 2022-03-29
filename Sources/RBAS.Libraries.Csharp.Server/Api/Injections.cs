using Microsoft.AspNetCore.Builder;

namespace RBAS.Libraries.Csharp.Server.Api;

public class Injections
{
 public static IApplicationBuilder UseRBASApiProtocol (this IApplicationBuilder app)
 {
  app.UseMiddleware<>()
 }  
}