using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<PaymentServiceDTO> Add(CreatePaymentserviceModel model);
    Task<(bool isValid, string message)> ValidateTransactionRules(CreatePaymentserviceModel model);
}
