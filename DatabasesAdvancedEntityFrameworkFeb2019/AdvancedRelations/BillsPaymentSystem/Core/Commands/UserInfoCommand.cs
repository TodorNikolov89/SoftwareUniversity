using BillsPaymentSystem.Core.Commands.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillsPaymentSystem.Core.Commands
{
    public class UserInfoCommand : ICommand
    {
        private readonly BillsPaymentSystemContext context;

        public UserInfoCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);

            var user = this.context.Users.FirstOrDefault(x => x.UserId == userId);


            if (user == null)
            {
                throw new ArgumentNullException("User not found!");
            }
            ;
            StringBuilder sb = new StringBuilder();
            var fullName = user.FirstName + " " + user.LastName;


            sb.AppendLine($"User: {user.FirstName} {user.LastName}");


            foreach (var p in user.PaymentMethods.Where(x=> (int)x.Type == 0))
            {
                sb.AppendLine($"-- ID: {p.BankAccountId}");
                sb.AppendLine($"---Balance: {p.BankAccount.Balance}");
                sb.AppendLine($"--- Bank: {p.BankAccount.BankName}");
                sb.AppendLine($"--- SWIFT: {p.BankAccount.Swift}");
            }

            sb.AppendLine($"Credit Cards:");
            foreach (var p in user.PaymentMethods.Where(x => (int)x.Type == 1))
            {
                sb.AppendLine($"-- ID: {p.BankAccountId}");
                sb.AppendLine($"---Limit: {p.CreditCard.Limit}");
                sb.AppendLine($"---Money Owed: {p.CreditCard.MoneyOwed}");
                sb.AppendLine($"--- Limit Left: {p.CreditCard.LimitLeft}");
                sb.AppendLine($"--- Expiration Date: {p.CreditCard.ExpirationDate}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }
    }
}
