﻿namespace BillsPaymentSystem.Models
{
    using BillsPaymentSystem.Models.Attributes;
    using BillsPaymentSystem.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaymentMethod
    {
        public int Id { get; set; }

        public PaymentType Type { get; set; }

        public int UserId { get; set; }

        [Xor(nameof(CreditCardId))]
        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }

    }
}