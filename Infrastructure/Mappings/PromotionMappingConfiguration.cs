﻿using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings
{
    public class PromotionMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatePromotionModel, Promotion>()
                .Map(dest => dest.Start, src => src.Start)
                .Map(dest => dest.End, src => src.End)
                .Map(dest => dest.Discount, src => src.Discount);

            config.NewConfig<Promotion, PromotionDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Start, src => src.Start)
                .Map(dest => dest.End, src => src.End)
                .Map(dest => dest.Discount, src => src.Discount)
                .Map(dest => dest.Enterprises, src => src.PromotionsEnterprises.Select(pe => pe.Enterprise));
        }
    }
}