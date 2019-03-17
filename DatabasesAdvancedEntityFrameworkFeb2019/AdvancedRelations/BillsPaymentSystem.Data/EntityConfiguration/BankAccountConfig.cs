namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder
                .Property(p => p.BankName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(p => p.Swift)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
