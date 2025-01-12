using System;
using System.Text.Json;
using fdeLambdaProcessor.Model;
namespace fdeLambdaProcessor.Provider
{

    public class LocalDb
    {
        private const string filePath = "/tmp/imageData.txt";


        public async Task Insert(Request request)
        {
            await WriteData(request);
        }


        public async Task<ResponseDto> GetLatest()
        {
            return await GetLatestRequestAsync();
        }

        private async Task<ResponseDto> GetLatestRequestAsync()
        {
            var existingContent = await ReadAllDataAsync();
            if (existingContent != null && existingContent.Count() != 0)
            {
                var latestImage = existingContent.OrderByDescending(x => x.CreatedDateTime).First();
                var imagesUploadedLastHr = existingContent.Count(x => DateTime.UtcNow.Subtract(x.CreatedDateTime).TotalHours <= 1);
                return new ResponseDto(
                    latestImage.ImageUrl,
                    latestImage.Description,
                    latestImage.CreatedDateTime,
                    imagesUploadedLastHr
                );
            }
            return null;
        }

        private async Task WriteData(Request request)
        {

            var currentList = await ReadAllDataAsync() ?? new List<ImageDto>();
            ImageDto insertResponse = new ImageDto(request.ImageUrl, request.Description, DateTime.Now);

            using (var writer = currentList.Count() == 0 ? File.CreateText(filePath) : new StreamWriter(filePath, false))
            {

                currentList.Add(insertResponse);

                var contentToWrite = JsonSerializer.Serialize(currentList);
                await writer.WriteAsync(contentToWrite);
            }

        }

        private async Task<List<ImageDto>?> ReadAllDataAsync()
        {
            string serializedText = string.Empty;
            if (File.Exists(filePath))
            {

                using (var reader = new StreamReader(filePath))
                {
                    serializedText = await File.ReadAllTextAsync(filePath);
                }
                return !string.IsNullOrEmpty(serializedText) ? JsonSerializer.Deserialize<List<ImageDto>>(serializedText) : new List<ImageDto>();
            }
            return null;
        }
    }
}

