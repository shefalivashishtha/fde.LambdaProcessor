using System;
using System.Text.Json;

namespace fde.Kinesis.Producer
{
	public class DataProvider
	{
		public async Task<List<Request>> GetRandomRequest()
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

					response = images.Select(x => new Request(x.url, $"Image is by {x.author}")).ToList();
					
				}

            }

            return response;
        }
	}

	public record Image(string id, string author,int width, int height, string url, string download_url);
    public record Request(string ImageUrl, string Description);
}

