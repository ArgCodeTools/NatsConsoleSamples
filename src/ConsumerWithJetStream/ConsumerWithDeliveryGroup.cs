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
            Name = consumerName, // It is important that both consumers have the same name so that they belong to the same delivery group (DeliverGroup).
            AckPolicy = ConsumerConfigAckPolicy.Explicit,
            DeliverGroup = deliverGroup,
        };

        await using var nc = new NatsClient(url);

        var js = nc.CreateJetStreamContext();

        var consumer1 = await js.CreateOrUpdateConsumerAsync(streamName, consumerConfig);
        var consumer2 = await js.CreateOrUpdateConsumerAsync(streamName, consumerConfig);

        var task1 = Task.Run(async () =>
        {
            await foreach (var msg in consumer1.ConsumeAsync<string>())
            {
                Console.WriteLine($"[Consumer-1] Received: {msg.Subject} - {msg.Data}");
                await msg.AckAsync();
                Console.WriteLine("[Consumer-1] Message confirmed");
            }
        });

        var task2 = Task.Run(async () =>
        {
            await foreach (var msg in consumer2.ConsumeAsync<string>())
            {
                Console.WriteLine($"[Consumer-2] Received: {msg.Subject} - {msg.Data}");
                await msg.AckAsync();
                Console.WriteLine("[Consumer-2] Message confirmed");
            }
        });

        await Task.WhenAll(task1, task2);
    }
}