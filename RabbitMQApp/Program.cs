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
        static void Main(string[] args)
        {
            //Make a connection to RabbitMQ
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            IConnection connection = factory.CreateConnection();

            //Open a channel to RabbitMQ
            IModel channel = connection.CreateModel();  //This channel can now be used to send and receive messages

            //Declare an exchange
            channel.ExchangeDeclare("MyExchange", "direct");

            //Declare a queue
            channel.QueueDeclare("MyQueue");

            //Bind the queue to the exchange
            channel.QueueBind("MyQueue", "MyExchange","");


            //RabbitMQ Message Workflow as it pertains to C# objects

            //Create an object
            Payment payment1 = new Payment {
                                            AmounToPay = 25.0m,
                                            CardNumber = "1234123412341234"
                                            };

            //Here we do two things.  First we serialize our object into a JSON string 
            //then we call the GetBytes method on it to get a byte array

            byte[] serialized = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payment1));

            //RabbitMQ deserailizes the object.  In C# if we were to deserialize the object
            //we may go about it by getting the string and then use JsonConvert to
            //deserialize
            string bytesAsString = Encoding.UTF8.GetString(serialized);
            Payment DeserializedPayment = JsonConvert.DeserializeObject<Payment>(bytesAsString);
            

        }
    }
}
