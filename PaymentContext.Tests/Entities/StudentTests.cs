using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests

    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;

        private readonly Student _student;
        

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayner");
            _document = new Document("00006282040", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _student = new Student(_name, _document, _email);
            _address = new Address("rua 1", "666", "Bairro", "Gotham", "SP", "BR", "13400-123");
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
             var _subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678",
           System.DateTime.Now, System.DateTime.Now.AddDays(5), 10, 10, "Waynetech", _document, _address, _email);
          
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        public void ShouldReturnErrorWhenHadActiveSubscriptionHasNoPayment()
        {
           var  _subscription = new Subscription(null);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var _subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678",
           System.DateTime.Now, System.DateTime.Now.AddDays(5), 10, 10, "Waynetech", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            

            Assert.IsTrue(_student.Valid);
        }
    }
}