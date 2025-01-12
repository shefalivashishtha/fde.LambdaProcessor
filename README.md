# fde.LambdaProcessor

This application contains

1. Kinesis Producer Worker Service 
2. Kinesis Consumer Lambda Processor
3. Generic Lambda Processor that receives and returns the above received data

Hosted on AWS using:
1. Kinesis for Data Stream
2. AWS Lambda Function to Trigger and process kinesis data stream
3. API Gateway to expose API

# Accessing Swagger UI
To access and view swagger details for backend API, follow below steps:
1. Copy/download contents of [swagger.yaml](https://github.com/shefalivashishtha/ui.fde/blob/main/swagger.yaml)
2. Navigate to `https://editor-next.swagger.io/` and paste above copied contents.
3. Use Swagger UI to upload and./or get latest image.

Test Payload to upload:
{
    "ImageUrl":"https://picsum.photos/id/237/200/300",
    "Description":"Hello world"
}