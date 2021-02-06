﻿namespace EmitLog
{
    using System;
    using System.Text;
    using RabbitMQ.Client;

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs", routingKey: string.Empty, basicProperties: null, body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0)
                ? string.Join(" ", args)
                : "Hello World!");
        }
    }
}
