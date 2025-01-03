﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
namespace CashFlow.Infra.DataAccess.Repos;

internal class ExpensesRepo : IExpenseReadOnly,IExpensesWrite, IExpensesUpdate
{
    private readonly CashFlowDbContext _dbContext;
  
    public ExpensesRepo(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }
    public async Task<List<Expense>> GetAll(User user)
    {
        return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
    }

    public async Task<List<ExpenseDTO>> GetTitlesWithAmount(User user)
    {

        return await _dbContext.Expenses.AsNoTracking()
            .Where(expense => expense.UserId == user.Id)
            .OrderByDescending(expense => expense.Amount)
            .Select(expense => new ExpenseDTO
            {
                Title = expense.Title,
                Amount = expense.Amount
            })
            .ToListAsync();

    }

    public async Task<List<ExpenseDTO>> GetTitlesWithAmountByMonth(User user, DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext.Expenses.AsNoTracking()
            .Where(expense => expense.UserId == user.Id && expense.Date >= startDate && expense.Date <= endDate)
            .OrderByDescending(expense => expense.Amount)
            .Select(expense => new ExpenseDTO
            {
                Title = expense.Title,
                Amount = expense.Amount
            })
            .ToListAsync();
    }


    public async Task Delete(long id)
    {
        var result = await _dbContext.Expenses.FindAsync(id);
        _dbContext.Expenses.Remove(result!);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

     async Task<Expense?> IExpenseReadOnly.GetById(User user, long id)
     {
        return await GetFullExpense()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
     } 

     async Task<Expense?> IExpensesUpdate.GetById(User user, long id)
    {
        return await GetFullExpense()
           .FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
    }

    public async Task<List<Expense>> FilterByMonth(User user, DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Expenses
            .AsNoTracking()
            .Where(expense => expense.UserId == user.Id && expense.Date >= startDate && expense.Date <= endDate)
            .OrderBy(expense=> expense.Date)
            .ThenBy(expense => expense.Title)
            .ToListAsync();
    }

    private IIncludableQueryable<Expense, ICollection<Tag>> GetFullExpense()
    {
        return _dbContext.Expenses
            .Include(expense => expense.Tags);
    }
}
