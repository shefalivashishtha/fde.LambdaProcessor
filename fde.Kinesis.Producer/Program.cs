using Amazon.Kinesis.Model;
using Amazon.Kinesis;
using System.Text.Json;
using System.Text;
using fde.Kinesis.Producer;

DataProvider provider = new DataProvider();
while (true)
{
    using (var kinesisClient = new AmazonKinesisClient())
    {
        foreach (var request in await provider.GetRandomRequest())
        {
            var imageRecord = new PutRecordRequest
            {
                StreamName = "fde-data-stream",
                PartitionKey = Guid.NewGuid().ToString(),
                Data = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)))
            };

            var response = await kinesisClient.PutRecordAsync(imageRecord);

            Console.WriteLine($"Request sent for {request.ImageUrl}, {request.Description}");

            Thread.Sleep(new TimeSpan(0, 58, 0));
        }
    }
}


