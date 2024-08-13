namespace CashFlow.Domain.Repos;
public interface IUnityOfWork
{
    void Commit();
}
