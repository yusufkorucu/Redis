using StackExchange.Redis;

var redis = await ConnectionMultiplexer.ConnectAsync("localhost:1923");

var subscriber = redis.GetSubscriber();

subscriber.Subscribe("Korucu.*", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();