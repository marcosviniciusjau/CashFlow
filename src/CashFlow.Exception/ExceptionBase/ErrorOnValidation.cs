﻿namespace CashFlow.Exception.ExceptionBase;
public class ErrorOnValidation : CashFlowException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidation(List<string> errorMessages) : base(string.Empty)
    {
        Errors = errorMessages;
    }
}