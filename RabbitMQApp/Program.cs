using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using Newtonsoft;



namespace RabbitMQApp
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

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

            StandardQueue.CreateQueue();

            StandardQueue.SendMessage(payment1);
            StandardQueue.SendMessage(payment2);
            StandardQueue.SendMessage(payment3);
            StandardQueue.SendMessage(payment4);
            StandardQueue.SendMessage(payment5);
            StandardQueue.SendMessage(payment6);
            StandardQueue.SendMessage(payment7);
            StandardQueue.SendMessage(payment8);
            StandardQueue.SendMessage(payment9);
            StandardQueue.SendMessage(payment10);

            StandardQueue.ReceiveMessage();
            Console.ReadLine();
        }
    }
}
