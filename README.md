# Repository Overview

This repository contains

1. fde.Kinesis.ProducerWorker : To produce data into the kinesis data stream
2. fdeConsumerProcessor : To process data published in kinesis data stream.
3. fdeLambdaProcessor : To process API requests and return latest received object

# Infra details

Each of the above listed component uses:
1. Kinesis for Data Stream
2. AWS Lambda Function to Trigger and process kinesis data stream
3. API Gateway to expose following two endpoints
    - Upload Image: `https://hm15blhzgb.execute-api.eu-west-1.amazonaws.com/Development/upload` 
    - Get Latest Image - `https://hm15blhzgb.execute-api.eu-west-1.amazonaws.com/Development/getLatest` 

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

# UI Application

Client application - http://ui-fde-bucket.s3-website-eu-west-1.amazonaws.com/  