# ASP.NET-Routing
O objetivo deste projeto é criar aplicativos de mensagens para assinar apenas um subconjunto específico de mensagens, por exemplo direcionar logs críticos para arquivo de log visando economia de espaço em disco e exibir todos os tipos de log no console.  
Foram criados dois projetos baseados no template Application Console do framework .NET 5:

- *Producer (NewTask):* aplicação que envia mensagens;
- *Exchange:* intermedia a passagem de mensagens do *Producer* para a lista;
- *Queue (RabbitMQ):* buffer que armazena mensagens;
- *Consumer (Worker):* aplicação que recebe mensagens.

## Exchange
O *Producer* enviará as mensagens para uma *exchange*, seu funcionamento é simples, por um lado recebe as mensagens do *Producer* e, por outro, envia as mensagens para a fila.

### Tipos de exchange: **Direct**
Transmite a mensagem para as listas cuja *binding key* seja exatamente igual à *routing key* da mensagem.

## Referências
- [Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html)
- [Routing](https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html)