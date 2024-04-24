using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IDepositRepository
{
    Task<DepositDTO> Add(CreateDepositModel model);
    Task<(bool isValid, string message)> ValidateTransactionRules(CreateDepositModel model);
}
