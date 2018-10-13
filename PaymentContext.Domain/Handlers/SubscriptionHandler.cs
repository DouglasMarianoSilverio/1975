using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
    Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;
        public readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository,  IEmailService emailService)
        {
            this._repository = repository;
            _emailService= emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "não foi possivel realizar sua assinatura.");
            }
            //verificar se o documento ja esta cadastrado.
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso");
            }

            //Verificar se o email ja esta cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este email já está em uso");
            }

            //gerar os VOS
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);

            var address = new Address(command.Street, command.Number, command.Neighborhood
            , command.City, command.State, command.Country, command.ZipCode);


            //gerar as entidades
            var student = new Student(name, document, email);
            //assumindo q seja um boleto mensal.
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new BoletoPayment(
                command.Barcode, 
                command.BoletoNumber, 
                command.PaidDate,
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);
            //Relacionamentos.
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);   

            //agrupar as validacoes
            AddNotifications(name,document,email,address,student,subscription,payment);

            //salvar as informações
            _repository.CreateSubscription(student);

            //enviar email de boas vindas.
             
            _emailService.Send(student.Name.ToString(), student.Email.Address,"Bem vindo ao balta.io","Sua assinatura foi criada");
            //retornar informacos

            return new CommandResult(true, "Assinatura realizada com sucesso.");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
              //Fail fast Validations
            /*command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "não foi possivel realizar sua assinatura.");
            }*/

            //verificar se o documento ja esta cadastrado.
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso");
            }

            //Verificar se o email ja esta cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este email já está em uso");
            }

            //gerar os VOS
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);

            var address = new Address(command.Street, command.Number, command.Neighborhood
            , command.City, command.State, command.Country, command.ZipCode);


            //gerar as entidades
            var student = new Student(name, document, email);
            //assumindo q seja um boleto mensal.
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new PayPalPayment(
                command.TransactionCode, 
                
                command.PaidDate,
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);
            //Relacionamentos.
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);   

            //agrupar as validacoes
            AddNotifications(name,document,email,address,student,subscription,payment);

            //salvar as informações
            _repository.CreateSubscription(student);

            //enviar email de boas vindas.
             
            _emailService.Send(student.Name.ToString(), student.Email.Address,"Bem vindo ao balta.io","Sua assinatura foi criada");
            //retornar informacos

            return new CommandResult(true, "Assinatura realizada com sucesso.");
        }
    }
}