using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using fdeLambdaProcessor.Model;
using fdeLambdaProcessor.Provider;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace fdeLambdaProcessor;

public class Function
{
    public LocalDb dbProvider { get; set; }
    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var _dbProvider = dbProvider ?? new LocalDb();

        if (string.IsNullOrEmpty(request.Body))
        {
            var response = await _dbProvider.GetLatest();
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Headers = getHeaders(),
                Body = response == null ? "No data in the collection. Upload some images first to fetch data" : JsonSerializer.Serialize(response)
            };
        }
        else
        {
            var deserializeBody = JsonSerializer.Deserialize<Request>(request.Body);
            if (deserializeBody is Request)
            {
                await _dbProvider.Insert(deserializeBody);
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,

                    Headers = getHeaders(),
                    Body = request.Body
                };
            }
        }

        return new APIGatewayProxyResponse
        {
            StatusCode = 200
        };
    }

    private Dictionary<string, string> getHeaders() => new Dictionary<string, string>()
    {
        { "Access-Control-Allow-Origin","*"},
        { "Access-Control-Allow-Methods", "OPTIONS,GET,POST,PUT" },
        { "Access-Control-Allow-Headers", "Content-Type" }
    };


}

