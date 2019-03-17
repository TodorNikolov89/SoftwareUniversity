namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using System;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(p => p.Email)
                .HasMaxLength(80)
                .IsUnicode(false);

            builder
                .Property(p => p.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
