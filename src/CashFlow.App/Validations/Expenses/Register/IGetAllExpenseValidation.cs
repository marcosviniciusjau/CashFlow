﻿using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Expenses.Register;
public interface IGetAllExpenseValidation
{
    Task<ResponseExpenses> Execute();
}
