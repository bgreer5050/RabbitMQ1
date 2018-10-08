using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace RabbitMQApp
{
    public class WorkerQueue
    {
        private const string QueueName = "WorkerQueue_Queue";
        private static IModel _model;
        static RabbitMQ.Client.ConnectionFactory _factory;
        static IConnection _connection;


        public static void CreateQueue()
        {
            //var _factory = new ConnectionFactory
            //{
            //    HostName = "localhost",
            //    UserName = "guest",
            //    Password = "guest"
            //};

            //IConnection connection = _factory.CreateConnection();
            //_model = connection.CreateModel();
            //_model.QueueDeclare(QueueName, true, false, false, null);

        }

        public static void SendMessage(Payment message)
        {
            // Leaving the exchange blank. Publish will use the default exchange
            // The message will be published to the StandardQueue_ExampleQueue queue
            // The third parameter is if we want to pass some properties along with the message (i.e. correlation Id, reply to addresses, etc
            //Finally we take our message and serialize it into a byte array using the extension method we created in our Object Serialize class.
            _model.BasicPublish("", QueueName, null, message.Serialize());

            Console.WriteLine(" Payment message sent:{0} :{1}",
                                message.CardNumber,
                                message.AmounToPay);
        }

        public static void ReceiveMessage()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    channel.BasicQos(0, 1, false);
                }
            }

            
        }
    }
}
