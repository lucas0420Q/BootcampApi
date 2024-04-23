//using Core.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Configurations;

//public class ProductConfiguration : IEntityTypeConfiguration<Product>
//{
//    public void Configure(EntityTypeBuilder<Product> entity)
//    {
//        entity.HasKey(p => p.Id);

//        entity
//            .Property(p => p.ProductType)
//            .IsRequired();
//    }
//}
