namespace StoreManagementSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using Newtonsoft.Json;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {

        private readonly IEnumerable<Category> categories =
            JsonConvert.DeserializeObject<Category[]>(File.ReadAllText("../StoreManagementSystem.Data/SeedData/CategorySeed.json"))!;

        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(categories);
        }
    }
}
