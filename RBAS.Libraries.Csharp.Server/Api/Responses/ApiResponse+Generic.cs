using System.Net;

namespace RBAS.Libraries.Csharp.Server.Api.Responses;

public class ApiResponse<TData> : ApiResponse
{
    public new TData? Data { get; set; }

    public ApiResponse(TData? data) : this(HttpStatusCode.OK, data)
    {
    }

    public ApiResponse(HttpStatusCode statusCode, TData? data) : base(statusCode)
    {
        Data = data;
    }
    
    protected override void CollectFields(Dictionary<string, object?> container)
    {
        base.CollectFields(container);
        
        container["data"] = Data;
    }
}
