using CashFlow.Domain.Repos;

namespace CashFlow.Infra.DataAccess;

internal class UnitOfWork : IUnityOfWork
{
    private readonly CashFlowDbContext _dbContext;
    public UnitOfWork(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Commit()
    {
        _dbContext.SaveChanges();
    }
}
