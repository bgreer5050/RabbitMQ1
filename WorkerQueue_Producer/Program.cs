using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace WorkerQueue_Producer
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "WorkerQueue_Queue";

        static void Main(string[] args)
        {
            var payment1 = new Payment { AmounToPay = 250.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment2 = new Payment { AmounToPay = 25.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment3 = new Payment { AmounToPay = 12.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment4 = new Payment { AmounToPay = 84.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment5 = new Payment { AmounToPay = 3.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment6 = new Payment { AmounToPay = 19.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment7 = new Payment { AmounToPay = 16.7m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment8 = new Payment { AmounToPay = 33.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment9 = new Payment { AmounToPay = 29.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
            var payment10 = new Payment { AmounToPay = 8.40m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };

            CreateConnection();

            SendMessage(payment1);
            SendMessage(payment2);
            SendMessage(payment3);
            SendMessage(payment4);
            SendMessage(payment5);
            SendMessage(payment6);
            SendMessage(payment7);
            SendMessage(payment8);
            SendMessage(payment9);
            SendMessage(payment10);

            Console.ReadLine();

        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" }; //Create a connection factory
            _connection = _factory.CreateConnection(); //Create a connection
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, true, false, false, null);  //If it does not already exist, RabbitMQ will create a queue name after our QueueName variable
        }

        private static void SendMessage(Payment message)
        {
            _model.BasicPublish("", QueueName, null ,message.Serialize()); //Send a message to a queue name after our QueueName variable
            Console.WriteLine("Payment Sent {0}, f{1}", message.CardNumber, message.AmounToPay);
        }
    }
}
