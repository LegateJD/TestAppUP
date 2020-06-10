using TestApp.Domain.Interfaces.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace TestApp.Dal.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestAppContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(
            TestAppContext context,
            ILogger<UnitOfWork> logger)
        {
            _logger = logger;
            _context = context;
            _logger = logger;
        }

        public void Commit()
        {
            _context.SaveChanges();
            _logger.LogDebug("Transaction's commited");
        }
    }
}
