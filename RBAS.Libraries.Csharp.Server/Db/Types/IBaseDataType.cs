namespace RBAS.Libraries.Csharp.Server.Db.Types
{
    public interface IBaseDataType<TKey> where TKey : unmanaged
    {
        TKey Id { get; set; }

        DateTimeOffset CreatedAt { get; set; }
        
        DateTimeOffset UpdatedAt { get; set; }
    }
}
