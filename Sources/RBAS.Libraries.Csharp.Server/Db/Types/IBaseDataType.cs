namespace RBAS.Libraries.Csharp.Server.Db.Types
{
    public interface IBaseDataType
    {
        Guid Id { get; set; }

        DateTimeOffset CreatedAt { get; set; }
    }
}
