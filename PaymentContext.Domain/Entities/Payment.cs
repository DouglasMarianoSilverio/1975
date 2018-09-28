
using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment
    {
        protected Payment( DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, string document, string address, string email)
        {
            Number = Guid.NewGuid().ToString();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Payer = payer;
            Document = document;
            Address = address;
            Email = email;
        }

        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public Decimal Total { get; private set; }
        public Decimal TotalPaid { get; private set; }
        public string Payer { get; private set; }
        public string Document { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

    }

    public class BoletoPayment : Payment
    {
        public string Barcode { get; private set; }
        public string BoletoNumber { get; private set; }

    }

    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }

    public class PaypalPayment : Payment
    {
        public string TransactionCode { get; private set; }

    }
}