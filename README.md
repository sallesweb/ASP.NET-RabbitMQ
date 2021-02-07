# ASP.NET-RabbitMQ
O objetivo deste repositório é aplicar na prática os fundamentos da criação de aplicativos de mensages usando o RabbitMQ.  
Para exemplificar cada cenário, será criado um projeto para cada nova funcionalidade a ser testada:
1. [Hello World](./1.HelloWorld)
1. [Work Queues](./2.WorkQueues)
1. [Publish/Subscribe](./3.PublishSubscribe)
1. [Routing](./4.Routing)

> **Importante:** Para execução dos exemplos deste repositório o servidor RabbitMQ deve estar instalado.
>
>```
>docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
>```

## Enviando e recebendo mensagens

- Criando conexão e um canal de comunicação com o servidor RabbitMQ.
```csharp
var factory = new ConnectionFactory() { HostName = "localhost" };
using(var connection = factory.CreateConnection())
using(var channel = connection.CreateModel())
{
    ...
}
```

- Declarando uma fila idempotente, ela só será criada se ainda não existir, para publicar ou consumir mensagens.
```csharp
channel.QueueDeclare (
    queue: "hello" ,
    durável: false ,
    exclusivo: false ,
    autoDelete: false ,
    argumentos: null);
```

- **Publisher:** Como o conteúdo da mensagem é um Array de bytes, é possível configurar o que quiser nela.
```csharp
string message = "Hello World!";
​var body = Encoding.UTF8.GetBytes(message);

​channel.BasicPublish(
    exchange: "",
    ​routingKey: "hello",
    ​basicProperties: null,
    ​body: body);
```

- **Consumer:** Para solicitar as mensagens da fila ao servidor RabbitMQ, utilizamos o manipulador de eventos *EventingBasicConsumer.Received*.
```csharp
var consumer = new EventingBasicConsumer(channel);
​consumer.Received += (model, ea) =>
​{
    ​var body = ea.Body.ToArray();
    ​var message = Encoding.UTF8.GetString(body);

    channel.BasicConsume(
        queue: "hello",
        ​autoAck: true,
        ​consumer: consumer);
​};
```

## Confirmação de mensagem
- ***Consumer died:*** Para garantir que uma mensagem não seja perdida mesmo se o *Consumer* parar de funcionar, podemos enviar uma confirmação assim que a tarefa for concluída. Todas as mensagens não confirmadas serão reenviadas.

> Desabilitando a confirmação de mensagem automática:
```csharp
channel.BasicConsume(
    queue: "task_queue",
    autoAck: false,
    consumer: consumer);
```

> Habilitando a confirmação de mensagem manual:
```csharp
channel.BasicAck(
    deliveryTag: ea.DeliveryTag,
    multiple: false);
```

- ***RabbitMQ server died:*** Para diminuir as chances de uma mensagem ser perdida, mesmo se o servidor RabbitMQ travar ou for encerrado, podemos marcar a fila e as mensagens como duráveis. Para uma garantia maior podemos usar o *publisher confirms*.

> Definindo a lista como durável tanto no *Publisher* quanto no *Consumer*:
```csharp
channel.QueueDeclare(
    queue: "hello",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);
```

> Definindo as mensagens como duráveis no *Publisher*:
```csharp
var properties = channel.CreateBasicProperties();
properties.Persistent = true;
```

## Distribuição de mensagens
Por padrão o RabbitMQ distribuirá as mensagens igualitariamente entre os *Consumers*, para alterar esse comportamento e apenas enviar a mensagem quando o *Consumer* não estiver ocupado, podemos utilizar o método *BasicQos* fornecendo o parâmetro *prefetchCount = 1*.
```csharp
channel.BasicQos(
    prefetchSize: 0,
    prefetchCount: 1,
    global: false);
```

## Exchange
O *Producer* enviará as mensagens para uma *exchange*, seu funcionamento é simples, por um lado recebe as mensagens do *Producer* e, por outro, envia as mensagens para a fila.

### Tipo de exchange: **Fanout**
Transmite (broadcast) todas as mensagens para todas as listas conhecidas por ele.

> Criando uma exchange
```csharp
channel.ExchangeDeclare(
    exchange: "logs",
    type: ExchangeType.Fanout);
```

> **Producer** publicando uma mensagem no exchange
```csharp
channel.BasicPublish(
    exchange: "logs",
    routingKey: "",
    basicProperties: null,
    body: body);
```

> **Consumer** criando uma lista temporária
```csharp
var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueName,
    exchange: "logs",
    routingKey: "");
```

### Tipos de exchange: **Direct**
Transmite as mensagens para as listas cuja *binding key* seja exatamente igual à *routing key* da mensagem.

> Criando uma exchange
```csharp
channel.ExchangeDeclare(
    exchange: "direct_logs",
    type: ExchangeType.Direct);
```

> **Producer:** definindo a *routing key*
```csharp
channel.BasicPublish(
    exchange: "direct_logs",
    routingKey: "critical",
    basicProperties: null,
    body: body);
```

> **Consumer:** definindo o *binding key*
```csharp
var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueName,
    exchange: "direct_logs",
    routingKey: "critical");
```

## Referências
- [Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html)
- [Tutoriais RabbitMQ](https://www.rabbitmq.com/getstarted.html)