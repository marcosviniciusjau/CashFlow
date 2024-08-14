using CashFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpenses
{
    Task Add(Expense expense);
    Task Delete(Expense expense);
    Task Update(Expense expense);
    Task <List<Expense>> GetAll();
    Expense GetById(int id);
}
