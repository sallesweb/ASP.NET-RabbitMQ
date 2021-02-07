# ASP.NET-Topics
O objetivo deste projeto é criar aplicativos de mensagens para assinar com base em tópicos, por exemplo, podemos querer ouvir apenas os logs *error* do tópico 'aplicacao' mas também os logs *warning* e *error* do tópico 'geral'.  
Foram criados dois projetos baseados no template Application Console do framework .NET 5:

- *Producer (NewTask):* aplicação que envia mensagens;
- *Exchange:* intermedia a passagem de mensagens do *Producer* para a lista;
- *Queue (RabbitMQ):* buffer que armazena mensagens;
- *Consumer (Worker):* aplicação que recebe mensagens.

## Exchange
O *Producer* enviará as mensagens para uma *exchange*, seu funcionamento é simples, por um lado recebe as mensagens do *Producer* e, por outro, envia as mensagens para a fila.

### Tipos de exchange: **Topic**
Similar ao tipo *exchange direct*, o *topic* transmite uma mensagem com um *routing key* específico para ser entregue com base em tópicos. O *routing key* do tipo *topic* deve ser uma lista de palavras separadas por pontos e limitado à 255 caracteres, nomalmente o nome é sugestivo quanto ao que a mensagem representa.

- ***:** pode substituir somente uma palavra
> \*.aplicacao.\*  
> \*.\*.geral
- **#:** pode substituir zero ou mais palavras
> geral.\#

## Referências
- [Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html)
- [Topics](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)