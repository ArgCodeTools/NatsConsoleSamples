using NATS.Client.JetStream.Models;
using NATS.Net;

var url = "nats://127.0.0.1:4222";
var streamName = "EVENTS-SAMPLE";
var subject = "events.>";

await using var nc = new NatsClient(url); // Connect to NATS server
var js = nc.CreateJetStreamContext();

var config = new StreamConfig(streamName, [subject])
{
    Storage = StreamConfigStorage.File,
    Retention = StreamConfigRetention.Workqueue,
    AllowDirect = true
};

await js.CreateOrUpdateStreamAsync(config);

for (int i = 0; i < 20; i++)
{
    var topic = $"events.JetstreamTest.{i}";
    await js.PublishAsync<string>(topic, $"JetStream event {i}");
    Console.WriteLine($"Published message to {topic}");
}
