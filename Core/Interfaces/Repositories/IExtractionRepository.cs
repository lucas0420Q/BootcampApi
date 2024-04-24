using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IExtractionRepository 
{
    Task<ExtractionDTO> Add(CreateExtractionModel model);
    Task<(bool isValid, string message)> ValidateTransactionRules(CreateExtractionModel model);
}
