using Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectRouting_Publisher
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "DirectRouting_Exchange";
        private const string CardPaymentQueueName = "CardPaymentDirectRouting_Queue";
        private const string PurchaseOrderQueueName = "PurchaseOrderDirectRouting_Queue";

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

            var purchaseOrder1 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder2 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder3 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder4 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder5 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder6 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder7 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder8 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder9 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };
            var purchaseOrder10 = new PurchaseOrders { AmountToPay = 50.0m, CompanyName = "ABC Comp", PaymentDayTerms = 75, PONumber = "12122" };


            CreateConnection();

            SendPayment(payment1);
            SendPayment(payment2);
            SendPayment(payment3);
            SendPayment(payment4);
            SendPayment(payment5);
            SendPayment(payment6);
            SendPayment(payment7);
            SendPayment(payment8);
            SendPayment(payment9);
            SendPayment(payment10);

            SendPurchaseOrder(purchaseOrder1);
            SendPurchaseOrder(purchaseOrder2);
            SendPurchaseOrder(purchaseOrder3);
            SendPurchaseOrder(purchaseOrder4);
            SendPurchaseOrder(purchaseOrder5);
            SendPurchaseOrder(purchaseOrder6);
            SendPurchaseOrder(purchaseOrder7);
            SendPurchaseOrder(purchaseOrder8);
            SendPurchaseOrder(purchaseOrder9);
            SendPurchaseOrder(purchaseOrder10);

            Console.ReadLine();

        }

      

        private static void SendPayment(Payment payment5)
        {
            throw new NotImplementedException();
        }

        private static void SendPurchaseOrder(PurchaseOrders purchaseOrder6)
        {
            throw new NotImplementedException();
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" }; //Create a connection factory
            _connection = _factory.CreateConnection(); //Create a connection
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "direct");
            _model.QueueDeclare(CardPaymentQueueName, true, false, false, null);
            _model.QueueDeclare(PurchaseOrderQueueName, true, false, false, null);

            _model.QueueBind(CardPaymentQueueName, ExchangeName, "CardPayment");
            _model.QueueBind(PurchaseOrderQueueName, ExchangeName, "PurchaseOrder");
        }

        private static void SendMessage(byte[] message, string routingKey)
        {
            _model.BasicPublish(ExchangeName, routingKey, null, message);
        }


    }
}
