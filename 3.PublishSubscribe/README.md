# ASP.NET-PublishSubscribe
O objetivo deste projeto é criar aplicativos de mensagens para distribuição da mesma mensagem para vários *Consumers* (padrão Publish/Subscribe) que interagem com o servidor RabbitMQ.  
Foram criados dois projetos baseados no template Application Console do framework .NET 5:

- *Producer (NewTask):* aplicação que envia mensagens;
- *Exchange:* intermedia a passagem de mensagens do *Producer* para a lista;
- *Queue (RabbitMQ):* buffer que armazena mensagens;
- *Consumer (Worker):* aplicação que recebe mensagens.

## Exchange
O *Producer* enviará as mensagens para uma *exchange*, seu funcionamento é simples, por um lado recebe as mensagens do *Producer* e, por outro, envia as mensagens para a fila.

### Tipo de exchange: **Fanout**
Transmite (broadcast) todas as mensagens para todas as listas conhecidas por ele.

## Referências
- [Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html)
- [Publish/Subscribe](https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html)