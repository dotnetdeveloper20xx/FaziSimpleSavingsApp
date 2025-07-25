using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace FaziSimpleSavings.Test.TestUtilities;

public abstract class BaseTest
{
    protected IAppDbContext Context { get; private set; }

    protected BaseTest()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new TestDbContext(options);
    }
}
