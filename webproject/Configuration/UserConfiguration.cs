using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webproject.Models;

namespace webproject.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u=>u.notifications)
                .WithOne(n=>n.user)
                .HasForeignKey(n=>n.Id_User)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
