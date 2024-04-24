using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExtractionService : IExtractionService
    {
        private readonly IExtractionRepository _extractionRepository;

        public ExtractionService(IExtractionRepository extractionRepository)
        {
            _extractionRepository = extractionRepository;
        }

        public async Task<ExtractionDTO> Add(CreateExtractionModel model)
        {
            return await _extractionRepository.Add(model);
        }

        public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateExtractionModel model)
        {
            return await _extractionRepository.ValidateTransactionRules(model);
        }
    }
}
