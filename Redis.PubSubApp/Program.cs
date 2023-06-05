//korucu

using StackExchange.Redis;

var redis = await ConnectionMultiplexer.ConnectAsync("localhost:1923");

var subscriber = redis.GetSubscriber();

while (true)
{
    Console.Write("Message: ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("Korucu", message);
}