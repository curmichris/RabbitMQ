using PaymentsApi.Models;
using RabbitMQ.Client;
using Common;
using System;

namespace PaymentsApi.RabbitMQ
{
    public class RabbitMQClient
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "Topic_Exchange";
        private const string CardPaymentQueueName = "CardPaymentTopic_Queue";
        private const string PurchaseOrderQueueName = "PurchaseOrderTopic_Queue";
        private const string AllQueueName = "AllTopic_Queueu";

        public RabbitMQClient()
        {
            CreateConnection();
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "topic");

            _model.QueueDeclare(CardPaymentQueueName, true, false, false, null);
            _model.QueueDeclare(PurchaseOrderQueueName, true, false, false, null);
            _model.QueueDeclare(AllQueueName, true, false, false, null);

            _model.QueueBind(CardPaymentQueueName, ExchangeName, "payment.card");
            _model.QueueBind(PurchaseOrderQueueName, ExchangeName, "payment.purchaseorder");
            _model.QueueBind(AllQueueName, ExchangeName, "payment.*");
        }

        public void SendPayment(CardPayment payment)
        {
            SendMessage(payment.Serialize(), "payment.card");
            Console.WriteLine(" Payment Sent {0}, ${1}", payment.CardNumber, payment.Amount);
        }

        public void Close()
        {
            _connection.Close();
        }

        private void SendMessage(byte[] message, string routingKey)
        {
            _model.BasicPublish(ExchangeName, routingKey, null, message);
        }
    }
}
