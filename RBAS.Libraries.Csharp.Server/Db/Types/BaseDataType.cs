using System.ComponentModel.DataAnnotations;

namespace RBAS.Libraries.Csharp.Server.Db.Types
{
    public class BaseDataType : BaseDataType<long> {}
    
    /// <summary>
    /// Базовый тип для всех моделей EF Core. Содержит такие поля как:
    /// Id - ключевое поле
    /// CreatedAt - дата создания
    /// UpdatedAt - дата последнего обновления
    /// </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class BaseDataType<TKey> : IBaseDataType<TKey> where TKey : unmanaged
    {
        protected BaseDataType()
        {
            Id = default;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.Now;
        }

        [Key]
        public TKey Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
