﻿namespace CashFlow.Communication.Responses;
public class RequestUpdateUser
{
    public string CompanyName { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
