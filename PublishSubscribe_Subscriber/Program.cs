using Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishSubscribe_Subscriber
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static QueueingBasicConsumer _consumer;

        private const string ExchangeName = "PublishSubscribe_Exchange";

        static void Main(string[] args)
        {
            //create connection factory
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };

            //create connection and the model
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    var queueName = DeclareAndBindQueueToExchange(channel);
                    channel.BasicConsume(queueName.ToString(), true, _consumer);  //We use auto ack as we do not need to tell the Exchange we processed the message in a fanout topology.

                    //Dequeue messages forever
                    while(true)
                    {
                        var ea = _consumer.Queue.Dequeue();
                        var message = (Payment)ea.Body.DeSerialize(typeof(Payment));

                        Console.WriteLine("-----Payment Processed {0} : {1}", message.CardNumber, message.AmounToPay);
                    }
                }
            }
        }

        private static object DeclareAndBindQueueToExchange(IModel channel)
        {
            channel.ExchangeDeclare(ExchangeName, "fanout"); //Declare an exchange.  If it exists it will not be overwritten
            var queueName = channel.QueueDeclare().QueueName; //We get a queue name 
            channel.QueueBind(queueName, ExchangeName, ""); //We bind the queue name to the exchange
            _consumer = new QueueingBasicConsumer(channel); //We create our QueueingBasicConsumer
            return queueName;
        }
    }
}
