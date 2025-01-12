using System.Text;
using System.Text.Json;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace fde.Kinesis.ProducerWorker;

public class ProducerWorker : BackgroundService
{
    private readonly ILogger<ProducerWorker> _logger;

    public ProducerWorker(ILogger<ProducerWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Process();

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task Process()
    {
        using (var kinesisClient = new AmazonKinesisClient())
        {
            foreach (var request in await GetRandomRequest())
            {
                var imageRecord = new PutRecordRequest
                {
                    StreamName = "fde-data-stream",
                    PartitionKey = Guid.NewGuid().ToString(),
                    Data = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)))
                };

                var response = await kinesisClient.PutRecordAsync(imageRecord);

                _logger.LogInformation($"Request Status : {response.HttpStatusCode} for Request : {request.ImageUrl}, {request.Description}");
            }

            Thread.Sleep(new TimeSpan(0, 58, 0));
        }
    }

    private  async Task<List<Request>> GetRandomRequest()
    {
        List<Request> response = new List<Request>();
        using (var client = new HttpClient())
        {
            var baseAddress = "https://picsum.photos/v2/list";

            var httpResponse = await client.GetAsync(baseAddress);
            if (httpResponse.IsSuccessStatusCode)
            {
                var serializedResponse = await httpResponse.Content.ReadAsStringAsync();
                var images = JsonSerializer.Deserialize<List<Image>>(serializedResponse);

                response = images.Select(x => new Request(x.download_url, $"Image is by {x.author}")).ToList();

            }

        }

        return response;
    }

}

record Image(string id, string author, int width, int height, string url, string download_url);
record Request(string ImageUrl, string Description);

