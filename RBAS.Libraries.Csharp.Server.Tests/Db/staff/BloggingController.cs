using Microsoft.Extensions.DependencyInjection;
using Moq;
using RBAS.Libraries.Csharp.Server.Db.Repository.Implementations;
using RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;
using RBAS.Libraries.Csharp.Server.Tests.Db.staff.Entities;

namespace RBAS.Libraries.Csharp.Server.Tests.Db.staff;

public class BloggingController
{
    private readonly ICrudRepository<Blog, long> _blogs;

    public BloggingController(SqlContext context)
    {
        var serviceProvider = new Mock<IServiceProvider>();
        serviceProvider
            .Setup(x => x.GetService(typeof(SqlContext)))
            .Returns(context);
        
        /*var serviceScope = new Mock<IServiceScope>();
        serviceScope.Setup(x => x.ServiceProvider)
            .Returns(serviceProvider.Object);
        
        var serviceScopeFactory = new Mock<IServiceScopeFactory>();
        serviceScopeFactory
            .Setup(x => x.CreateScope())
            .Returns(serviceScope.Object);
        
        serviceProvider
            .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
            .Returns(serviceScopeFactory.Object);*/
        
        var factory = new CrudFactory<SqlContext>(serviceProvider.Object);

        _blogs = factory.Get<Blog, long>();
    }

    public Blog Add(Blog blog)
    {
        return _blogs.AddAsync(blog).Result;
    }

    public Blog Get(long id)
    {
        return _blogs.FindAsync(id).Result;
    }
}