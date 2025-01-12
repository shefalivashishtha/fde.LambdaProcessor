# fde.LambdaProcessor

This application contains

1. Kinesis Producer Worker Service 
2. Kinesis Consumer Lambda Processor
3. Generic Lambda Processor that receives and returns the above received data

Hosted on AWS using:
1. Kinesis for Data Stream
2. AWS Lambda Function to Trigger and process kinesis data stream
3. API Gateway to expose API

Swagger Documentation of FDE.API. Import in Swagger UI to view this in action:
{
  "swagger" : "2.0",
  "info" : {
    "description" : "This is an API.",
    "version" : "1.0.0",
    "title" : "FDE API"
  },
  "host" : "hm15blhzgb.execute-api.eu-west-1.amazonaws.com",
  "basePath" : "/Development",
  "schemes" : [ "https" ],
  "paths" : {
    "/getLatest" : {
      "post" : {
        "produces" : [ "application/json" ],
        "responses" : {
          "200" : {
            "description" : "200 response",
            "schema" : {
              "$ref" : "#/definitions/ArrayOfResponse"
            }
          }
        }
      },
      "options" : {
        "consumes" : [ "application/json" ],
        "produces" : [ "application/json" ],
        "responses" : {
          "200" : {
            "description" : "200 response",
            "schema" : {
              "$ref" : "#/definitions/Empty"
            },
            "headers" : {
              "Access-Control-Allow-Origin" : {
                "type" : "string"
              },
              "Access-Control-Allow-Methods" : {
                "type" : "string"
              },
              "Access-Control-Allow-Headers" : {
                "type" : "string"
              }
            }
          }
        }
      }
    },
    "/upload" : {
      "post" : {
        "consumes" : [ "application/json" ],
        "produces" : [ "application/json" ],
        "parameters" : [ {
          "in" : "body",
          "name" : "Request",
          "required" : true,
          "schema" : {
            "$ref" : "#/definitions/Request"
          }
        } ],
        "responses" : {
          "201" : {
            "description" : "201 response",
            "schema" : {
              "$ref" : "#/definitions/Request"
            }
          }
        }
      }
    }
  },
  "definitions" : {
    "Response" : {
      "type" : "object",
      "properties" : {
        "ImageUrl" : {
          "type" : "string"
        },
        "Description" : {
          "type" : "string"
        },
        "CreatedDateTime" : {
          "type" : "string"
        },
        "TotalInLastHr" : {
          "type" : "number"
        }
      }
    },
    "Empty" : {
      "type" : "object",
      "title" : "Empty Schema"
    },
    "Request" : {
      "type" : "object",
      "properties" : {
        "ImageUrl" : {
          "type" : "string"
        },
        "Description" : {
          "type" : "string"
        }
      }
    },
    "ArrayOfResponse" : {
      "type" : "object"
    }
  }
}