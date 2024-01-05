using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerApp.Models;

namespace ServerApp.Config
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasIndex(c => c.ClientVAT).IsUnique();
            builder.HasOne(c => c.Currency).WithMany(c=>c.Clients).HasForeignKey(c=>c.CurrencyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
