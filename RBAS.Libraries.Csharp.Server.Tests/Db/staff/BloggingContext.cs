using Microsoft.EntityFrameworkCore;
using RBAS.Libraries.Csharp.Server.Tests.Db.staff.Entities;

namespace RBAS.Libraries.Csharp.Server.Tests.Db.staff;

public class SqlContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
}