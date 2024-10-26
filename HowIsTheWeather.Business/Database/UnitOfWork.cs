using Microsoft.EntityFrameworkCore.Storage;

namespace HowIsTheWeather.Service.Database;

internal class UnitOfWork : IUnitOfWork
{

    public HowIsTheWeatherDbContext Context { get; }

    private readonly HowIsTheWeatherDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(HowIsTheWeatherDbContext context)
    {
        _context = context;
        Context = _context;
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitBulkAsync()
    {
        try
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();

        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_context != null)
            {
                _transaction.Dispose();
                _context.Dispose();
            }
        }
    }
}
