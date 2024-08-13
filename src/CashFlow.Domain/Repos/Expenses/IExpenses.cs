using CashFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpenses
{
    void Add(Expense expense);
    void Delete(Expense expense);
    void Update(Expense expense);
    List<Expense> Get();
    Expense GetById(int id);
}
