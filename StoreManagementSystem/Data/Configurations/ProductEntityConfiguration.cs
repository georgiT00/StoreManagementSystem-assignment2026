namespace StoreManagementSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using Newtonsoft.Json;

    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        private readonly IEnumerable<Product> products = 
            JsonConvert.DeserializeObject<Product[]>(File.ReadAllText("Data/SeedData/ProductSeed.json"))!;
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasData(products);
        }
    }
}
