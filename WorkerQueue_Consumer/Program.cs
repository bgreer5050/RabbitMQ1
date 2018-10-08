using Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerQueue_Consumer
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;

        private const string QueueName = "WorkerQueue_Queue";

        static void Main(string[] args)
        {
            Receive();

            Console.ReadLine();
        }

        public static void Receive()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(QueueName, false, consumer);

                    // We receive messages forever
                    while(true)
                    {
                        var ea = consumer.Queue.Dequeue(); //We remove the message from the queue
                        var message = (Payment)ea.Body.DeSerialize(typeof(Payment)); //We DeSerialize the message into a Payment object.
                        channel.BasicAck(ea.DeliveryTag, false);  //We send an acknowledgement to RabbitMQ to acknowledge that we processed the message.

                        Console.WriteLine("----- Payment Processed {0} : {1}", message.CardNumber, message.AmounToPay);
                    }
                }
            }
        }
    }
}
