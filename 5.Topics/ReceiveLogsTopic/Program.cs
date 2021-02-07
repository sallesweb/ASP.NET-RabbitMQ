namespace ReceiveLogsTopic
{
    using System;
    using System.Text;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

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
                var queueName = channel.QueueDeclare().QueueName;

                HaveArgumentsOrReturn(args);

                foreach (var bindingKey in args)
                {
                    channel.QueueBind(queue: queueName, exchange: exchange, routingKey: bindingKey);
                }

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;

                    Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void HaveArgumentsOrReturn(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Usage: {0} [binding_key...]", Environment.GetCommandLineArgs()[0]);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }
        }
    }
}
