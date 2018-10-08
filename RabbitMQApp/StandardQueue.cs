using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;
using Common;

namespace RabbitMQApp
{
    public static class StandardQueue
    {
        private const string QueueName = "StandardQueue_ExampleQueue";
        private static IModel _model;

        private static void CreateQueue()
        {
            var _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            IConnection  connection = _factory.CreateConnection();
            _model = connection.CreateModel();
            _model.QueueDeclare(QueueName, true, false, false, null);
            
        }

        private static void SendMessage(Payment message)
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
    }

    
}
