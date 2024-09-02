﻿using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.App.Validations.Users;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Length >= 8;
    }
}
