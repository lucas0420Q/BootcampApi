using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentServiceDTO> Add(CreatePaymentserviceModel model);
    Task<(bool isValid, string message)> ValidateTransactionRules(CreatePaymentserviceModel model);
}
