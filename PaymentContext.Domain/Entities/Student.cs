using System;
using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {

        private List<Subscription> _subscriptions;
        
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            //agrupa as notificações
            AddNotifications(name,document,email);

        }

        public Name Name;


        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            //se tiver uma assinatura ativa, cancela.
            //se o nome nao tiver 30 caracteres

            foreach (var sub in this.Subscriptions)
            {
                sub.Inactivate();
            }
            this._subscriptions.Add(subscription);
        }
    }
}