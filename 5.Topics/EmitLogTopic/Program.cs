namespace EmitLogTopic
{
    using System;
    using System.Linq;
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
                var exchange = "topic_logs";
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);

                var routingKey = GetRoutingKey(args);
                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);

                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
            }
        }

        private static string GetRoutingKey(string[] args)
        {
            return (args.Length > 0)
                ? args[0]
                : "anonymous.info";
        }

        private static string GetMessage(string[] args)
        {
            return (args.Length > 1)
                ? string.Join(" ", args.Skip(1).ToArray())
                : "Hello World!";
        }
    }
}
