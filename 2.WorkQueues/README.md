# ASP.NET-WorkQueues
Implementação de dois projetos baseados no template Application Console do framework .NET 5 para distribuição de tarefas entre os *Workers* interagindo com o servidor RabbitMQ.
- *Publisher (NewTask):* envia mensagem para a lista do servidor RabbitMQ;
- *Consumer (Worker):* consome as mensagens da lista do servidor RabbitMQ.

## Referências
- [Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html)
- [Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)
    - [Competing Consumers](https://www.enterpriseintegrationpatterns.com/patterns/messaging/CompetingConsumers.html)