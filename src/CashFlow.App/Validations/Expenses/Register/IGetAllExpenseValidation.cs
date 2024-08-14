using CashFlow.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.App.Validations.Expenses.Register;
public interface IGetAllExpenseValidation
{
    Task<ResponseExpenses> Execute();
}
