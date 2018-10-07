using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;

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
            _model.QueueDeclare(QueueName, false, false, false, null);
            
        }

        private static void SendMessage(Payment message)
        {
            //Leaving the exchange blank. Publish will use the default exchange
            _model.BasicPublish("", QueueName, null, message.Serialize());

            Console.WriteLine(" Payment message sent:{0} :{1}",
                                message.CardNumber,
                                message.AmountToPay);
        }
    }

    
}
