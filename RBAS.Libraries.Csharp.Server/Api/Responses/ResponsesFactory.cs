using System.Net;

namespace RBAS.Libraries.Csharp.Server.Api.Responses
{
    public static class ResponsesFactory
    {
        public static ApiResponse Ok() => Ok<object>(null);
        public static ApiResponse Ok<TData>(TData? data) => new ApiResponse<TData?>(data);
        
        public static ApiResponse Forbidden() => Response(HttpStatusCode.Forbidden, "Access denied", null);
        public static ApiResponse Forbidden(string? reason) => Response(HttpStatusCode.Forbidden, "Access denied", reason);

        public static ApiResponse Unauthorized() => Unauthorized("");        
        public static ApiResponse Unauthorized(string? reason) => Response(HttpStatusCode.Unauthorized, "Not authorized access", reason);

        public static ApiResponse BadRequest() => BadRequest("");
        public static ApiResponse BadRequest(string? reason) => Response(HttpStatusCode.BadRequest, "Bad request data", reason);

        public static ApiResponse NotFound() => NotFound("");
        
        public static ApiResponse NotFound(string? reason) => Response(HttpStatusCode.NotFound, "Not found", reason);

        public static ApiResponse InternalServerError() => Response(HttpStatusCode.InternalServerError, "Internal server error", null);

        public static ApiResponse InternalServerException(string? reason) => Response(HttpStatusCode.InternalServerError, "Internal server error", reason);

        private static ApiResponse Response(HttpStatusCode code, string? exception, string? reason) =>
            new(code, exception, reason);
    }
}