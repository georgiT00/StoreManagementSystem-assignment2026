namespace StoreManagementSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using Newtonsoft.Json;

    public class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        private readonly IEnumerable<Supplier> suppliers =
            JsonConvert.DeserializeObject<Supplier[]>(File.ReadAllText("Data/SeedData/SupplierSeed.json"))!;
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {
            entity.HasData(suppliers);
        }
    }
}
