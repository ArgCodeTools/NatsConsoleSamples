# SubscriberWithMessaging

This project demonstrates a simple NATS subscriber using C# (.NET 9). The subscriber listens for messages of type `Order` on subjects matching `orders.>`.

## How to Publish a Message from the Command Line (NATS CLI)

You can publish a message to this subscriber using the [NATS CLI](https://docs.nats.io/nats-tools/nats_cli).  
Make sure you have the NATS CLI installed and the NATS server running.

### Example Command

```powershell
'{"OrderId":123}' | nats pub orders.new.123 --server nats://localhost:4222
```


- `'{"OrderId":123}'` is the message payload in JSON format, matching the expected `Order` structure.
- `orders.new.123` is the subject the subscriber is listening to (`orders.>` matches any subject starting with `orders.`).
- `--server` specifies the NATS server address.

### Steps
1. Open your terminal.
2. Run the command above, replacing the payload as needed.

The subscriber will display the received message in its console output.

---

**Note:**  
The message format should match the expected structure (`Order` record: `{ "OrderId": <number> }`).
