using Microsoft.EntityFrameworkCore;
using Moq;
using RBAS.Libraries.Csharp.Server.Tests.Db.staff;
using RBAS.Libraries.Csharp.Server.Tests.Db.staff.Entities;
using Xunit;

namespace RBAS.Libraries.Csharp.Server.Tests.Db;

public class TestRepository
{
    [Fact]
    public void Create()
    {
        var data = new List<Blog>
        {
            new() { Name = "BBB" },
            new() { Name = "ZZZ" },
            new() { Name = "AAA" },
        }.AsQueryable();
        
        var blogSet = new Mock<DbSet<Blog>>();
        blogSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(data.Provider);
        blogSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(data.Expression);
        blogSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(data.ElementType);
        blogSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<SqlContext>();
        mockContext.Setup(m => m.Set<Blog>()).Returns(blogSet.Object);

        var controller = new BloggingController(mockContext.Object);
        var blog = controller.Add(new Blog {Name = "1"});
        
        // TODO Add checks
        //mockContext.Verify(m => m.AddRangeAsync(It.IsAny<Blog>()), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(new CancellationToken()), Times.Once());

        //var blog2 = controller.Get(blog.Id);
        //Assert.Equal(blog, blog2);
    }
}