using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace fdeKinesisConsumer;

public class Function
{
    public async Task FunctionHandler(KinesisEvent request, ILambdaContext context)
    {
        if (request is not null)
        {
            if (request.Records != null && request.Records.Count() > 0)
            {
                foreach (var record in request.Records)
                {
                    var decodedData = Encoding.UTF8.GetString(record.Kinesis.Data.ToArray());
                    using (var client = new HttpClient())
                    {
                        var baseAddress = "https://hm15blhzgb.execute-api.eu-west-1.amazonaws.com/Development/upload";
                        HttpContent content = new StringContent(decodedData);
                        var httpResponse = await client.PostAsync(baseAddress, content);
                        Console.WriteLine(httpResponse.IsSuccessStatusCode ? $"Request sent to API" : "Data not sent");

                    }
                }
            }
        }

    }

}