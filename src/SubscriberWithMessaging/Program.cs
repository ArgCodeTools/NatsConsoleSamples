using NATS.Net;

var url = "nats://127.0.0.1:4222";

await using var nc = new NatsClient(url);

Console.WriteLine($"Connected to NATS server at {url}");
Console.WriteLine("Subscriber is listening for messages.");

await foreach (var msg in nc.SubscribeAsync<Order>("orders.>"))
{
    var order = msg.Data;
    Console.WriteLine($"Subscriber received {msg.Subject}: {order}");
}

public record Order(int OrderId);