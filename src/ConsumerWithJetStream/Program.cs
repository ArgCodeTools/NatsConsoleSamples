class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Select the consumer type:");
        Console.WriteLine("1. deliverygroup");
        Console.WriteLine("2. basic");
        Console.Write("Enter the option number: ");
        var input = Console.ReadLine();

        string? opcion = input switch
        {
            "1" => "deliverygroup",
            "2" => "basic",
            _ => null
        };

        if (string.IsNullOrEmpty(opcion))
        {
              Console.WriteLine("Unrecognized option.");
            return;
        }

        switch (opcion)
        {
            case "deliverygroup":
                await ConsumerWithDeliveryGroup.RunAsync();
                break;
            case "basic":
                await BasicConsumer.RunAsync();
                break;
            // Add more cases as needed
            default:
                    Console.WriteLine("Unrecognized option.");
                break;
        }
    }
}