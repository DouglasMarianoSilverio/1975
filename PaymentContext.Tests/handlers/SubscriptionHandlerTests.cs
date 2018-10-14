

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests

    {

        [TestMethod]
        public void ShouldReturnSuccessWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayner";
            command.Document = "99999999999";
            command.Email = "hell2@balta.io";
            command.Barcode = "1234567890";
            command.BoletoNumber = "12345612";
            command.PaymentNumber = "12321";
            command.PaidDate = System.DateTime.Now;
            command.ExpireDate = System.DateTime.Now.AddMonths(1);
            command.Total = 80;
            command.TotalPaid = 80;
            command.Payer = "Wayne Tech";
            command.PayerDocument = "12346578901";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Street = "asd";
            command.Number = "asd";
            command.Neighborhood = "asd";
            command.City = "asd";
            command.State = "asd";
            command.Country = "US";
            command.ZipCode = "12235340";
            command.PayerEmail = "batman@dc.com";
            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);



        }
    }
}