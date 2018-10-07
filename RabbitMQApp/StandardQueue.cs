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

        private static void CreateQueue()
        {
            var _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            IConnection  connection = _factory.CreateConnection();
            IModel _model = connection.CreateModel();
            _model.QueueDeclare(QueueName, false, false, false, null);
            
        }


    }
}
