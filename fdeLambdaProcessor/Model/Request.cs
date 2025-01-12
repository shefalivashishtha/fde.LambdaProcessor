namespace fdeLambdaProcessor.Model
{
	public record Request(string ImageUrl, string Description );
    public record ImageDto (string ImageUrl, string Description, DateTime CreatedDateTime);
    public record ResponseDto(string ImageUrl, string Description, DateTime CreatedDateTime, int TotalInLastHr);
}

