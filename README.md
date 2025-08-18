# ServiceDeskPlus SDK

[![NuGet Version](https://img.shields.io/nuget/v/ServiceDeskPlus.SDK)](https://www.nuget.org/packages/ServiceDeskPlus.SDK)
[![codecov](https://codecov.io/github/jeppevinkel/ServiceDeskPlus.SDK/graph/badge.svg?token=1YH1X27JBL)](https://codecov.io/github/jeppevinkel/ServiceDeskPlus.SDK)

A lightweight .NET SDK that simplifies working with the ManageEngine ServiceDesk Plus REST API. The library provides a typed client and models so you can issue requests and handle responses and API errors in a straightforward, idiomatic C# way.

## What is this project?
- ServiceDeskPlus.SDK: The core SDK library targeting .NET 8 (net8.0). It contains the client(s), request/response models, and error handling utilities for interacting with ServiceDesk Plus.
- ServiceDeskPlus.Tests: Unit tests for the SDK, ensuring correctness and guarding against regressions.

## Why use it?
- Strongly typed models and responses instead of manual JSON handling.
- Clear error handling that surfaces ServiceDesk Plus API status information alongside HTTP details.

## Getting started
While the exact initialization may vary based on your environment and authentication, the general pattern is:

### Simple instantiation
```csharp
using ServiceDeskPlus.SDK;

// Get the host and auth token from environment variables or other configuration sources.
string sdpHost = Environment.GetEnvironmentVariable("SDP_HOST") ?? "https://your-servicedesk-plus-instance";
string sdpAuthToken = Environment.GetEnvironmentVariable("SDP_AUTH_TOKEN") ?? "";

var sdpClient = new SdpClient(sdpHost, sdpAuthToken);

// Example: retrieve a request by id
var result = await sdpClient.Request.GetAsync(12345);
```

### Dependency injection
#### Initialization
```csharp
using ServiceDeskPlus.SDK;

// Assuming the options are stored in the "ServiceDeskPlus" scope of the configuration.
string hostAddress = builder.Configuration.GetValue("ServiceDeskPlus:HostAddress", "https://localhost:8080");
string authToken = builder.Configuration.GetValue("ServiceDeskPlus:AuthToken", "");
builder.Services.AddScoped(s => new ServiceDeskPlus.SDK.SdpClient(hostAddress, authToken));
```

#### Usage
```csharp
using ServiceDeskPlus.SDK;

public class MyService
{
    private readonly SdpClient _sdpClient;
    
    public MyService(SdpClient sdpClient)
    {
        _sdpClient = sdpClient;
    }
    
    public async Task SomeTask()
    {
        var request = await _sdpClient.Request.GetAsync(12345);
        // Do something with the result...
    }
}
```

Adjust the base URL and authentication to match your ServiceDesk Plus instance and configuration.

## Build and test
- Build: `dotnet build`
- Test: `dotnet test`

Both commands should be run from the repository root using .NET 8 SDK.

## Status
This project is currently in very early development and does not yet support all of the available API endpoints.

### Progress
- Requests
  - [x] Get
  - [x] List
  - [ ] Add
  - [ ] Edit
  - [ ] Delete

## Contributing
Issues and pull requests are welcome. If you plan substantial changes, consider opening an issue first to discuss your approach.
