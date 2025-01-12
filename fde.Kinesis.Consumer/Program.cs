using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace fde.Kinesis.Consumer;

class Program
{
    static async Task Main(string[] args)
    {
        string streamName = "fde-data-stream";
        using(var kinesisClient = new AmazonKinesisClient())
        {
            Console.WriteLine("Starting to read kinesis stream");

            GetShardIteratorRequest shardRequest = new GetShardIteratorRequest
            {
                StreamName = streamName,
                ShardId = "shardId-000000000000",
                ShardIteratorType = ShardIteratorType.TRIM_HORIZON
            };

            var response = await kinesisClient.GetShardIteratorAsync(shardRequest);
            var shardIterator = response.ShardIterator;
            while (!string.IsNullOrEmpty(shardIterator))
            {
                var getRecordsRequest = new GetRecordsRequest
                {
                    ShardIterator = shardIterator,
                    Limit = 20
                };

                var records = await kinesisClient.GetRecordsAsync(getRecordsRequest);
                foreach(var request in records.Records)
                {
                    var decodedData = System.Text.Encoding.UTF8.GetString(request.Data.ToArray());
                    using (var client = new HttpClient())
                    {
                        var baseAddress = "https://hm15blhzgb.execute-api.eu-west-1.amazonaws.com/Development/upload";
                        HttpContent content = new StringContent(decodedData);
                        var httpResponse = await client.PostAsync(baseAddress, content);
                        Console.WriteLine(httpResponse.IsSuccessStatusCode ? $"Request sent to API" : "Data not sent");

                    }
                }

                shardIterator = records.NextShardIterator;
                Thread.Sleep(new TimeSpan(1, 5, 0));
            }
        }
        
    }
}

