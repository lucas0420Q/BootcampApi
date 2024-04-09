﻿using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class CurrencyMappingConfiguration
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Currency, CurrencyDTO>()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.BuyValue, src => src.BuyValue)
        .Map(dest => dest.SellValue, src => src.SellValue);

        config.NewConfig<CurrencyDTO, Currency>()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.BuyValue, src => src.BuyValue)
        .Map(dest => dest.SellValue, src => src.SellValue);
    }
}
