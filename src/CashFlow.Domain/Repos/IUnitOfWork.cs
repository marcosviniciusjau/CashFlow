namespace CashFlow.Domain.Repos;
public interface IUnitOfWork
{
    Task Commit();
}
