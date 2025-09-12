using NATS.Client.JetStream.Models;
using NATS.Net;

public static class BasicConsumer
{
    public static async Task RunAsync()
    {

        var url = "nats://127.0.0.1:4222";

        await using var nc = new NatsClient(url); // Connect to NATS server

        var js = nc.CreateJetStreamContext();

        var streamName = "EVENTS-SAMPLE"; // Stream name and subject to subscribe

        var consumerConfig = new ConsumerConfig("my_consumer")
        {
            AckPolicy = ConsumerConfigAckPolicy.Explicit, // Require explicit acknowledgment
        };

        // Create the consumer in the stream
        var consumer = await js.CreateOrUpdateConsumerAsync(streamName, consumerConfig);

        // Subscribe and process messages
        await foreach (var msg in consumer.ConsumeAsync<string>())
        {
            Console.WriteLine($"Received: {msg.Subject} - {msg.Data}");
            await msg.AckAsync(); // Deletes the message from the stream
            Console.WriteLine("Message confirmed");
        }
    }
}