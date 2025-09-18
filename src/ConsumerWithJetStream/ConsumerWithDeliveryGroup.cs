using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;

public static class ConsumerWithDeliveryGroup
{
    public static async Task RunAsync()
    {
        var url = "nats://127.0.0.1:4222";
        var streamName = "EVENTS-SAMPLE";
        var deliverGroup = "workers";
        var consumerName = "consumer-sample";
        
        var consumerConfig = new ConsumerConfig
        {
            Name = consumerName, // El nombre del consumidor debe ser único por grupo de entrega
            AckPolicy = ConsumerConfigAckPolicy.Explicit,
            DeliverGroup = deliverGroup,
        };

        await using var nc = new NatsClient(url);

        var js = nc.CreateJetStreamContext();

        var consumer1 = await js.CreateOrUpdateConsumerAsync(streamName, consumerConfig);
        var consumer2 = await js.CreateOrUpdateConsumerAsync(streamName, consumerConfig);

        var consumerOpts = new NatsJSConsumeOpts
        {
            MaxMsgs = 1 // Procesar un mensaje a la vez
        };

        var task1 = Task.Run(async () =>
        {
            Console.WriteLine("[Consumer-1] Listening...");

            await foreach (var msg in consumer2.ConsumeAsync<string>(opts: consumerOpts))
            {
                Console.WriteLine($"[Consumer-1] Received: {msg.Subject} - {msg.Data}");
                await msg.AckAsync();
                Console.WriteLine("[Consumer-1] Message confirmed");
            }
        });

        var task2 = Task.Run(async () =>
        {
            Console.WriteLine("[Consumer-2] Listening with 5 seconds delay...");

            await foreach (var msg in consumer2.ConsumeAsync<string>(opts: consumerOpts))
            {
                Console.WriteLine($"[Consumer-2] Received: {msg.Subject} - {msg.Data}");
                await Task.Delay(5000); // Simula lentitud en el procesamiento
                await msg.AckAsync();
                Console.WriteLine("[Consumer-2] Message confirmed");
            }
        });

        await Task.WhenAll(task1, task2);
    }
}