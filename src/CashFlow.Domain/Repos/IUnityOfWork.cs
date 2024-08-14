namespace CashFlow.Domain.Repos;
public interface IUnityOfWork
{
    Task Commit();
}
