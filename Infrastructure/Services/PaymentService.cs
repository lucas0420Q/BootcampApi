using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentServiceDTO> Add(CreatePaymentserviceModel model)
    {
        return await _paymentRepository.Add(model);
    }

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreatePaymentserviceModel model)
    {
        return await _paymentRepository.ValidateTransactionRules(model);
    }
}
