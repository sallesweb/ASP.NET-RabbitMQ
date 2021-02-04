# ASP.NET-WorkQueues
O objetivo deste projeto é criar aplicativos de mensagens para distribuição de tarefas entre os *Workers* que interagem com o servidor RabbitMQ.  
Foram criados dois projetos baseados no template Application Console do framework .NET 5:
- *Producer (NewTask):* aplicação que envia mensagens;
- *Queue (RabbitMQ):* buffer que armazena mensagens;
- *Consumer (Worker):* aplicação que recebe mensagens.

## Referências
- [Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html)
- [Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)
    - [Competing Consumers](https://www.enterpriseintegrationpatterns.com/patterns/messaging/CompetingConsumers.html)