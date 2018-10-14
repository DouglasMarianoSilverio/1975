using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {

        }

        public bool DocumentExists(string document)
        {
            if (document.Equals("99999999999"))
                return true;
            return false;

        }

        public bool EmailExists(string email)
        {
           if (email.Equals("hello@balta.io"))
                return true;
            return false;
        }
    }
}