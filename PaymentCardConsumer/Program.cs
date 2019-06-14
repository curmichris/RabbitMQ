using Common.RabbitMQ;
using System;

namespace PaymentCardConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQConsumer client = new RabbitMQConsumer();
            client.CreateConnection();
            client.ProcessMessages();
            Console.WriteLine("Hello World!");
        }
    }
}
