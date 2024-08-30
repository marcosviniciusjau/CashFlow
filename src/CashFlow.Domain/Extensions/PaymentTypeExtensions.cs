using CashFlow.Domain.Entities.Enums;

namespace CashFlow.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentTypes paymentType)
    {
        return paymentType switch
        {
            PaymentTypes.Cash => "Dinheiro",
            PaymentTypes.CreditCard => "Cartão crédito",
            PaymentTypes.DebitCard => "Cartão débito",
            PaymentTypes.EletronicTransfer => "TED",
            _ => string.Empty
        };
    }
}
