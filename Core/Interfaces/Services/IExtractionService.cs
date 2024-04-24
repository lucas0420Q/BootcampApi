using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IExtractionService
{
    Task<ExtractionDTO> Add(CreateExtractionModel model);
    Task<(bool isValid, string message)> ValidateTransactionRules(CreateExtractionModel model);
}
